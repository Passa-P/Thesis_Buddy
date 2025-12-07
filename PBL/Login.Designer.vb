<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        PanelLeft = New Panel()
        PictureLogo = New PictureBox()
        PanelCard = New Panel()
        LabelTitle = New Label()
        LabelSubtitle = New Label()
        LabelUsername = New Label()
        TextBoxUsername = New TextBox()
        LabelPassword = New Label()
        TextBoxPassword = New TextBox()
        CheckBoxShow = New CheckBox()
        LinkForgot = New LinkLabel()
        ButtonRegister = New Button()
        ButtonLogin = New Button()
        PanelLeft.SuspendLayout()
        CType(PictureLogo, ComponentModel.ISupportInitialize).BeginInit()
        PanelCard.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelLeft
        ' 
        PanelLeft.BackColor = Color.FromArgb(CByte(26), CByte(32), CByte(44))
        PanelLeft.Controls.Add(PictureLogo)
        PanelLeft.Dock = DockStyle.Left
        PanelLeft.Location = New Point(0, 0)
        PanelLeft.Name = "PanelLeft"
        PanelLeft.Size = New Size(380, 600)
        PanelLeft.TabIndex = 0
        ' 
        ' PictureLogo
        ' 
        PictureLogo.Image = CType(resources.GetObject("PictureLogo.Image"), Image)
        PictureLogo.Location = New Point(-20, 23)
        PictureLogo.Name = "PictureLogo"
        PictureLogo.Size = New Size(405, 473)
        PictureLogo.SizeMode = PictureBoxSizeMode.Zoom
        PictureLogo.TabIndex = 1
        PictureLogo.TabStop = False
        ' 
        ' PanelCard
        ' 
        PanelCard.BackColor = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        PanelCard.Controls.Add(LabelTitle)
        PanelCard.Controls.Add(LabelSubtitle)
        PanelCard.Controls.Add(LabelUsername)
        PanelCard.Controls.Add(TextBoxUsername)
        PanelCard.Controls.Add(LabelPassword)
        PanelCard.Controls.Add(TextBoxPassword)
        PanelCard.Controls.Add(CheckBoxShow)
        PanelCard.Controls.Add(LinkForgot)
        PanelCard.Controls.Add(ButtonRegister)
        PanelCard.Controls.Add(ButtonLogin)
        PanelCard.Location = New Point(440, 100)
        PanelCard.Name = "PanelCard"
        PanelCard.Size = New Size(480, 400)
        PanelCard.TabIndex = 2
        ' 
        ' LabelTitle
        ' 
        LabelTitle.AutoSize = True
        LabelTitle.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        LabelTitle.ForeColor = Color.FromArgb(CByte(59), CByte(130), CByte(246))
        LabelTitle.Location = New Point(40, 35)
        LabelTitle.Name = "LabelTitle"
        LabelTitle.Size = New Size(248, 45)
        LabelTitle.TabIndex = 3
        LabelTitle.Text = "Welcome Back!"
        ' 
        ' LabelSubtitle
        ' 
        LabelSubtitle.AutoSize = True
        LabelSubtitle.Font = New Font("Segoe UI", 11F)
        LabelSubtitle.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        LabelSubtitle.Location = New Point(40, 85)
        LabelSubtitle.Name = "LabelSubtitle"
        LabelSubtitle.Size = New Size(237, 20)
        LabelSubtitle.TabIndex = 4
        LabelSubtitle.Text = "Sign in to continue to ThesisBuddy"
        ' 
        ' LabelUsername
        ' 
        LabelUsername.AutoSize = True
        LabelUsername.Font = New Font("Segoe UI", 9F)
        LabelUsername.ForeColor = Color.LightGray
        LabelUsername.Location = New Point(40, 127)
        LabelUsername.Name = "LabelUsername"
        LabelUsername.Size = New Size(60, 15)
        LabelUsername.TabIndex = 5
        LabelUsername.Text = "Username"
        ' 
        ' TextBoxUsername
        ' 
        TextBoxUsername.BackColor = Color.FromArgb(CByte(248), CByte(250), CByte(252))
        TextBoxUsername.BorderStyle = BorderStyle.FixedSingle
        TextBoxUsername.Font = New Font("Segoe UI", 11F)
        TextBoxUsername.ForeColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        TextBoxUsername.Location = New Point(40, 155)
        TextBoxUsername.Name = "TextBoxUsername"
        TextBoxUsername.Size = New Size(400, 27)
        TextBoxUsername.TabIndex = 6
        ' 
        ' LabelPassword
        ' 
        LabelPassword.AutoSize = True
        LabelPassword.Font = New Font("Segoe UI", 9F)
        LabelPassword.ForeColor = Color.LightGray
        LabelPassword.Location = New Point(40, 209)
        LabelPassword.Name = "LabelPassword"
        LabelPassword.Size = New Size(57, 15)
        LabelPassword.TabIndex = 7
        LabelPassword.Text = "Password"
        ' 
        ' TextBoxPassword
        ' 
        TextBoxPassword.BackColor = Color.FromArgb(CByte(248), CByte(250), CByte(252))
        TextBoxPassword.BorderStyle = BorderStyle.FixedSingle
        TextBoxPassword.Font = New Font("Segoe UI", 11F)
        TextBoxPassword.ForeColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        TextBoxPassword.Location = New Point(40, 230)
        TextBoxPassword.Name = "TextBoxPassword"
        TextBoxPassword.PasswordChar = "●"c
        TextBoxPassword.Size = New Size(400, 27)
        TextBoxPassword.TabIndex = 8
        ' 
        ' CheckBoxShow
        ' 
        CheckBoxShow.AutoSize = True
        CheckBoxShow.ForeColor = Color.LightGray
        CheckBoxShow.Location = New Point(425, 210)
        CheckBoxShow.Name = "CheckBoxShow"
        CheckBoxShow.Size = New Size(15, 14)
        CheckBoxShow.TabIndex = 9
        CheckBoxShow.UseVisualStyleBackColor = True
        ' 
        ' LinkForgot
        ' 
        LinkForgot.ActiveLinkColor = Color.FromArgb(CByte(255), CByte(128), CByte(128))
        LinkForgot.AutoSize = True
        LinkForgot.LinkColor = Color.Maroon
        LinkForgot.Location = New Point(36, 260)
        LinkForgot.Name = "LinkForgot"
        LinkForgot.Size = New Size(118, 19)
        LinkForgot.TabIndex = 10
        LinkForgot.TabStop = True
        LinkForgot.Text = "Forgot password?"
        ' 
        ' ButtonRegister
        ' 
        ButtonRegister.BackColor = Color.White
        ButtonRegister.Cursor = Cursors.Hand
        ButtonRegister.FlatAppearance.BorderColor = Color.FromArgb(CByte(59), CByte(130), CByte(246))
        ButtonRegister.FlatAppearance.BorderSize = 2
        ButtonRegister.FlatStyle = FlatStyle.Flat
        ButtonRegister.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        ButtonRegister.ForeColor = Color.FromArgb(CByte(59), CByte(130), CByte(246))
        ButtonRegister.Location = New Point(40, 310)
        ButtonRegister.Name = "ButtonRegister"
        ButtonRegister.Size = New Size(220, 48)
        ButtonRegister.TabIndex = 11
        ButtonRegister.Text = "Create Account"
        ButtonRegister.UseVisualStyleBackColor = False
        ' 
        ' ButtonLogin
        ' 
        ButtonLogin.BackColor = Color.FromArgb(CByte(59), CByte(130), CByte(246))
        ButtonLogin.Cursor = Cursors.Hand
        ButtonLogin.FlatAppearance.BorderSize = 0
        ButtonLogin.FlatStyle = FlatStyle.Flat
        ButtonLogin.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        ButtonLogin.ForeColor = Color.White
        ButtonLogin.Location = New Point(270, 310)
        ButtonLogin.Name = "ButtonLogin"
        ButtonLogin.Size = New Size(170, 48)
        ButtonLogin.TabIndex = 12
        ButtonLogin.Text = "Sign In"
        ButtonLogin.UseVisualStyleBackColor = False
        ' 
        ' Login
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(243), CByte(244), CByte(246))
        ClientSize = New Size(1000, 600)
        Controls.Add(PanelCard)
        Controls.Add(PanelLeft)
        Font = New Font("Segoe UI", 10F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "Login"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Login - ThesisBuddy"
        PanelLeft.ResumeLayout(False)
        CType(PictureLogo, ComponentModel.ISupportInitialize).EndInit()
        PanelCard.ResumeLayout(False)
        PanelCard.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents PanelLeft As Panel
    Friend WithEvents PictureLogo As PictureBox
    Friend WithEvents PanelCard As Panel
    Friend WithEvents LabelSubtitle As Label
    Friend WithEvents LabelTitle As Label
    Friend WithEvents ButtonRegister As Button
    Friend WithEvents ButtonLogin As Button
    Friend WithEvents LinkForgot As LinkLabel
    Friend WithEvents CheckBoxShow As CheckBox
    Friend WithEvents TextBoxPassword As TextBox
    Friend WithEvents TextBoxUsername As TextBox
    Friend WithEvents LabelPassword As Label
    Friend WithEvents LabelUsername As Label
End Class