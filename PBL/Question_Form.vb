Imports System.Text
Imports System.Text.Json
Imports System.Drawing.Drawing2D
Imports System.IO

Public Class Question_Form

    Private Shared ReadOnly PRODI_OPTIONS As String() = {"TI", "TMJ", "TMD", "Lainnya"}
    Private Shared ReadOnly PS_OPTIONS As String() = {"0.0", "0.2", "0.4", "0.6", "0.8", "1.0"}
    Private Shared ReadOnly WORK_OPTIONS As String() = {"Individu", "Kelompok"}
    Private Shared ReadOnly DOMAIN_OPTIONS As String() = {"Algoritmik", "Desain kreatif", "Perangkat keras"}
    Private Shared ReadOnly METHOD_OPTIONS As String() = {"Experimental", "R&D", "Case Study", "Simulation"}
    Private Shared ReadOnly OUTPUT_OPTIONS As String() = {"Aplikasi", "Game", "IoT Device", "Model AI", "Other"}
    Private Shared ReadOnly YESNO_OPTIONS As String() = {"Ya", "Tidak"}

    Private Shared ReadOnly LANGUAGE_LIST As String() = {"Python", "Java", "C++", "C#", "JavaScript", "PHP", "Kotlin", "Go", "R", "MATLAB"}

    Private answers As New Dictionary(Of String, Object)()
    Private currentStep As Integer = 1

    Private titleLabel As Label
    Private subtitleLabel As Label

    Private Sub Question_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Position and rounded panel card
        Dim found = Me.Controls.Find("PanelCard", True)
        If found.Length > 0 Then
            Dim panelCard As Panel = DirectCast(found(0), Panel)
            Dim leftFound = Me.Controls.Find("PanelLeft", True)
            If leftFound.Length > 0 Then
                Dim panelLeft As Panel = DirectCast(leftFound(0), Panel)
                Dim margin As Integer = 30
                panelCard.Left = panelLeft.Right + margin
                panelCard.Top = (Me.ClientSize.Height - panelCard.Height) \ 2
            End If

            Try
                Dim r As Integer = 16
                Dim rect As Rectangle = panelCard.ClientRectangle
                Dim path As New Drawing2D.GraphicsPath()
                path.AddArc(rect.X, rect.Y, r, r, 180, 90)
                path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90)
                path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90)
                path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90)
                path.CloseFigure()
                panelCard.Region = New Region(path)
            Catch
            End Try
        End If

        ' Make logo smaller and position top-left
        Try
            If PictureLogo IsNot Nothing Then
                PictureLogo.SizeMode = PictureBoxSizeMode.Zoom
                PictureLogo.Width = 64
                PictureLogo.Height = 64
                PictureLogo.Left = 12
                PictureLogo.Top = 12
            End If
        Catch
        End Try

        ' Create title and subtitle centrally
        Try
            titleLabel = New Label()
            titleLabel.Text = "Sistem Pakar ThesisBuddy"
            titleLabel.ForeColor = Color.FromArgb(102, 204, 255)
            titleLabel.AutoSize = False
            titleLabel.Height = 36
            titleLabel.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
            titleLabel.TextAlign = ContentAlignment.MiddleCenter
            titleLabel.Dock = DockStyle.Top

            subtitleLabel = New Label()
            subtitleLabel.Text = "Rekomendasi Teknologi untuk Skripsimu"
            subtitleLabel.ForeColor = Color.LightGray
            subtitleLabel.AutoSize = False
            subtitleLabel.Height = 20
            subtitleLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            subtitleLabel.TextAlign = ContentAlignment.MiddleCenter
            subtitleLabel.Dock = DockStyle.Top

            Me.Controls.Add(subtitleLabel)
            Me.Controls.Add(titleLabel)
            titleLabel.BringToFront()
            subtitleLabel.BringToFront()

            If FlowLayoutPanelQuestions IsNot Nothing Then
                FlowLayoutPanelQuestions.Padding = New Padding(12)
            End If
        Catch
        End Try

        FlowLayoutPanelQuestions.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom

        ' Hide designer cancel button if present to avoid duplicate
        Try
            Dim bc = Me.Controls.Find("ButtonCancel", True)
            If bc.Length > 0 Then
                bc(0).Visible = False
            End If
        Catch
        End Try

        DatabaseHelper.EnsureQuestionsTable()
        currentStep = 1
        ShowQuestionsForStep(currentStep)
    End Sub

    Private Function ControlWidth() As Integer
        Dim w = FlowLayoutPanelQuestions.ClientSize.Width - FlowLayoutPanelQuestions.Padding.Horizontal - 10
        If w < 100 Then w = 100
        Return w
    End Function

    Private Sub ShowQuestionsForStep(stepIndex As Integer)
        FlowLayoutPanelQuestions.Controls.Clear()
        PanelNav.Controls.Clear()

        Dim questions = DatabaseHelper.GetQuestionsByStep(stepIndex)
        If questions Is Nothing OrElse questions.Count = 0 Then
            Dim lbl As New Label With {.Text = "Tidak ada pertanyaan di langkah ini.", .ForeColor = Color.LightGray, .AutoSize = True}
            FlowLayoutPanelQuestions.Controls.Add(lbl)
        Else
            For Each q In questions
                Dim lbl As New Label()
                lbl.Text = q.Prompt
                lbl.ForeColor = Color.White
                lbl.AutoSize = False
                lbl.Width = ControlWidth()
                lbl.Height = 22
                FlowLayoutPanelQuestions.Controls.Add(lbl)

                Dim typ As String = ""
                If q.QType IsNot Nothing Then typ = q.QType.ToLower()

                Select Case typ
                    Case "text", "string"
                        Dim tb As New TextBox()
                        tb.Width = ControlWidth()
                        tb.ForeColor = Color.White
                        tb.BackColor = Color.FromArgb(24, 30, 36)
                        FlowLayoutPanelQuestions.Controls.Add(tb)
                        AddHandler tb.LostFocus, Sub(s, ev) answers(q.QKey) = tb.Text

                    Case "number"
                        Dim tbn As New TextBox()
                        tbn.Width = ControlWidth()
                        tbn.ForeColor = Color.White
                        tbn.BackColor = Color.FromArgb(24, 30, 36)
                        FlowLayoutPanelQuestions.Controls.Add(tbn)
                        AddHandler tbn.LostFocus, Sub(s, ev)
                                                      Dim d As Double
                                                      If Double.TryParse(tbn.Text, d) Then
                                                          answers(q.QKey) = d
                                                      Else
                                                          answers(q.QKey) = 0.0
                                                      End If
                                                  End Sub

                    Case "kvlist"
                        ' create table layout with 3 columns
                        Dim cols As Integer = 3
                        Dim rows As Integer = CInt(Math.Ceiling(LANGUAGE_LIST.Length / CDbl(cols)))
                        Dim tlp As New TableLayoutPanel()
                        tlp.AutoSize = True
                        tlp.ColumnCount = cols
                        tlp.RowCount = rows
                        tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
                        tlp.Width = ControlWidth()
                        tlp.Padding = New Padding(0)
                        tlp.Margin = New Padding(0, 4, 0, 8)

                        For i As Integer = 0 To cols - 1
                            tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, CSng(100.0 / cols)))
                        Next

                        Dim idx As Integer = 0
                        For Each lang In LANGUAGE_LIST
                            Dim r = idx \ cols
                            Dim c = idx Mod cols

                            Dim itemPanel As New Panel()
                            itemPanel.AutoSize = True
                            itemPanel.Padding = New Padding(2)

                            Dim l As New Label()
                            l.Text = lang
                            l.ForeColor = Color.LightGray
                            l.AutoSize = True
                            l.Top = 2
                            l.Left = 2
                            itemPanel.Controls.Add(l)

                            Dim nud As New NumericUpDown()
                            nud.Minimum = CDec(0)
                            nud.Maximum = CDec(5)
                            nud.DecimalPlaces = 1
                            nud.Increment = CDec(0.1)
                            nud.Value = CDec(0)
                            nud.Width = 90
                            nud.Top = l.Bottom + 6
                            nud.Left = 2
                            nud.ForeColor = Color.White
                            nud.BackColor = Color.FromArgb(34, 40, 46)
                            nud.Tag = lang

                            AddHandler nud.ValueChanged, AddressOf LanguageNumeric_ValueChanged
                            AddHandler nud.GotFocus, AddressOf LanguageNumeric_GotFocus
                            AddHandler nud.LostFocus, AddressOf LanguageNumeric_LostFocus

                            itemPanel.Controls.Add(nud)

                            Dim placeholder As New Panel()
                            placeholder.AutoSize = True
                            placeholder.Controls.Add(itemPanel)

                            ' ensure enough rows
                            While tlp.RowCount <= r
                                tlp.RowCount += 1
                                tlp.RowStyles.Add(New RowStyle(SizeType.AutoSize))
                            End While

                            tlp.Controls.Add(placeholder, c, r)

                            idx += 1
                        Next

                        FlowLayoutPanelQuestions.Controls.Add(tlp)
                        UpdateKvlistAnswers(tlp, q.QKey)

                    Case "select"
                        Dim cb As New ComboBox()
                        cb.Width = ControlWidth()
                        cb.ForeColor = Color.White
                        cb.BackColor = Color.FromArgb(24, 30, 36)
                        If Not String.IsNullOrWhiteSpace(q.Options) Then
                            cb.Items.AddRange(q.Options.Split({","c}, StringSplitOptions.RemoveEmptyEntries))
                        End If
                        FlowLayoutPanelQuestions.Controls.Add(cb)
                        AddHandler cb.SelectedIndexChanged, Sub(s, ev) If cb.SelectedItem IsNot Nothing Then answers(q.QKey) = cb.SelectedItem.ToString()

                    Case "yesno"
                        Dim cbyn As New ComboBox()
                        cbyn.Width = 120
                        cbyn.Items.AddRange(YESNO_OPTIONS)
                        FlowLayoutPanelQuestions.Controls.Add(cbyn)
                        AddHandler cbyn.SelectedIndexChanged, Sub(s, ev) If cbyn.SelectedItem IsNot Nothing Then answers(q.QKey) = cbyn.SelectedItem.ToString()

                    Case Else
                        Dim tbf As New TextBox()
                        tbf.Width = ControlWidth()
                        tbf.ForeColor = Color.White
                        tbf.BackColor = Color.FromArgb(24, 30, 36)
                        FlowLayoutPanelQuestions.Controls.Add(tbf)
                        AddHandler tbf.LostFocus, Sub(s, ev) answers(q.QKey) = tbf.Text
                End Select

                Dim spacer As New Label()
                spacer.Height = 8
                spacer.AutoSize = False
                FlowLayoutPanelQuestions.Controls.Add(spacer)
            Next
        End If

        ' Navigation buttons
        If stepIndex > 1 Then
            Dim btnPrev As New Button()
            btnPrev.Text = "Previous"
            btnPrev.Width = 120
            AddHandler btnPrev.Click, Sub(s, ev)
                                          currentStep = Math.Max(1, currentStep - 1)
                                          ShowQuestionsForStep(currentStep)
                                      End Sub
            btnPrev.Margin = New Padding(6)
            PanelNav.Controls.Add(btnPrev)
        End If

        Dim nextQuestions = DatabaseHelper.GetQuestionsByStep(stepIndex + 1)
        If nextQuestions IsNot Nothing AndAlso nextQuestions.Count > 0 Then
            Dim btnNext As New Button()
            btnNext.Text = "Next"
            btnNext.Width = 120
            AddHandler btnNext.Click, Sub(s, ev)
                                          currentStep += 1
                                          ShowQuestionsForStep(currentStep)
                                      End Sub
            btnNext.Margin = New Padding(6)
            PanelNav.Controls.Add(btnNext)
        Else
            Dim btnSubmit As New Button()
            btnSubmit.Text = "Submit"
            btnSubmit.Width = 120
            AddHandler btnSubmit.Click, AddressOf ButtonSubmit_Click
            btnSubmit.Margin = New Padding(6)
            btnSubmit.BackColor = Color.FromArgb(102, 204, 255)
            btnSubmit.FlatStyle = FlatStyle.Flat
            btnSubmit.ForeColor = Color.Black
            PanelNav.Controls.Add(btnSubmit)
        End If

        Dim btnCancelNav As New Button()
        btnCancelNav.Text = "Cancel"
        btnCancelNav.Width = 120
        AddHandler btnCancelNav.Click, Sub(s, ev) Me.Close()
        btnCancelNav.Margin = New Padding(6)
        btnCancelNav.ForeColor = Color.White
        btnCancelNav.BackColor = Color.FromArgb(34, 40, 46)
        btnCancelNav.FlatStyle = FlatStyle.Flat
        PanelNav.Controls.Add(btnCancelNav)

        ' right-align spacer
        Dim spacerLabel As New Label()
        spacerLabel.Width = Math.Max(0, PanelNav.Width - PanelNav.Controls.Cast(Of Control)().Sum(Function(c) c.Width + c.Margin.Horizontal))
        PanelNav.Controls.Add(spacerLabel)
    End Sub

    Private Sub LanguageNumeric_ValueChanged(sender As Object, e As EventArgs)
        Dim nud = TryCast(sender, NumericUpDown)
        If nud Is Nothing Then Return

        ' find parent TableLayoutPanel
        Dim tlp = FindParentOfType(nud, GetType(TableLayoutPanel))
        If tlp IsNot Nothing Then
            ' attempt to find the qKey by searching nearby question label text - not ideal but qKey stored via tag not available here
            ' Instead rebuild all kvlist answers across controls and merge into answers dictionary under both 'skills' and 'interests' where applicable
            UpdateAllKvlistAnswers()
        End If
    End Sub

    Private Sub LanguageNumeric_GotFocus(sender As Object, e As EventArgs)
        Dim nud = TryCast(sender, NumericUpDown)
        If nud IsNot Nothing Then
            nud.BackColor = Color.FromArgb(102, 204, 255)
        End If
    End Sub

    Private Sub LanguageNumeric_LostFocus(sender As Object, e As EventArgs)
        Dim nud = TryCast(sender, NumericUpDown)
        If nud IsNot Nothing Then
            nud.BackColor = Color.FromArgb(34, 40, 46)
        End If
    End Sub

    Private Sub UpdateKvlistAnswers(tlp As TableLayoutPanel, qKey As String)
        Try
            Dim dict As New Dictionary(Of String, Double)()
            For Each ctrl As Control In tlp.Controls
                For Each inner As Control In ctrl.Controls
                    For Each child As Control In inner.Controls
                        If TypeOf child Is NumericUpDown Then
                            Dim nud As NumericUpDown = DirectCast(child, NumericUpDown)
                            Dim lang = Convert.ToString(nud.Tag)
                            If Not String.IsNullOrWhiteSpace(lang) Then
                                Dim normalized As Double = CDbl(nud.Value) / 5.0
                                dict(lang) = ExpertEngine.Normalize(normalized)
                            End If
                        End If
                    Next
                Next
            Next
            answers(qKey) = dict
        Catch
        End Try
    End Sub

    Private Sub UpdateAllKvlistAnswers()
        ' Iterate FlowLayoutPanelQuestions controls and update any kvlist-style TableLayoutPanel found
        For Each c As Control In FlowLayoutPanelQuestions.Controls
            If TypeOf c Is TableLayoutPanel Then
                ' find associated question key by checking previous label (assumes label added before)
                Dim idx = FlowLayoutPanelQuestions.Controls.GetChildIndex(c)
                If idx > 0 Then
                    Dim prev = FlowLayoutPanelQuestions.Controls(idx - 1)
                    Dim qKey As String = Nothing
                    If TypeOf prev Is Label Then
                        ' might contain prompt, cannot reliably map to qKey; skip mapping here
                    End If
                End If
                ' attempt to update a generic key name 'skills' if exists
                UpdateKvlistAnswers(DirectCast(c, TableLayoutPanel), "skills")
                UpdateKvlistAnswers(DirectCast(c, TableLayoutPanel), "interests")
            End If
        Next
    End Sub

    Private Function FindParentOfType(child As Control, t As Type) As Control
        Dim p = child.Parent
        While p IsNot Nothing
            If p.GetType() Is t OrElse p.GetType().IsSubclassOf(t) Then Return p
            p = p.Parent
        End While
        Return Nothing
    End Function

    Private Sub ButtonSubmit_Click(sender As Object, e As EventArgs)
        If Not answers.ContainsKey("interests") Then
            MessageBox.Show("Silakan isi minat terlebih dahulu.")
            Return
        End If
        If Not answers.ContainsKey("skills") Then
            MessageBox.Show("Silakan isi skill terlebih dahulu.")
            Return
        End If

        Dim username As String = Environment.UserName
        Dim recs = ExpertEngine.Evaluate(answers)
        ExpertEngine.SaveConsultationToDb(username, answers, recs)

        Dim sb As New StringBuilder()
        sb.AppendLine("Rekomendasi:")
        For Each r In recs
            sb.AppendLine("- " & r)
        Next
        MessageBox.Show(sb.ToString(), "Hasil")
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

End Class