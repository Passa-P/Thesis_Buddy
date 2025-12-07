Imports System.Text
Imports System.Text.Json
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Linq
Imports System.Reflection

Public Partial Class Question_Form

    Private Shared ReadOnly PRODI_OPTIONS As String() = {"TI", "TMJ", "TMD", "Lainnya"}
    Private Shared ReadOnly PS_OPTIONS As String() = {"0.0", "0.2", "0.4", "0.6", "0.8", "1.0"}
    Private Shared ReadOnly WORK_OPTIONS As String() = {"Individu", "Kelompok"}
    Private Shared ReadOnly DOMAIN_OPTIONS As String() = {"Algoritmik", "Desain kreatif", "Perangkat keras"}
    Private Shared ReadOnly METHOD_OPTIONS As String() = {"Experimental", "R&D", "Case Study", "Simulation"}
    Private Shared ReadOnly OUTPUT_OPTIONS As String() = {"Aplikasi", "Game", "IoT Device", "Model AI", "Other"}
    Private Shared ReadOnly YESNO_OPTIONS As String() = {"Ya", "Tidak"}
    Private Shared ReadOnly LANGUAGE_LIST As String() = {"Python", "Java", "C++", "C#", "JavaScript", "PHP", "Kotlin", "Go", "R", "MATLAB"}

    Private Shared ReadOnly THEME_BACKGROUND As Color = Color.FromArgb(9, 12, 23)
    Private Shared ReadOnly THEME_SURFACE As Color = Color.FromArgb(17, 24, 39)
    Private Shared ReadOnly THEME_CARD As Color = Color.FromArgb(28, 38, 57)
    Private Shared ReadOnly THEME_CARD_GLOW As Color = Color.FromArgb(40, 56, 84)
    Private Shared ReadOnly THEME_TEXT As Color = Color.FromArgb(229, 231, 235)
    Private Shared ReadOnly THEME_MUTED_TEXT As Color = Color.FromArgb(148, 163, 184)
    Private Shared ReadOnly THEME_PRIMARY As Color = Color.FromArgb(96, 165, 250)
    Private Shared ReadOnly THEME_DANGER As Color = Color.FromArgb(248, 113, 113)
    Private Shared ReadOnly THEME_BORDER As Color = Color.FromArgb(56, 68, 90)
    Private Shared ReadOnly THEME_INPUT_BG As Color = Color.FromArgb(15, 23, 42)
    Private Shared ReadOnly THEME_INPUT_FOCUS As Color = Color.FromArgb(30, 41, 59)

    Private answers As New Dictionary(Of String, Object)()
    Private currentStep As Integer = 1
    Private questionLayoutHandlerAttached As Boolean = False
    Private questionsPanelCache As FlowLayoutPanel


    Private Sub Question_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.BackColor = THEME_BACKGROUND

        Dim found = Me.Controls.Find("PanelCard", True)
        If found.Length > 0 Then
            Dim panelCard As Panel = DirectCast(found(0), Panel)
            panelCard.Left = Math.Max(20, (Me.ClientSize.Width - panelCard.Width) \ 2)
            panelCard.Top = 30
            panelCard.BackColor = THEME_SURFACE
            panelCard.Padding = New Padding(0, 0, 0, 12)

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
            PictureLogo.Width = 80
            PictureLogo.Height = 80
            PictureLogo.Left = 24
            PictureLogo.Top = 24
            PictureLogo.BackColor = Color.Transparent
        End If

        If LabelTitle IsNot Nothing Then
            LabelTitle.Text = "Sistem Pakar ThesisBuddy"
            LabelTitle.ForeColor = THEME_PRIMARY
            If PictureLogo IsNot Nothing Then
                LabelTitle.Left = PictureLogo.Right + 18
                LabelTitle.Top = PictureLogo.Top
            End If
        End If

        If LabelSubtitle IsNot Nothing Then
            LabelSubtitle.Text = "Rekomendasi Teknologi untuk Skripsimu"
            LabelSubtitle.ForeColor = THEME_MUTED_TEXT
            If LabelTitle IsNot Nothing Then
                LabelSubtitle.Left = LabelTitle.Left
                LabelSubtitle.Top = LabelTitle.Bottom - 4
            End If
        End If

        Dim questionsPanel = ResolveQuestionsPanel()
        If questionsPanel IsNot Nothing Then
            questionsPanel.Padding = New Padding(16)
            questionsPanel.Margin = New Padding(0, 16, 0, 0)
            questionsPanel.AutoScrollMargin = New Size(0, 24)
            questionsPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
            questionsPanel.BackColor = THEME_CARD
            EnableDoubleBuffer(questionsPanel)
        End If

        If PanelNav IsNot Nothing Then
            PanelNav.BackColor = Color.Transparent
            EnableDoubleBuffer(PanelNav)
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

    Private Function ResolveQuestionsPanel() As FlowLayoutPanel
        If questionsPanelCache Is Nothing OrElse questionsPanelCache.IsDisposed Then
            Dim matches = Me.Controls.Find("FlowLayoutPanelQuestions", True)
            If matches.Length > 0 Then
                questionsPanelCache = TryCast(matches(0), FlowLayoutPanel)
            End If
        End If
        Return questionsPanelCache
    End Function

    Private Sub ShowQuestionsForStep(stepIndex As Integer)
        Dim questionsPanel = ResolveQuestionsPanel()
        If questionsPanel Is Nothing Then Return

        questionsPanel.SuspendLayout()
        questionsPanel.Controls.Clear()
        PanelNav.Controls.Clear()

        Dim baseTextColor = THEME_TEXT
        Dim mutedTextColor = THEME_MUTED_TEXT
        Dim primaryColor = THEME_PRIMARY
        Dim accentDanger = THEME_DANGER
        Dim inputMargin = New Padding(0, 0, 0, 10)

        AttachQuestionLayoutHandler()

        Dim questions = DatabaseHelper.GetQuestionsByStep(stepIndex)
        If questions Is Nothing OrElse questions.Count = 0 Then
            Dim lbl As New Label With {
                .Text = "Tidak ada pertanyaan di langkah ini.",
                .ForeColor = mutedTextColor,
                .AutoSize = True,
                .Margin = New Padding(0, 10, 0, 0)
            }
            questionsPanel.Controls.Add(lbl)
        Else
            For i As Integer = 0 To questions.Count - 1
                Dim card = CreateQuestionCard(questions(i), baseTextColor, mutedTextColor, primaryColor, inputMargin, i + 1)
                questionsPanel.Controls.Add(card)
            Next
        End If

        questionsPanel.ResumeLayout()
        ApplyQuestionCardWidth()

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
            btnPrev.ForeColor = baseTextColor
            btnPrev.BackColor = THEME_INPUT_BG
            btnPrev.FlatAppearance.BorderColor = THEME_BORDER
            btnPrev.FlatAppearance.BorderSize = 1
            btnPrev.FlatAppearance.MouseOverBackColor = THEME_INPUT_FOCUS
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
            btnNext.FlatAppearance.MouseOverBackColor = Color.FromArgb(79, 139, 237)
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
        btnCancelNav.BackColor = THEME_INPUT_BG
        btnCancelNav.FlatStyle = FlatStyle.Flat
        btnCancelNav.FlatAppearance.BorderColor = accentDanger
        btnCancelNav.FlatAppearance.BorderSize = 1
        btnCancelNav.FlatAppearance.MouseOverBackColor = THEME_INPUT_FOCUS
        PanelNav.Controls.Add(btnCancelNav)
    End Sub

    Private Sub AttachQuestionLayoutHandler()
        Dim questionsPanel = ResolveQuestionsPanel()
        If questionLayoutHandlerAttached OrElse questionsPanel Is Nothing Then Return
        AddHandler questionsPanel.SizeChanged, Sub(sender As Object, e As EventArgs) ApplyQuestionCardWidth()
        questionLayoutHandlerAttached = True
    End Sub

    Private Sub ApplyQuestionCardWidth()
        Dim questionsPanel = ResolveQuestionsPanel()
        If questionsPanel Is Nothing Then Return
        Dim availableWidth = questionsPanel.ClientSize.Width - questionsPanel.Padding.Horizontal
        If availableWidth <= 0 Then Return
        For Each ctrl As Control In questionsPanel.Controls
            If TypeOf ctrl Is Panel AndAlso ctrl.Tag IsNot Nothing AndAlso ctrl.Tag.ToString().StartsWith("question-card") Then
                ctrl.Width = Math.Max(availableWidth, 200)
            End If
        Next
    End Sub

    Private Sub EnableDoubleBuffer(ctrl As Control)
        If ctrl Is Nothing Then Return
        Dim prop = GetType(Control).GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        If prop IsNot Nothing Then
            prop.SetValue(ctrl, True, Nothing)
        End If
    End Sub

    Private Function CreateQuestionCard(question As QuestionModel, baseTextColor As Color, mutedTextColor As Color, primaryColor As Color, inputMargin As Padding, questionNumber As Integer) As Control
        Dim card As New Panel()
        card.Tag = $"question-card|{question.QKey}"
        card.BackColor = THEME_CARD
        card.Padding = New Padding(18)
        card.Margin = New Padding(0, 0, 0, 14)
        card.AutoSize = True
        card.AutoSizeMode = AutoSizeMode.GrowAndShrink
        card.MinimumSize = New Size(0, 90)
        card.BorderStyle = BorderStyle.None
        AddHandler card.Paint, AddressOf Card_Paint
        EnableDoubleBuffer(card)

        Dim container As New TableLayoutPanel()
        container.ColumnCount = 1
        container.RowCount = 3
        container.Dock = DockStyle.Fill
        container.AutoSize = True
        container.AutoSizeMode = AutoSizeMode.GrowAndShrink
        container.Padding = New Padding(0)
        container.Margin = New Padding(0)
        container.RowStyles.Clear()
        container.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        container.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        container.RowStyles.Add(New RowStyle(SizeType.AutoSize))

        Dim header As New FlowLayoutPanel()
        header.AutoSize = True
        header.Margin = New Padding(0, 0, 0, 6)
        header.Padding = New Padding(0)
        header.WrapContents = False

        Dim questionLabel = questionNumber.ToString("00")
        Dim numberBadge As New Label()
        numberBadge.Text = $"Q{questionLabel}"
        numberBadge.AutoSize = True
        numberBadge.Padding = New Padding(10, 4, 10, 4)
        numberBadge.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        numberBadge.ForeColor = Color.White
        numberBadge.BackColor = Color.FromArgb(37, 99, 235)
        numberBadge.Margin = New Padding(0, 0, 8, 0)
        header.Controls.Add(numberBadge)

        Dim keyBadge As New Label()
        keyBadge.Text = If(String.IsNullOrWhiteSpace(question.QKey), "QUESTION", question.QKey)
        keyBadge.AutoSize = True
        keyBadge.Padding = New Padding(8, 4, 8, 4)
        keyBadge.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        keyBadge.ForeColor = primaryColor
        keyBadge.BackColor = Color.FromArgb(32, 48, 78)
        keyBadge.Margin = New Padding(0, 0, 8, 0)
        header.Controls.Add(keyBadge)

        Dim categoryText = If(String.IsNullOrWhiteSpace(question.Category), "General", question.Category)
        Dim categoryBadge As New Label()
        categoryBadge.Text = categoryText
        categoryBadge.AutoSize = True
        categoryBadge.Padding = New Padding(8, 4, 8, 4)
        categoryBadge.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        categoryBadge.ForeColor = mutedTextColor
        categoryBadge.BackColor = Color.FromArgb(36, 47, 72)
        header.Controls.Add(categoryBadge)

        Dim promptText As String = question.Prompt
        If Not String.IsNullOrWhiteSpace(question.QKey) Then
            promptText = $"[{question.QKey}] {question.Prompt}"
        End If

        Dim lbl As New Label()
        lbl.Text = promptText
        lbl.ForeColor = baseTextColor
        lbl.AutoSize = True
        lbl.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        lbl.Margin = New Padding(0, 0, 0, 14)
        lbl.MaximumSize = New Size(900, 0)

        Dim inputControl = BuildInputControl(question, baseTextColor, primaryColor, inputMargin)

        container.Controls.Add(header, 0, 0)
        container.Controls.Add(lbl, 0, 1)
        container.Controls.Add(inputControl, 0, 2)

        card.Controls.Add(container)

        Return card
    End Function

    Private Function BuildInputControl(question As QuestionModel, baseTextColor As Color, primaryColor As Color, inputMargin As Padding) As Control
        Dim typ As String = If(question.QType, String.Empty).ToLowerInvariant()
        Dim preferredWidth = ComputeInputWidth()

        Select Case typ
            Case "text", "string"
                Dim tb As New TextBox()
                tb.Width = preferredWidth
                tb.MinimumSize = New Size(preferredWidth, tb.Height)
                tb.Anchor = AnchorStyles.Left Or AnchorStyles.Right
                tb.ForeColor = baseTextColor
                tb.BackColor = THEME_INPUT_BG
                tb.BorderStyle = BorderStyle.FixedSingle
                tb.Margin = inputMargin
                AddHandler tb.LostFocus, Sub(s, ev) answers(question.QKey) = tb.Text
                Return WrapInput(tb)

            Case "number"
                Dim tbn As New TextBox()
                tbn.Width = Math.Max(200, preferredWidth \ 2)
                tbn.MinimumSize = New Size(Math.Max(200, preferredWidth \ 2), tbn.Height)
                tbn.Anchor = AnchorStyles.Left Or AnchorStyles.Right
                tbn.ForeColor = baseTextColor
                tbn.BackColor = THEME_INPUT_BG
                tbn.BorderStyle = BorderStyle.FixedSingle
                tbn.Margin = inputMargin
                AddHandler tbn.LostFocus, Sub(s, ev)
                                              Dim d As Double
                                              If Double.TryParse(tbn.Text, d) Then
                                                  answers(question.QKey) = d
                                              Else
                                                  answers(question.QKey) = 0.0
                                              End If
                                          End Sub
                Return WrapInput(tbn)

            Case "kvlist"
                Dim cols As Integer = 3
                Dim tlp As New TableLayoutPanel()
                tlp.AutoSize = True
                tlp.ColumnCount = cols
                tlp.RowCount = CInt(Math.Ceiling(LANGUAGE_LIST.Length / CDbl(cols)))
                tlp.Padding = New Padding(0)
                tlp.Margin = inputMargin
                tlp.GrowStyle = TableLayoutPanelGrowStyle.AddRows
                tlp.Tag = $"kvlist|{question.QKey}"
                tlp.MaximumSize = New Size(preferredWidth, 0)

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
                    nud.BackColor = THEME_INPUT_BG
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

                UpdateKvlistAnswers(tlp, question.QKey)
                Return WrapInput(tlp)

            Case "select"
                Dim separators = New String() {",", "|", ";", Environment.NewLine}
                Dim rawOptions = If(question.Options, String.Empty)
                Dim optionList = rawOptions.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                Dim cleaned = optionList.Select(Function(opt) opt.Trim()).Where(Function(opt) Not String.IsNullOrWhiteSpace(opt)).ToList()
                If cleaned.Count = 0 Then Exit Select
                Dim radioGroup = BuildRadioOptionGroup(question.QKey, cleaned, baseTextColor, primaryColor, inputMargin, preferredWidth)
                Return radioGroup

            Case "yesno"
                Dim yesNoList = YESNO_OPTIONS.Select(Function(opt) opt).ToList()
                Dim yesNoGroup = BuildRadioOptionGroup(question.QKey, yesNoList, baseTextColor, primaryColor, inputMargin, Math.Max(260, preferredWidth \ 2))
                Return yesNoGroup

            Case Else
                Dim tbf As New TextBox()
                tbf.Width = preferredWidth
                tbf.MinimumSize = New Size(preferredWidth, tbf.Height)
                tbf.Anchor = AnchorStyles.Left Or AnchorStyles.Right
                tbf.ForeColor = baseTextColor
                tbf.BackColor = THEME_INPUT_BG
                tbf.BorderStyle = BorderStyle.FixedSingle
                tbf.Margin = inputMargin
                AddHandler tbf.LostFocus, Sub(s, ev) answers(question.QKey) = tbf.Text
                Return WrapInput(tbf)
        End Select

        Dim fallback As New TextBox()
        fallback.Width = preferredWidth
        fallback.MinimumSize = New Size(preferredWidth, fallback.Height)
        fallback.Anchor = AnchorStyles.Left Or AnchorStyles.Right
        fallback.ForeColor = baseTextColor
        fallback.BackColor = THEME_INPUT_BG
        fallback.BorderStyle = BorderStyle.FixedSingle
        fallback.Margin = inputMargin
        AddHandler fallback.LostFocus, Sub(s, ev) answers(question.QKey) = fallback.Text
        Return WrapInput(fallback)
    End Function

    Private Function WrapInput(control As Control) As Control
        If TypeOf control Is TableLayoutPanel Then
            control.Margin = New Padding(0)
            Return control
        End If

        Dim wrapper As New Panel()
        wrapper.AutoSize = True
        wrapper.AutoSizeMode = AutoSizeMode.GrowAndShrink
        wrapper.Padding = New Padding(0)
        wrapper.Margin = New Padding(0)
        wrapper.Controls.Add(control)
        Return wrapper
    End Function

    Private Function BuildRadioOptionGroup(qKey As String, options As IEnumerable(Of String), baseTextColor As Color, primaryColor As Color, inputMargin As Padding, preferredWidth As Integer) As Control
        Dim optionItems = options?.Where(Function(opt) Not String.IsNullOrWhiteSpace(opt)).Select(Function(opt) opt.Trim()).ToList()
        Dim group As New FlowLayoutPanel()
        group.AutoSize = True
        group.AutoSizeMode = AutoSizeMode.GrowAndShrink
        group.FlowDirection = FlowDirection.LeftToRight
        group.WrapContents = True
        group.Padding = New Padding(0)
        group.Margin = inputMargin
        group.MaximumSize = New Size(preferredWidth, 0)

        Dim existingValue As String = Nothing
        If answers.ContainsKey(qKey) Then
            existingValue = Convert.ToString(answers(qKey))
        End If

        If optionItems Is Nothing OrElse optionItems.Count = 0 Then
            Return group
        End If

        For Each opt In optionItems
            Dim radio As New RadioButton()
            radio.Appearance = Appearance.Button
            radio.AutoSize = True
            radio.Text = opt
            radio.Cursor = Cursors.Hand
            radio.FlatStyle = FlatStyle.Flat
            radio.FlatAppearance.BorderSize = 1
            radio.FlatAppearance.BorderColor = THEME_BORDER
            radio.FlatAppearance.CheckedBackColor = Color.Transparent
            radio.FlatAppearance.MouseOverBackColor = THEME_INPUT_FOCUS
            radio.Margin = New Padding(0, 0, 10, 10)
            radio.Padding = New Padding(14, 8, 14, 8)
            radio.TextAlign = ContentAlignment.MiddleCenter
            radio.Checked = String.Equals(existingValue, opt, StringComparison.OrdinalIgnoreCase)
            UpdateRadioVisualState(radio, radio.Checked, primaryColor, baseTextColor)
            AddHandler radio.CheckedChanged,
                Sub(sender, args)
                    Dim rb = DirectCast(sender, RadioButton)
                    UpdateRadioVisualState(rb, rb.Checked, primaryColor, baseTextColor)
                    If rb.Checked Then
                        answers(qKey) = rb.Text
                    End If
                End Sub
            group.Controls.Add(radio)
        Next

        Return group
    End Function

    Private Sub UpdateRadioVisualState(radio As RadioButton, isChecked As Boolean, primaryColor As Color, baseTextColor As Color)
        Dim borderColor = THEME_BORDER
        radio.BackColor = If(isChecked, primaryColor, THEME_INPUT_BG)
        radio.FlatAppearance.BorderColor = If(isChecked, primaryColor, borderColor)
        radio.ForeColor = If(isChecked, Color.FromArgb(15, 23, 42), baseTextColor)
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
            nud.BackColor = THEME_INPUT_FOCUS
        End If
    End Sub

    Private Sub LanguageNumeric_LostFocus(sender As Object, e As EventArgs)
        Dim nud = TryCast(sender, NumericUpDown)
        If nud IsNot Nothing Then
            nud.BackColor = THEME_INPUT_BG
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
        Dim questionsPanel = ResolveQuestionsPanel()
        If questionsPanel Is Nothing Then Return
        For Each tlp As TableLayoutPanel In EnumerateDescendants(Of TableLayoutPanel)(questionsPanel)
            Dim tagValue = Convert.ToString(tlp.Tag)
            If String.IsNullOrWhiteSpace(tagValue) OrElse Not tagValue.StartsWith("kvlist|") Then Continue For
            Dim qKey = tagValue.Substring(7)
            UpdateKvlistAnswers(tlp, qKey)
        Next
    End Sub

    Private Iterator Function EnumerateDescendants(Of T As Control)(parent As Control) As IEnumerable(Of T)
        If parent Is Nothing Then Return
        For Each child As Control In parent.Controls
            If TypeOf child Is T Then
                Yield DirectCast(child, T)
            End If
            For Each nested In EnumerateDescendants(Of T)(child)
                Yield nested
            Next
        Next
    End Function

    Private Function ComputeInputWidth() As Integer
        Dim questionsPanel = ResolveQuestionsPanel()
        If questionsPanel Is Nothing Then Return 420
        Dim width = questionsPanel.ClientSize.Width - questionsPanel.Padding.Horizontal - 60
        Return Math.Max(width, 220)
    End Function

    Private Sub Card_Paint(sender As Object, e As PaintEventArgs)
        Dim card = TryCast(sender, Panel)
        If card Is Nothing Then Return
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        Dim rect = card.ClientRectangle
        If rect.Width <= 0 OrElse rect.Height <= 0 Then Return
        rect.Inflate(-1, -1)
        Using path = BuildRoundedPath(rect, 16)
            Using fillBrush As New LinearGradientBrush(rect, THEME_CARD_GLOW, THEME_CARD, LinearGradientMode.Vertical)
                e.Graphics.FillPath(fillBrush, path)
            End Using

            Using borderPen As New Pen(THEME_BORDER)
                e.Graphics.DrawPath(borderPen, path)
            End Using
        End Using
    End Sub

    Private Function BuildRoundedPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim diameter = radius * 2
        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90)
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90)
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90)
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90)
        path.CloseFigure()
        Return path
    End Function

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

    Private Sub PictureLogo_Click(sender As Object, e As EventArgs) Handles PictureLogo.Click

    End Sub
End Class