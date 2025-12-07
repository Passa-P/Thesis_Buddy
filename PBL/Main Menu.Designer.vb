<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Menu
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
        ButtonStart = New Button()
        ButtonAbout = New Button()
        ButtonExit = New Button()
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
        PanelCard.BackColor = Color.FromArgb(CByte(255), CByte(255), CByte(255))
        PanelCard.Controls.Add(LabelTitle)
        PanelCard.Controls.Add(ButtonStart)
        PanelCard.Controls.Add(ButtonAbout)
        PanelCard.Controls.Add(ButtonExit)
        PanelCard.Location = New Point(440, 100)
        PanelCard.Name = "PanelCard"
        PanelCard.Size = New Size(480, 400)
        PanelCard.TabIndex = 2
        ' 
        ' LabelTitle
        ' 
        LabelTitle.AutoSize = True
        LabelTitle.Font = New Font("Segoe UI", 26F, FontStyle.Bold)
        LabelTitle.ForeColor = Color.FromArgb(CByte(59), CByte(130), CByte(246))
        LabelTitle.Location = New Point(95, 40)
        LabelTitle.Name = "LabelTitle"
        LabelTitle.Size = New Size(290, 47)
        LabelTitle.TabIndex = 3
        LabelTitle.Text = "ThesisBuddy"
        ' 
        ' ButtonStart
        ' 
        ButtonStart.BackColor = Color.FromArgb(CByte(59), CByte(130), CByte(246))
        ButtonStart.FlatAppearance.BorderSize = 0
        ButtonStart.FlatStyle = FlatStyle.Flat
        ButtonStart.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        ButtonStart.ForeColor = Color.White
        ButtonStart.Location = New Point(90, 150)
        ButtonStart.Name = "ButtonStart"
        ButtonStart.Size = New Size(300, 70)
        ButtonStart.TabIndex = 4
        ButtonStart.Text = "▶ Start Consultation"
        ButtonStart.UseVisualStyleBackColor = False
        ButtonStart.Cursor = Cursors.Hand
        ' 
        ' ButtonAbout
        ' 
        ButtonAbout.BackColor = Color.White
        ButtonAbout.FlatAppearance.BorderColor = Color.FromArgb(CByte(59), CByte(130), CByte(246))
        ButtonAbout.FlatAppearance.BorderSize = 2
        ButtonAbout.FlatStyle = FlatStyle.Flat
        ButtonAbout.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        ButtonAbout.ForeColor = Color.FromArgb(CByte(59), CByte(130), CByte(246))
        ButtonAbout.Location = New Point(90, 245)
        ButtonAbout.Name = "ButtonAbout"
        ButtonAbout.Size = New Size(300, 50)
        ButtonAbout.TabIndex = 5
        ButtonAbout.Text = "ℹ About"
        ButtonAbout.UseVisualStyleBackColor = False
        ButtonAbout.Cursor = Cursors.Hand
        ' 
        ' ButtonExit
        ' 
        ButtonExit.BackColor = Color.White
        ButtonExit.FlatAppearance.BorderColor = Color.FromArgb(CByte(239), CByte(68), CByte(68))
        ButtonExit.FlatAppearance.BorderSize = 2
        ButtonExit.FlatStyle = FlatStyle.Flat
        ButtonExit.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        ButtonExit.ForeColor = Color.FromArgb(CByte(239), CByte(68), CByte(68))
        ButtonExit.Location = New Point(90, 315)
        ButtonExit.Name = "ButtonExit"
        ButtonExit.Size = New Size(300, 50)
        ButtonExit.TabIndex = 6
        ButtonExit.Text = "✖ Exit"
        ButtonExit.UseVisualStyleBackColor = False
        ButtonExit.Cursor = Cursors.Hand
        ' 
        ' Main_Menu
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
        Name = "Main_Menu"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Main Menu - ThesisBuddy"
        PanelLeft.ResumeLayout(False)
        CType(PictureLogo, ComponentModel.ISupportInitialize).EndInit()
        PanelCard.ResumeLayout(False)
        PanelCard.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents PanelLeft As Panel
    Friend WithEvents PictureLogo As PictureBox
    Friend WithEvents PanelCard As Panel
    Friend WithEvents LabelTitle As Label
    Friend WithEvents ButtonStart As Button
    Friend WithEvents ButtonAbout As Button
    Friend WithEvents ButtonExit As Button
End Class
