<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Register
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
        PanelLeft = New Panel()
        PictureLogo = New PictureBox()
        PanelCard = New Panel()
        LabelTitle = New Label()
        LabelSubtitle = New Label()
        LabelUsername = New Label()
        TextBoxUsername = New TextBox()
        LabelPassword = New Label()
        TextBoxPassword = New TextBox()
        LabelConfirm = New Label()
        TextBoxConfirm = New TextBox()
        CheckBoxShow = New CheckBox()
        ButtonBack = New Button()
        ButtonRegister = New Button()
        PanelLeft.SuspendLayout()
        CType(PictureLogo, ComponentModel.ISupportInitialize).BeginInit()
        PanelCard.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelLeft
        ' 
        PanelLeft.BackColor = Color.FromArgb(CByte(12), CByte(18), CByte(25))
        PanelLeft.Controls.Add(PictureLogo)
        PanelLeft.Dock = DockStyle.Left
        PanelLeft.Location = New Point(0, 0)
        PanelLeft.Name = "PanelLeft"
        PanelLeft.Size = New Size(360, 520)
        PanelLeft.TabIndex = 0
        ' 
        ' PictureLogo
        ' 
        PictureLogo.Image = My.Resources.Resources.thesisbuddy_logo
        PictureLogo.Location = New Point(-20, 23)
        PictureLogo.Name = "PictureLogo"
        PictureLogo.Size = New Size(405, 473)
        PictureLogo.SizeMode = PictureBoxSizeMode.Zoom
        PictureLogo.TabIndex = 1
        PictureLogo.TabStop = False
        ' 
        ' PanelCard
        ' 
        PanelCard.BackColor = Color.FromArgb(CByte(24), CByte(30), CByte(36))
        PanelCard.Controls.Add(LabelTitle)
        PanelCard.Controls.Add(LabelSubtitle)
        PanelCard.Controls.Add(LabelUsername)
        PanelCard.Controls.Add(TextBoxUsername)
        PanelCard.Controls.Add(LabelPassword)
        PanelCard.Controls.Add(TextBoxPassword)
        PanelCard.Controls.Add(LabelConfirm)
        PanelCard.Controls.Add(TextBoxConfirm)
        PanelCard.Controls.Add(CheckBoxShow)
        PanelCard.Controls.Add(ButtonBack)
        PanelCard.Controls.Add(ButtonRegister)
        PanelCard.Location = New Point(400, 80)
        PanelCard.Name = "PanelCard"
        PanelCard.Size = New Size(460, 360)
        PanelCard.TabIndex = 2
        ' 
        ' LabelTitle
        ' 
        LabelTitle.AutoSize = True
        LabelTitle.Font = New Font("Segoe UI Semibold", 18.0F, FontStyle.Bold)
        LabelTitle.ForeColor = Color.FromArgb(CByte(72), CByte(233), CByte(223))
        LabelTitle.Location = New Point(36, 28)
        LabelTitle.Name = "LabelTitle"
        LabelTitle.Size = New Size(220, 32)
        LabelTitle.TabIndex = 3
        LabelTitle.Text = "Create an account"
        ' 
        ' LabelSubtitle
        ' 
        LabelSubtitle.AutoSize = True
        LabelSubtitle.Font = New Font("Segoe UI", 9.0F)
        LabelSubtitle.ForeColor = Color.LightGray
        LabelSubtitle.Location = New Point(36, 70)
        LabelSubtitle.Name = "LabelSubtitle"
        LabelSubtitle.Size = New Size(170, 15)
        LabelSubtitle.TabIndex = 4
        LabelSubtitle.Text = "Register to ThesisBuddy"
        ' 
        ' LabelUsername
        ' 
        LabelUsername.AutoSize = True
        LabelUsername.Font = New Font("Segoe UI", 9.0F)
        LabelUsername.ForeColor = Color.LightGray
        LabelUsername.Location = New Point(36, 110)
        LabelUsername.Name = "LabelUsername"
        LabelUsername.Size = New Size(60, 15)
        LabelUsername.TabIndex = 5
        LabelUsername.Text = "Username"
        ' 
        ' TextBoxUsername
        ' 
        TextBoxUsername.BackColor = Color.White
        TextBoxUsername.BorderStyle = BorderStyle.FixedSingle
        TextBoxUsername.Font = New Font("Segoe UI", 10.0F)
        TextBoxUsername.ForeColor = Color.FromArgb(CByte(10), CByte(20), CByte(26))
        TextBoxUsername.Location = New Point(36, 135)
        TextBoxUsername.Name = "TextBoxUsername"
        TextBoxUsername.Size = New Size(388, 25)
        TextBoxUsername.TabIndex = 6
        ' 
        ' LabelPassword
        ' 
        LabelPassword.AutoSize = True
        LabelPassword.Font = New Font("Segoe UI", 9.0F)
        LabelPassword.ForeColor = Color.LightGray
        LabelPassword.Location = New Point(36, 170)
        LabelPassword.Name = "LabelPassword"
        LabelPassword.Size = New Size(57, 15)
        LabelPassword.TabIndex = 7
        LabelPassword.Text = "Password"
        ' 
        ' TextBoxPassword
        ' 
        TextBoxPassword.BackColor = Color.White
        TextBoxPassword.BorderStyle = BorderStyle.FixedSingle
        TextBoxPassword.Font = New Font("Segoe UI", 10.0F)
        TextBoxPassword.ForeColor = Color.FromArgb(CByte(10), CByte(20), CByte(26))
        TextBoxPassword.Location = New Point(36, 195)
        TextBoxPassword.Name = "TextBoxPassword"
        TextBoxPassword.PasswordChar = "?"c
        TextBoxPassword.Size = New Size(360, 25)
        TextBoxPassword.TabIndex = 8
        ' 
        ' LabelConfirm
        ' 
        LabelConfirm.AutoSize = True
        LabelConfirm.Font = New Font("Segoe UI", 9.0F)
        LabelConfirm.ForeColor = Color.LightGray
        LabelConfirm.Location = New Point(36, 225)
        LabelConfirm.Name = "LabelConfirm"
        LabelConfirm.Size = New Size(100, 15)
        LabelConfirm.TabIndex = 9
        LabelConfirm.Text = "Confirm Password"
        ' 
        ' TextBoxConfirm
        ' 
        TextBoxConfirm.BackColor = Color.White
        TextBoxConfirm.BorderStyle = BorderStyle.FixedSingle
        TextBoxConfirm.Font = New Font("Segoe UI", 10.0F)
        TextBoxConfirm.ForeColor = Color.FromArgb(CByte(10), CByte(20), CByte(26))
        TextBoxConfirm.Location = New Point(36, 250)
        TextBoxConfirm.Name = "TextBoxConfirm"
        TextBoxConfirm.PasswordChar = "?"c
        TextBoxConfirm.Size = New Size(388, 25)
        TextBoxConfirm.TabIndex = 10
        ' 
        ' CheckBoxShow
        ' 
        CheckBoxShow.AutoSize = True
        CheckBoxShow.ForeColor = Color.LightGray
        CheckBoxShow.Location = New Point(403, 199)
        CheckBoxShow.Name = "CheckBoxShow"
        CheckBoxShow.Size = New Size(15, 14)
        CheckBoxShow.TabIndex = 11
        CheckBoxShow.UseVisualStyleBackColor = True
        ' 
        ' ButtonBack
        ' 
        ButtonBack.BackColor = Color.FromArgb(CByte(24), CByte(30), CByte(36))
        ButtonBack.FlatStyle = FlatStyle.Flat
        ButtonBack.FlatAppearance.BorderSize = 2
        ButtonBack.FlatAppearance.BorderColor = Color.White
        ButtonBack.Font = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        ButtonBack.ForeColor = Color.White
        ButtonBack.Location = New Point(36, 300)
        ButtonBack.Name = "ButtonBack"
        ButtonBack.Size = New Size(140, 40)
        ButtonBack.TabIndex = 12
        ButtonBack.Text = "Back"
        ButtonBack.UseVisualStyleBackColor = False
        ' 
        ' ButtonRegister
        ' 
        ButtonRegister.BackColor = Color.FromArgb(CByte(24), CByte(30), CByte(36))
        ButtonRegister.FlatStyle = FlatStyle.Flat
        ButtonRegister.FlatAppearance.BorderSize = 2
        ButtonRegister.FlatAppearance.BorderColor = Color.White
        ButtonRegister.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        ButtonRegister.ForeColor = Color.White
        ButtonRegister.Location = New Point(304, 300)
        ButtonRegister.Name = "ButtonRegister"
        ButtonRegister.Size = New Size(120, 40)
        ButtonRegister.TabIndex = 13
        ButtonRegister.Text = "Create account"
        ButtonRegister.UseVisualStyleBackColor = False
        ' 
        ' Register
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 17.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(18), CByte(24), CByte(31))
        ClientSize = New Size(900, 520)
        Controls.Add(PanelCard)
        Controls.Add(PanelLeft)
        Font = New Font("Segoe UI", 10.0F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "Register"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Register - ThesisBuddy"
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
    Friend WithEvents ButtonBack As Button
    Friend WithEvents ButtonRegister As Button
    Friend WithEvents CheckBoxShow As CheckBox
    Friend WithEvents TextBoxConfirm As TextBox
    Friend WithEvents TextBoxPassword As TextBox
    Friend WithEvents TextBoxUsername As TextBox
    Friend WithEvents LabelConfirm As Label
    Friend WithEvents LabelPassword As Label
    Friend WithEvents LabelUsername As Label
End Class