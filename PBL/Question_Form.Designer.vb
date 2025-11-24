<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Question_Form
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
        Button1 = New Button()
        ProgressBar1 = New ProgressBar()
        CheckBox1 = New CheckBox()
        CheckBox2 = New CheckBox()
        CheckBox3 = New CheckBox()
        CheckBox4 = New CheckBox()
        Button2 = New Button()
        Panel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Label1)
        Panel1.Location = New Point(1, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(800, 108)
        Panel1.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(263, 24)
        Label1.Name = "Label1"
        Label1.Size = New Size(225, 62)
        Label1.TabIndex = 0
        Label1.Text = "Perntanyaan 1 dari ..." & vbCrLf & "     Pertanyaan ...." & vbCrLf
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(628, 369)
        Button1.Name = "Button1"
        Button1.Size = New Size(160, 53)
        Button1.TabIndex = 1
        Button1.Text = "Next"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(26, 393)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(309, 29)
        ProgressBar1.TabIndex = 2
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Font = New Font("Segoe UI", 12F)
        CheckBox1.Location = New Point(151, 135)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(126, 32)
        CheckBox1.TabIndex = 3
        CheckBox1.Text = "Jawaban A" & vbCrLf
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Font = New Font("Segoe UI", 12F)
        CheckBox2.Location = New Point(151, 180)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(124, 32)
        CheckBox2.TabIndex = 4
        CheckBox2.Text = "Jawaban B"
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' CheckBox3
        ' 
        CheckBox3.AutoSize = True
        CheckBox3.Font = New Font("Segoe UI", 12F)
        CheckBox3.Location = New Point(151, 223)
        CheckBox3.Name = "CheckBox3"
        CheckBox3.Size = New Size(125, 32)
        CheckBox3.TabIndex = 5
        CheckBox3.Text = "Jawaban C"
        CheckBox3.UseVisualStyleBackColor = True
        ' 
        ' CheckBox4
        ' 
        CheckBox4.AutoSize = True
        CheckBox4.Font = New Font("Segoe UI", 12F)
        CheckBox4.Location = New Point(151, 268)
        CheckBox4.Name = "CheckBox4"
        CheckBox4.Size = New Size(127, 32)
        CheckBox4.TabIndex = 6
        CheckBox4.Text = "Jawaban D"
        CheckBox4.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(462, 369)
        Button2.Name = "Button2"
        Button2.Size = New Size(160, 53)
        Button2.TabIndex = 7
        Button2.Text = "Back"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Question_Form
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Cornsilk
        ClientSize = New Size(800, 450)
        Controls.Add(Button2)
        Controls.Add(CheckBox4)
        Controls.Add(CheckBox3)
        Controls.Add(CheckBox2)
        Controls.Add(CheckBox1)
        Controls.Add(ProgressBar1)
        Controls.Add(Button1)
        Controls.Add(Panel1)
        Name = "Question_Form"
        Text = "Question Form"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents Button2 As Button
End Class
