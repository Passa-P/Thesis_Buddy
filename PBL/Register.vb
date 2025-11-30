Imports System.Drawing.Drawing2D

Public Class Register

    Private Sub Register_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim found = Me.Controls.Find("PanelCard", True)
        If found.Length > 0 Then
            Dim panelCard As Panel = DirectCast(found(0), Panel)
            panelCard.Left = (Me.ClientSize.Width - panelCard.Width) \ 2
            panelCard.Top = (Me.ClientSize.Height - panelCard.Height) \ 2

            ' Rounded corners for card
            Try
                Dim r As Integer = 12
                Dim rect As Rectangle = panelCard.ClientRectangle
                Dim path As New GraphicsPath()
                path.AddArc(rect.X, rect.Y, r, r, 180, 90)
                path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90)
                path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90)
                path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90)
                path.CloseFigure()
                panelCard.Region = New Region(path)
            Catch ex As Exception
            End Try
        End If

        ' Placeholders
        SetPlaceholder(TextBoxUsername, "Username")
        SetPlaceholderPassword(TextBoxPassword, "Password")
        SetPlaceholderPassword(TextBoxConfirm, "Confirm Password")
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
            ' placeholder re-applied by SetPlaceholder caller
        End If
    End Sub

    Private Sub RemovePasswordPlaceholder(sender As Object, e As EventArgs)
        Dim tb = DirectCast(sender, TextBox)
        If tb.ForeColor = Color.Gray Then
            tb.Text = String.Empty
            tb.ForeColor = Color.Black
            tb.PasswordChar = "?"c
        End If
    End Sub

    Private Sub ApplyPasswordPlaceholder(sender As Object, e As EventArgs)
        Dim tb = DirectCast(sender, TextBox)
        If String.IsNullOrWhiteSpace(tb.Text) Then
            tb.Text = "Confirm Password"
            tb.ForeColor = Color.Gray
            tb.PasswordChar = ControlChars.NullChar
        End If
    End Sub

    Private Sub ButtonRegister_Click(sender As Object, e As EventArgs) Handles ButtonRegister.Click
        Dim username As String = TextBoxUsername.Text.Trim()
        Dim password As String = TextBoxPassword.Text
        Dim confirm As String = TextBoxConfirm.Text

        If TextBoxUsername.ForeColor = Color.Gray Then username = String.Empty
        If TextBoxPassword.ForeColor = Color.Gray Then password = String.Empty
        If TextBoxConfirm.ForeColor = Color.Gray Then confirm = String.Empty

        If String.IsNullOrEmpty(username) Or String.IsNullOrEmpty(password) Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If

        If password <> confirm Then
            MessageBox.Show("Passwords do not match.")
            Return
        End If

        Try
            If DatabaseHelper.RegisterUser(username, password) Then
                MessageBox.Show("Registration successful!")
                Me.Close()
            Else
                MessageBox.Show("Username already exists.")
            End If
        Catch ex As Exception
            MessageBox.Show("Database error: " & ex.Message)
        End Try
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        Me.Close()
    End Sub

    Private Sub ButtonRegister_MouseEnter(sender As Object, e As EventArgs) Handles ButtonRegister.MouseEnter
        ButtonRegister.BackColor = Color.FromArgb(5, 90, 180)
    End Sub

    Private Sub ButtonRegister_MouseLeave(sender As Object, e As EventArgs) Handles ButtonRegister.MouseLeave
        ButtonRegister.BackColor = Color.FromArgb(3, 58, 115)
    End Sub
End Class