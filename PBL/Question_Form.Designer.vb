<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Question_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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

    ' NOTE: The following procedure is required by the Windows Form Designer
    ' It can be modified using the Windows Form Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        PanelLeft = New Panel()
        PictureLogo = New PictureBox()
        PanelCard = New Panel()
        LabelTitle = New Label()
        LabelSubtitle = New Label()
        FlowLayoutPanelQuestions = New FlowLayoutPanel()
        PanelNav = New Panel()
        ButtonSubmit = New Button()
        ButtonCancel = New Button()
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
        PanelLeft.Padding = New Padding(20)
        PanelLeft.Size = New Size(360, 520)
        PanelLeft.TabIndex = 0
        ' 
        ' PictureLogo
        ' 
        PictureLogo.Image = My.Resources.Resources.thesisbuddy_logo
        PictureLogo.Location = New Point(20, 20)
        PictureLogo.Name = "PictureLogo"
        PictureLogo.Size = New Size(320, 480)
        PictureLogo.SizeMode = PictureBoxSizeMode.Zoom
        PictureLogo.TabIndex = 1
        PictureLogo.TabStop = False
        ' 
        ' PanelCard
        ' 
        PanelCard.BackColor = Color.FromArgb(CByte(24), CByte(30), CByte(36))
        PanelCard.Controls.Add(LabelTitle)
        PanelCard.Controls.Add(LabelSubtitle)
        PanelCard.Controls.Add(FlowLayoutPanelQuestions)
        PanelCard.Controls.Add(PanelNav)
        PanelCard.Controls.Add(ButtonSubmit)
        PanelCard.Controls.Add(ButtonCancel)
        PanelCard.Location = New Point(400, 40)
        PanelCard.Name = "PanelCard"
        PanelCard.Size = New Size(460, 440)
        PanelCard.TabIndex = 2
        ' 
        ' LabelTitle
        ' 
        LabelTitle.AutoSize = True
        LabelTitle.Font = New Font("Segoe UI Semibold", 18F, FontStyle.Bold)
        LabelTitle.ForeColor = Color.FromArgb(CByte(72), CByte(233), CByte(223))
        LabelTitle.Location = New Point(36, 18)
        LabelTitle.Name = "LabelTitle"
        LabelTitle.Size = New Size(296, 32)
        LabelTitle.TabIndex = 3
        LabelTitle.Text = "Sistem Pakar ThesisBuddy"
        ' 
        ' LabelSubtitle
        ' 
        LabelSubtitle.AutoSize = True
        LabelSubtitle.Font = New Font("Segoe UI", 10F)
        LabelSubtitle.ForeColor = Color.FromArgb(CByte(160), CByte(160), CByte(160))
        LabelSubtitle.Location = New Point(37, 58)
        LabelSubtitle.Name = "LabelSubtitle"
        LabelSubtitle.Size = New Size(125, 19)
        LabelSubtitle.TabIndex = 4
        LabelSubtitle.Text = "Dapatkan jawaban:"
        ' 
        ' FlowLayoutPanelQuestions
        ' 
        FlowLayoutPanelQuestions.AutoScroll = True
        FlowLayoutPanelQuestions.FlowDirection = FlowDirection.TopDown
        FlowLayoutPanelQuestions.Location = New Point(36, 80)
        FlowLayoutPanelQuestions.Name = "FlowLayoutPanelQuestions"
        FlowLayoutPanelQuestions.Padding = New Padding(8)
        FlowLayoutPanelQuestions.Size = New Size(388, 320)
        FlowLayoutPanelQuestions.TabIndex = 5
        FlowLayoutPanelQuestions.WrapContents = False
        ' 
        ' PanelNav
        ' 
        PanelNav.Location = New Point(36, 392)
        PanelNav.Name = "PanelNav"
        PanelNav.Size = New Size(388, 40)
        PanelNav.TabIndex = 7
        ' 
        ' ButtonSubmit
        ' 
        ButtonSubmit.BackColor = Color.FromArgb(CByte(24), CByte(30), CByte(36))
        ButtonSubmit.FlatAppearance.BorderColor = Color.White
        ButtonSubmit.FlatAppearance.BorderSize = 2
        ButtonSubmit.FlatStyle = FlatStyle.Flat
        ButtonSubmit.Font = New Font("Segoe UI Semibold", 11F, FontStyle.Bold)
        ButtonSubmit.ForeColor = Color.White
        ButtonSubmit.Location = New Point(304, 392)
        ButtonSubmit.Name = "ButtonSubmit"
        ButtonSubmit.Size = New Size(120, 40)
        ButtonSubmit.TabIndex = 6
        ButtonSubmit.Text = "Submit"
        ButtonSubmit.UseVisualStyleBackColor = False
        ButtonSubmit.Visible = False
        ' 
        ' ButtonCancel
        ' 
        ButtonCancel.BackColor = Color.FromArgb(CByte(24), CByte(30), CByte(36))
        ButtonCancel.FlatAppearance.BorderColor = Color.White
        ButtonCancel.FlatAppearance.BorderSize = 2
        ButtonCancel.FlatStyle = FlatStyle.Flat
        ButtonCancel.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        ButtonCancel.ForeColor = Color.White
        ButtonCancel.Location = New Point(36, 392)
        ButtonCancel.Name = "ButtonCancel"
        ButtonCancel.Size = New Size(140, 40)
        ButtonCancel.TabIndex = 7
        ButtonCancel.Text = "Cancel"
        ButtonCancel.UseVisualStyleBackColor = False
        ButtonCancel.Visible = False
        ' 
        ' Question_Form
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(18), CByte(24), CByte(31))
        ClientSize = New Size(900, 520)
        Controls.Add(PanelCard)
        Controls.Add(PanelLeft)
        Font = New Font("Segoe UI", 10F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "Question_Form"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Question Form - ThesisBuddy"
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
    Friend WithEvents LabelSubtitle As Label
    Friend WithEvents FlowLayoutPanelQuestions As FlowLayoutPanel
    Friend WithEvents PanelNav As Panel
    Friend WithEvents ButtonSubmit As Button
    Friend WithEvents ButtonCancel As Button

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
