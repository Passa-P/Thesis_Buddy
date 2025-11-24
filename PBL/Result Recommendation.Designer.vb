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
        Panel1.Controls.Add(Label1)
        Panel1.Location = New Point(0, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(801, 85)
        Panel1.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(216, 25)
        Label1.Name = "Label1"
        Label1.Size = New Size(366, 31)
        Label1.TabIndex = 0
        Label1.Text = "Hasil Rekomendasi Topik Skripsi" & vbCrLf
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(85, 122)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(658, 210)
        TextBox1.TabIndex = 1
        TextBox1.Text = "Hasil Rekomendasi.."
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(465, 379)
        Button1.Name = "Button1"
        Button1.Size = New Size(160, 53)
        Button1.TabIndex = 2
        Button1.Text = "Kembali"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(631, 379)
        Button2.Name = "Button2"
        Button2.Size = New Size(160, 53)
        Button2.TabIndex = 3
        Button2.Text = "Simpan"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Result_Recommendation
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Cornsilk
        ClientSize = New Size(800, 450)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(TextBox1)
        Controls.Add(Panel1)
        Name = "Result_Recommendation"
        Text = "Result Recommendation"
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
