Imports System.Text
Imports System.Text.Json
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Linq

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


    Private Sub Question_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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


        If PictureLogo IsNot Nothing Then
            PictureLogo.SizeMode = PictureBoxSizeMode.Zoom
            PictureLogo.Width = 280
            PictureLogo.Height = 360
            PictureLogo.Left = (PanelLeft.Width - PictureLogo.Width) \ 2
            PictureLogo.Top = 40
        End If

        If LabelTitle IsNot Nothing Then
            LabelTitle.Text = "Sistem Pakar ThesisBuddy"
            LabelTitle.ForeColor = Color.FromArgb(59, 130, 246)
        End If

        If LabelSubtitle IsNot Nothing Then
            LabelSubtitle.Text = "Rekomendasi Teknologi untuk Skripsimu"
            LabelSubtitle.ForeColor = Color.FromArgb(107, 114, 128)
        End If

        If FlowLayoutPanelQuestions IsNot Nothing Then
            FlowLayoutPanelQuestions.Padding = New Padding(16)
            FlowLayoutPanelQuestions.Margin = New Padding(0, 16, 0, 0)
            FlowLayoutPanelQuestions.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        End If

        Try
            Dim bc = Me.Controls.Find("ButtonCancel", True)
            If bc.Length > 0 Then
                bc(0).Visible = False
            End If
        Catch
        End Try

        DatabaseHelper.EnsureQuestionsTable()
        DatabaseHelper.SeedMcClellandQuestionnaire()
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

        Dim baseTextColor = Color.FromArgb(31, 41, 55)
        Dim mutedTextColor = Color.FromArgb(107, 114, 128)
        Dim primaryColor = Color.FromArgb(59, 130, 246)
        Dim accentDanger = Color.FromArgb(239, 68, 68)
        Dim inputMargin = New Padding(0, 0, 0, 12)
        Dim questions = DatabaseHelper.GetQuestionsByStep(stepIndex)
        If questions Is Nothing OrElse questions.Count = 0 Then
            Dim lbl As New Label With {.Text = "Tidak ada pertanyaan di langkah ini.", .ForeColor = Color.LightGray, .AutoSize = True}
            FlowLayoutPanelQuestions.Controls.Add(lbl)
        Else
            For Each q In questions
                Dim lbl As New Label()
                Dim promptText As String = q.Prompt
                If Not String.IsNullOrWhiteSpace(q.QKey) Then
                    promptText = $"[{q.QKey}] {q.Prompt}"
                End If
                lbl.Text = promptText
                lbl.ForeColor = baseTextColor
                lbl.AutoSize = False
                lbl.Width = ControlWidth()
                lbl.Height = 24
                lbl.Font = New Font("Segoe UI", 10.5F, FontStyle.Regular)
                lbl.Margin = New Padding(0, 0, 0, 4)
                FlowLayoutPanelQuestions.Controls.Add(lbl)

                Dim typ As String = ""
                If q.QType IsNot Nothing Then typ = q.QType.ToLower()

                Select Case typ
                    Case "text", "string"
                        Dim tb As New TextBox()
                        tb.Width = ControlWidth()
                        tb.ForeColor = baseTextColor
                        tb.BackColor = Color.White
                        tb.BorderStyle = BorderStyle.FixedSingle
                        tb.Margin = inputMargin
                        FlowLayoutPanelQuestions.Controls.Add(tb)
                        AddHandler tb.LostFocus, Sub(s, ev) answers(q.QKey) = tb.Text

                    Case "number"
                        Dim tbn As New TextBox()
                        tbn.Width = ControlWidth()
                        tbn.ForeColor = baseTextColor
                        tbn.BackColor = Color.White
                        tbn.BorderStyle = BorderStyle.FixedSingle
                        tbn.Margin = inputMargin
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

                        Dim cols As Integer = 3
                        Dim rows As Integer = CInt(Math.Ceiling(LANGUAGE_LIST.Length / CDbl(cols)))
                        Dim tlp As New TableLayoutPanel()
                        tlp.AutoSize = True
                        tlp.ColumnCount = cols
                        tlp.RowCount = rows
                        tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
                        tlp.Width = ControlWidth()
                        tlp.Padding = New Padding(0)
                        tlp.Margin = New Padding(0, 4, 0, 12)

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
                            l.ForeColor = baseTextColor
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
                            nud.ForeColor = baseTextColor
                            nud.BackColor = Color.White
                            nud.Tag = lang

                            AddHandler nud.ValueChanged, AddressOf LanguageNumeric_ValueChanged
                            AddHandler nud.GotFocus, AddressOf LanguageNumeric_GotFocus
                            AddHandler nud.LostFocus, AddressOf LanguageNumeric_LostFocus

                            itemPanel.Controls.Add(nud)

                            Dim placeholder As New Panel()
                            placeholder.AutoSize = True
                            placeholder.Controls.Add(itemPanel)


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
                        cb.ForeColor = baseTextColor
                        cb.BackColor = Color.White
                        cb.DropDownStyle = ComboBoxStyle.DropDownList
                        cb.FlatStyle = FlatStyle.Flat
                        cb.Margin = inputMargin
                        If Not String.IsNullOrWhiteSpace(q.Options) Then
                            cb.Items.AddRange(q.Options.Split({","c}, StringSplitOptions.RemoveEmptyEntries))
                        End If
                        FlowLayoutPanelQuestions.Controls.Add(cb)
                        AddHandler cb.SelectedIndexChanged, Sub(s, ev) If cb.SelectedItem IsNot Nothing Then answers(q.QKey) = cb.SelectedItem.ToString()

                    Case "yesno"
                        Dim cbyn As New ComboBox()
                        cbyn.Width = ControlWidth()
                        cbyn.DropDownStyle = ComboBoxStyle.DropDownList
                        cbyn.Items.AddRange(YESNO_OPTIONS)
                        cbyn.ForeColor = baseTextColor
                        cbyn.BackColor = Color.White
                        cbyn.FlatStyle = FlatStyle.Flat
                        cbyn.Margin = inputMargin
                        If answers.ContainsKey(q.QKey) Then
                            Dim existing = Convert.ToString(answers(q.QKey))
                            If Not String.IsNullOrWhiteSpace(existing) AndAlso cbyn.Items.Contains(existing) Then
                                cbyn.SelectedItem = existing
                            End If
                        End If
                        FlowLayoutPanelQuestions.Controls.Add(cbyn)
                        AddHandler cbyn.SelectedIndexChanged, Sub(s, ev) If cbyn.SelectedItem IsNot Nothing Then answers(q.QKey) = cbyn.SelectedItem.ToString()

                    Case Else
                        Dim tbf As New TextBox()
                        tbf.Width = ControlWidth()
                        tbf.ForeColor = baseTextColor
                        tbf.BackColor = Color.White
                        tbf.BorderStyle = BorderStyle.FixedSingle
                        tbf.Margin = inputMargin
                        FlowLayoutPanelQuestions.Controls.Add(tbf)
                        AddHandler tbf.LostFocus, Sub(s, ev) answers(q.QKey) = tbf.Text
                End Select

                Dim spacer As New Label()
                spacer.Height = 4
                spacer.AutoSize = False
                FlowLayoutPanelQuestions.Controls.Add(spacer)
            Next
        End If


        If stepIndex > 1 Then
            Dim btnPrev As New Button()
            btnPrev.Text = "Previous"
            btnPrev.Width = 130
            AddHandler btnPrev.Click, Sub(s, ev)
                                          currentStep = Math.Max(1, currentStep - 1)
                                          ShowQuestionsForStep(currentStep)
                                      End Sub
            btnPrev.Height = 42
            btnPrev.Margin = New Padding(0, 0, 10, 0)
            btnPrev.FlatStyle = FlatStyle.Flat
            btnPrev.ForeColor = mutedTextColor
            btnPrev.BackColor = Color.White
            btnPrev.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219)
            btnPrev.FlatAppearance.BorderSize = 1
            PanelNav.Controls.Add(btnPrev)
        End If

        Dim nextQuestions = DatabaseHelper.GetQuestionsByStep(stepIndex + 1)
        If nextQuestions IsNot Nothing AndAlso nextQuestions.Count > 0 Then
            Dim btnNext As New Button()
            btnNext.Text = "Next"
            btnNext.Width = 130
            AddHandler btnNext.Click, Sub(s, ev)
                                          currentStep += 1
                                          ShowQuestionsForStep(currentStep)
                                      End Sub
            btnNext.Height = 42
            btnNext.Margin = New Padding(0, 0, 10, 0)
            btnNext.FlatStyle = FlatStyle.Flat
            btnNext.ForeColor = Color.White
            btnNext.BackColor = primaryColor
            btnNext.FlatAppearance.BorderSize = 0
            PanelNav.Controls.Add(btnNext)
        Else
            Dim btnSubmit As New Button()
            btnSubmit.Text = "Submit"
            btnSubmit.Width = 130
            AddHandler btnSubmit.Click, AddressOf ButtonSubmit_Click
            btnSubmit.Height = 42
            btnSubmit.Margin = New Padding(0, 0, 10, 0)
            btnSubmit.BackColor = primaryColor
            btnSubmit.FlatStyle = FlatStyle.Flat
            btnSubmit.ForeColor = Color.White
            btnSubmit.FlatAppearance.BorderSize = 0
            PanelNav.Controls.Add(btnSubmit)
        End If

        Dim btnCancelNav As New Button()
        btnCancelNav.Text = "Cancel"
        btnCancelNav.Width = 130
        AddHandler btnCancelNav.Click, Sub(s, ev) Me.Close()
        btnCancelNav.Height = 42
        btnCancelNav.Margin = New Padding(0)
        btnCancelNav.ForeColor = accentDanger
        btnCancelNav.BackColor = Color.White
        btnCancelNav.FlatStyle = FlatStyle.Flat
        btnCancelNav.FlatAppearance.BorderColor = accentDanger
        btnCancelNav.FlatAppearance.BorderSize = 1
        PanelNav.Controls.Add(btnCancelNav)
    End Sub

    Private Sub LanguageNumeric_ValueChanged(sender As Object, e As EventArgs)
        Dim nud = TryCast(sender, NumericUpDown)
        If nud Is Nothing Then Return


        Dim tlp = FindParentOfType(nud, GetType(TableLayoutPanel))
        If tlp IsNot Nothing Then

            UpdateAllKvlistAnswers()
        End If
    End Sub

    Private Sub LanguageNumeric_GotFocus(sender As Object, e As EventArgs)
        Dim nud = TryCast(sender, NumericUpDown)
        If nud IsNot Nothing Then
            nud.BackColor = Color.FromArgb(219, 234, 254)
        End If
    End Sub

    Private Sub LanguageNumeric_LostFocus(sender As Object, e As EventArgs)
        Dim nud = TryCast(sender, NumericUpDown)
        If nud IsNot Nothing Then
            nud.BackColor = Color.White
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
        For Each c As Control In FlowLayoutPanelQuestions.Controls
            If TypeOf c Is TableLayoutPanel Then

                Dim idx = FlowLayoutPanelQuestions.Controls.GetChildIndex(c)
                If idx > 0 Then
                    Dim prev = FlowLayoutPanelQuestions.Controls(idx - 1)
                    Dim qKey As String = Nothing
                    If TypeOf prev Is Label Then

                    End If
                End If

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
        Dim questionBank = DatabaseHelper.GetAllActiveQuestions()
        If questionBank Is Nothing OrElse questionBank.Count = 0 Then
            MessageBox.Show("Daftar pertanyaan belum tersedia. Pastikan koneksi database aktif.")
            Return
        End If

        Dim unanswered = questionBank.Where(Function(q) q IsNot Nothing AndAlso q.Active AndAlso (Not answers.ContainsKey(q.QKey) OrElse String.IsNullOrWhiteSpace(Convert.ToString(answers(q.QKey))))).ToList()
        If unanswered.Count > 0 Then
            Dim firstMissing = unanswered.First()
            Dim keyInfo = If(firstMissing IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(firstMissing.QKey), firstMissing.QKey, "pertanyaan belum terjawab")
            MessageBox.Show($"Masih ada {unanswered.Count} pertanyaan yang belum dijawab. Contoh: {keyInfo}", "Lengkapi Jawaban", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim username As String = Environment.UserName
        Dim profile = ExpertEngine.Evaluate(answers, questionBank)
        ExpertEngine.SaveConsultationToDb(username, answers, profile)

        Dim sb As New StringBuilder()
        sb.AppendLine($"Kategori Dominan: {profile.PrimaryCategory}")
        sb.AppendLine()
        sb.AppendLine("Skor Certainty Factor:")
        For Each kvp In profile.Scores
            sb.AppendLine($"- {kvp.Key}: {kvp.Value:P1}")
        Next

        sb.AppendLine()
        sb.AppendLine("Rekomendasi Judul Skripsi:")
        For Each rec In profile.Recommendations
            sb.AppendLine("- " & rec)
        Next

        MessageBox.Show(sb.ToString(), "Profil Motivasi")
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

End Class