Imports System.Drawing.Drawing2D
Imports System.IO

Public Class Login

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DatabaseHelper.EnsureDefaultAdminUser()
        DatabaseHelper.EnsureQuestionsTable()
        DatabaseHelper.SeedMcClellandQuestionnaire()
        ' locate the panel by name to avoid designer sync issues
        Dim found = Me.Controls.Find("PanelCard", True)
        If found.Length > 0 Then
            Dim panelCard As Panel = DirectCast(found(0), Panel)

            ' If a left branding panel exists, place the card to the right of it with a margin
            Dim leftFound = Me.Controls.Find("PanelLeft", True)
            If leftFound.Length > 0 Then
                Dim panelLeft As Panel = DirectCast(leftFound(0), Panel)
                Dim margin As Integer = 30
                panelCard.Left = panelLeft.Right + margin
                ' vertically center within the form
                panelCard.Top = (Me.ClientSize.Height - panelCard.Height) \ 2
            Else
                ' fallback to centering the card in the form
                panelCard.Left = (Me.ClientSize.Width - panelCard.Width) \ 2
                panelCard.Top = (Me.ClientSize.Height - panelCard.Height) \ 2
            End If

            ' Rounded corners for card
            Try
                Dim r As Integer = 16
                Dim rect As Rectangle = panelCard.ClientRectangle
                Dim path As New GraphicsPath()
                path.AddArc(rect.X, rect.Y, r, r, 180, 90)
                path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90)
                path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90)
                path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90)
                path.CloseFigure()
                panelCard.Region = New Region(path)
            Catch ex As Exception
                ' ignore on failure
            End Try
        End If

        ' Placeholders
        SetPlaceholder(TextBoxUsername, "Username")
        SetPlaceholderPassword(TextBoxPassword, "Password")

        ' Initial button state
        ButtonLogin.FlatAppearance.BorderColor = Color.FromArgb(3, 58, 115)
        ButtonRegister.FlatAppearance.BorderColor = Color.FromArgb(3, 58, 115)

        ' Load logo into PictureLogo if image file exists (place logo file in project output folder)
        Try
            Dim candidates = New String() {Path.Combine(Application.StartupPath, "thesisbuddy_logo.png"), Path.Combine(Application.StartupPath, "logo.png"), Path.Combine(Application.StartupPath, "assets", "logo.png")}
            For Each p As String In candidates
                If File.Exists(p) Then
                    ' load a copy to avoid locking the original file
                    Using src As Image = Image.FromFile(p)
                        PictureLogo.Image = New Bitmap(src)
                    End Using
                    PictureLogo.SizeMode = PictureBoxSizeMode.Zoom
                    Exit For
                End If
            Next
        Catch ex As Exception
            ' ignore load errors
        End Try
    End Sub

    Private Sub SetPlaceholder(tb As TextBox, placeholder As String)
        If String.IsNullOrEmpty(tb.Text) Then
            tb.Text = placeholder
            tb.ForeColor = Color.Gray
        End If
        RemoveHandler tb.GotFocus, AddressOf RemovePlaceholder
        RemoveHandler tb.LostFocus, AddressOf ApplyPlaceholder
        AddHandler tb.GotFocus, AddressOf RemovePlaceholder
        AddHandler tb.LostFocus, AddressOf ApplyPlaceholder
    End Sub

    Private Sub SetPlaceholderPassword(tb As TextBox, placeholder As String)
        If String.IsNullOrEmpty(tb.Text) Then
            tb.Text = placeholder
            tb.ForeColor = Color.Gray
            tb.PasswordChar = ControlChars.NullChar
        End If
        RemoveHandler tb.GotFocus, AddressOf RemovePasswordPlaceholder
        RemoveHandler tb.LostFocus, AddressOf ApplyPasswordPlaceholder
        AddHandler tb.GotFocus, AddressOf RemovePasswordPlaceholder
        AddHandler tb.LostFocus, AddressOf ApplyPasswordPlaceholder
    End Sub

    Private Sub RemovePlaceholder(sender As Object, e As EventArgs)
        Dim tb = DirectCast(sender, TextBox)
        If tb.ForeColor = Color.Gray Then
            tb.Text = String.Empty
            tb.ForeColor = Color.Black
        End If
    End Sub

    Private Sub ApplyPlaceholder(sender As Object, e As EventArgs)
        Dim tb = DirectCast(sender, TextBox)
        If String.IsNullOrWhiteSpace(tb.Text) Then
            tb.Text = tb.Tag
        End If
    End Sub

    Private Sub RemovePasswordPlaceholder(sender As Object, e As EventArgs)
        Dim tb = DirectCast(sender, TextBox)
        If tb.ForeColor = Color.Gray Then
            tb.Text = String.Empty
            tb.ForeColor = Color.Black
            tb.PasswordChar = "●"c
        End If
    End Sub

    Private Sub ApplyPasswordPlaceholder(sender As Object, e As EventArgs)
        Dim tb = DirectCast(sender, TextBox)
        If String.IsNullOrWhiteSpace(tb.Text) Then
            tb.Text = "Password"
            tb.ForeColor = Color.Gray
            tb.PasswordChar = ControlChars.NullChar
        End If
    End Sub

    Private Sub ButtonLogin_Click(sender As Object, e As EventArgs) Handles ButtonLogin.Click
        Dim username As String = TextBoxUsername.Text.Trim()
        Dim password As String = TextBoxPassword.Text

        ' ignore placeholders
        If TextBoxUsername.ForeColor = Color.Gray Then username = String.Empty
        If TextBoxPassword.ForeColor = Color.Gray Then password = String.Empty

        If String.IsNullOrEmpty(username) Or String.IsNullOrEmpty(password) Then
            MessageBox.Show("Please enter username and password.")
            Return
        End If

        Try
            If DatabaseHelper.LoginUser(username, password) Then
                Dim role = DatabaseHelper.GetUserRole(username)
                If String.Equals(role, "admin", StringComparison.OrdinalIgnoreCase) Then
                    Dim adminForm As New Admin()
                    adminForm.Show()
                Else
                    Dim mainForm As New Main_Menu()
                    mainForm.Show()
                End If
                Me.Hide()
            Else
                MessageBox.Show("Invalid username or password.")
            End If
        Catch ex As Exception
            MessageBox.Show("Database error: " & ex.Message)
        End Try
    End Sub

    Private Sub ButtonRegister_Click(sender As Object, e As EventArgs) Handles ButtonRegister.Click
        Dim reg As New Register()
        reg.ShowDialog()
    End Sub

    Private Sub CheckBoxShow_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShow.CheckedChanged
        If TextBoxPassword.ForeColor = Color.Gray Then
            ' placeholder visible, ignore
            Return
        End If
        If CheckBoxShow.Checked Then
            TextBoxPassword.PasswordChar = ControlChars.NullChar
        Else
            TextBoxPassword.PasswordChar = "●"c
        End If
    End Sub

    Private Sub TextBoxes_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxUsername.KeyDown, TextBoxPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            ButtonLogin.PerformClick()
        End If
    End Sub

    Private Sub ButtonLogin_MouseEnter(sender As Object, e As EventArgs) Handles ButtonLogin.MouseEnter
        ButtonLogin.FlatAppearance.BorderColor = Color.White
        ButtonLogin.BackColor = Color.White
        ButtonLogin.ForeColor = Color.FromArgb(10, 20, 26)
    End Sub

    Private Sub ButtonLogin_MouseLeave(sender As Object, e As EventArgs) Handles ButtonLogin.MouseLeave
        ButtonLogin.FlatAppearance.BorderColor = Color.White
        ButtonLogin.BackColor = Color.FromArgb(24, 30, 36)
        ButtonLogin.ForeColor = Color.White
    End Sub

    Private Sub ButtonRegister_MouseEnter(sender As Object, e As EventArgs) Handles ButtonRegister.MouseEnter
        ButtonRegister.FlatAppearance.BorderColor = Color.White
        ButtonRegister.BackColor = Color.White
        ButtonRegister.ForeColor = Color.FromArgb(10, 20, 26)
    End Sub

    Private Sub ButtonRegister_MouseLeave(sender As Object, e As EventArgs) Handles ButtonRegister.MouseLeave
        ButtonRegister.FlatAppearance.BorderColor = Color.White
        ButtonRegister.BackColor = Color.FromArgb(24, 30, 36)
        ButtonRegister.ForeColor = Color.White
    End Sub

    Private Sub LinkForgot_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkForgot.LinkClicked
        MessageBox.Show("Password recovery not implemented.")
    End Sub

End Class