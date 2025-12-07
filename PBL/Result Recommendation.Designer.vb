<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Result_Recommendation
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
        Panel1 = New Panel()
        Label1 = New Label()
        TextBox1 = New TextBox()
        Button1 = New Button()
        Button2 = New Button()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(59), CByte(130), CByte(246))
        Panel1.Controls.Add(Label1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(900, 100)
        Panel1.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 24.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.White
        Label1.Location = New Point(40, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(450, 45)
        Label1.TabIndex = 0
        Label1.Text = "✨ Your Thesis Recommendation"
        ' 
        ' TextBox1
        ' 
        TextBox1.BackColor = Color.FromArgb(CByte(248), CByte(250), CByte(252))
        TextBox1.BorderStyle = BorderStyle.FixedSingle
        TextBox1.Font = New Font("Segoe UI", 11.0F)
        TextBox1.ForeColor = Color.FromArgb(CByte(31), CByte(41), CByte(55))
        TextBox1.Location = New Point(50, 150)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.ScrollBars = ScrollBars.Vertical
        TextBox1.Size = New Size(800, 320)
        TextBox1.TabIndex = 1
        TextBox1.Text = "Loading results..."
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.White
        Button1.FlatAppearance.BorderColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        Button1.FlatAppearance.BorderSize = 2
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        Button1.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        Button1.Location = New Point(50, 500)
        Button1.Name = "Button1"
        Button1.Size = New Size(180, 55)
        Button1.TabIndex = 2
        Button1.Text = "← Back"
        Button1.UseVisualStyleBackColor = False
        Button1.Cursor = Cursors.Hand
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.FromArgb(CByte(59), CByte(130), CByte(246))
        Button2.FlatAppearance.BorderSize = 0
        Button2.FlatStyle = FlatStyle.Flat
        Button2.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        Button2.ForeColor = Color.White
        Button2.Location = New Point(670, 500)
        Button2.Name = "Button2"
        Button2.Size = New Size(180, 55)
        Button2.TabIndex = 3
        Button2.Text = "💾 Save Result"
        Button2.UseVisualStyleBackColor = False
        Button2.Cursor = Cursors.Hand
        ' 
        ' Result_Recommendation
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(243), CByte(244), CByte(246))
        ClientSize = New Size(900, 600)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(TextBox1)
        Controls.Add(Panel1)
        Font = New Font("Segoe UI", 10.0F)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "Result_Recommendation"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Result Recommendation - ThesisBuddy"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
