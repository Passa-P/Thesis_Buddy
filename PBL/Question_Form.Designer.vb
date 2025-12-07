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
        PictureLogo = New PictureBox()
        PanelCard = New Panel()
        LabelTitle = New Label()
        LabelSubtitle = New Label()
        FlowLayoutPanelQuestions = New FlowLayoutPanel()
        PanelNav = New FlowLayoutPanel()
        ButtonSubmit = New Button()
        ButtonCancel = New Button()
        CType(PictureLogo, ComponentModel.ISupportInitialize).BeginInit()
        PanelCard.SuspendLayout()
        SuspendLayout()
        ' 
        ' PictureLogo
        ' 
        PictureLogo.BackColor = Color.Transparent
        PictureLogo.Image = My.Resources.Resources.thesisbuddy_logo
        PictureLogo.Location = New Point(24, 24)
        PictureLogo.Name = "PictureLogo"
        PictureLogo.Size = New Size(80, 80)
        PictureLogo.SizeMode = PictureBoxSizeMode.Zoom
        PictureLogo.TabIndex = 1
        PictureLogo.TabStop = False
        ' 
        ' PanelCard
        ' 
        PanelCard.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        PanelCard.BackColor = Color.FromArgb(CByte(17), CByte(24), CByte(39))
        PanelCard.Controls.Add(PictureLogo)
        PanelCard.Controls.Add(LabelTitle)
        PanelCard.Controls.Add(LabelSubtitle)
        PanelCard.Controls.Add(FlowLayoutPanelQuestions)
        PanelCard.Controls.Add(PanelNav)
        PanelCard.Controls.Add(ButtonSubmit)
        PanelCard.Controls.Add(ButtonCancel)
        PanelCard.Location = New Point(33, 30)
        PanelCard.Name = "PanelCard"
        PanelCard.Size = New Size(967, 640)
        PanelCard.TabIndex = 2
        ' 
        ' LabelTitle
        ' 
        LabelTitle.AutoSize = True
        LabelTitle.Font = New Font("Segoe UI", 22.0F, FontStyle.Bold)
        LabelTitle.ForeColor = Color.FromArgb(CByte(96), CByte(165), CByte(250))
        LabelTitle.Location = New Point(105, 31)
        LabelTitle.Name = "LabelTitle"
        LabelTitle.Size = New Size(345, 41)
        LabelTitle.TabIndex = 3
        LabelTitle.Text = "Motivation Assessment"
        ' 
        ' LabelSubtitle
        ' 
        LabelSubtitle.AutoSize = True
        LabelSubtitle.BackColor = Color.Transparent
        LabelSubtitle.Font = New Font("Segoe UI", 11.0F)
        LabelSubtitle.ForeColor = Color.FromArgb(CByte(148), CByte(163), CByte(184))
        LabelSubtitle.Location = New Point(105, 79)
        LabelSubtitle.Name = "LabelSubtitle"
        LabelSubtitle.Size = New Size(380, 20)
        LabelSubtitle.TabIndex = 4
        LabelSubtitle.Text = "Answer all questions to get your thesis recommendation"
        ' 
        ' FlowLayoutPanelQuestions
        ' 
        FlowLayoutPanelQuestions.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        FlowLayoutPanelQuestions.AutoScroll = True
        FlowLayoutPanelQuestions.BackColor = Color.FromArgb(CByte(28), CByte(38), CByte(57))
        FlowLayoutPanelQuestions.FlowDirection = FlowDirection.TopDown
        FlowLayoutPanelQuestions.Location = New Point(40, 110)
        FlowLayoutPanelQuestions.Name = "FlowLayoutPanelQuestions"
        FlowLayoutPanelQuestions.Padding = New Padding(12)
        FlowLayoutPanelQuestions.Size = New Size(887, 460)
        FlowLayoutPanelQuestions.TabIndex = 5
        FlowLayoutPanelQuestions.WrapContents = False
        ' 
        ' PanelNav
        ' 
        PanelNav.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        PanelNav.BackColor = Color.Transparent
        PanelNav.FlowDirection = FlowDirection.LeftToRight
        PanelNav.Location = New Point(40, 580)
        PanelNav.Margin = New Padding(0)
        PanelNav.Name = "PanelNav"
        PanelNav.Padding = New Padding(0)
        PanelNav.Size = New Size(887, 50)
        PanelNav.TabIndex = 7
        PanelNav.WrapContents = False
        ' 
        ' ButtonSubmit
        ' 
        ButtonSubmit.BackColor = Color.FromArgb(CByte(96), CByte(165), CByte(250))
        ButtonSubmit.Cursor = Cursors.Hand
        ButtonSubmit.FlatAppearance.BorderSize = 0
        ButtonSubmit.FlatStyle = FlatStyle.Flat
        ButtonSubmit.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        ButtonSubmit.ForeColor = Color.White
        ButtonSubmit.Location = New Point(595, 580)
        ButtonSubmit.Name = "ButtonSubmit"
        ButtonSubmit.Size = New Size(190, 50)
        ButtonSubmit.TabIndex = 6
        ButtonSubmit.Text = "Submit Answers"
        ButtonSubmit.UseVisualStyleBackColor = False
        ButtonSubmit.Visible = False
        ' 
        ' ButtonCancel
        ' 
        ButtonCancel.BackColor = Color.FromArgb(CByte(15), CByte(23), CByte(42))
        ButtonCancel.Cursor = Cursors.Hand
        ButtonCancel.FlatAppearance.BorderColor = Color.FromArgb(CByte(248), CByte(113), CByte(113))
        ButtonCancel.FlatAppearance.BorderSize = 2
        ButtonCancel.FlatStyle = FlatStyle.Flat
        ButtonCancel.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        ButtonCancel.ForeColor = Color.FromArgb(CByte(248), CByte(113), CByte(113))
        ButtonCancel.Location = New Point(40, 580)
        ButtonCancel.Name = "ButtonCancel"
        ButtonCancel.Size = New Size(190, 50)
        ButtonCancel.TabIndex = 7
        ButtonCancel.Text = "Cancel"
        ButtonCancel.UseVisualStyleBackColor = False
        ButtonCancel.Visible = False
        ' 
        ' Question_Form
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 17.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(9), CByte(12), CByte(23))
        ClientSize = New Size(1050, 700)
        Controls.Add(PanelCard)
        Font = New Font("Segoe UI", 10.0F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "Question_Form"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Question Form - ThesisBuddy"
        CType(PictureLogo, ComponentModel.ISupportInitialize).EndInit()
        PanelCard.ResumeLayout(False)
        PanelCard.PerformLayout()
        ResumeLayout(False)

    End Sub
    Friend WithEvents PictureLogo As PictureBox
    Friend WithEvents PanelCard As Panel
    Friend WithEvents LabelTitle As Label
    Friend WithEvents LabelSubtitle As Label
    Friend WithEvents FlowLayoutPanelQuestions As FlowLayoutPanel
    Friend WithEvents PanelNav As FlowLayoutPanel
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
