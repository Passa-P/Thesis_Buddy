<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        Label2 = New Label()
        Panel1 = New Panel()
        Panel3 = New Panel()
        Button1 = New Button()
        Button3 = New Button()
        Button2 = New Button()
        PictureBox1 = New PictureBox()
        Panel1.SuspendLayout()
        Panel3.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 28.2F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = SystemColors.ActiveCaptionText
        Label1.Location = New Point(119, 17)
        Label1.Name = "Label1"
        Label1.Size = New Size(809, 62)
        Label1.TabIndex = 3
        Label1.Text = "Sistem Pakar Penentu Topik Skripsi"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(33, 14)
        Label2.Name = "Label2"
        Label2.Size = New Size(196, 25)
        Label2.TabIndex = 4
        Label2.Text = "© 2025 – Thesis Buddy"
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(Label2)
        Panel1.Location = New Point(1, 565)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1040, 57)
        Panel1.TabIndex = 5
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(Label1)
        Panel3.Location = New Point(1, 1)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1040, 110)
        Panel3.TabIndex = 7
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(182, 297)
        Button1.Name = "Button1"
        Button1.Size = New Size(179, 60)
        Button1.TabIndex = 0
        Button1.Text = "Mulai Konsultasi ->"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(741, 297)
        Button3.Name = "Button3"
        Button3.Size = New Size(179, 60)
        Button3.TabIndex = 2
        Button3.Text = "Exit"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(443, 297)
        Button2.Name = "Button2"
        Button2.Size = New Size(179, 60)
        Button2.TabIndex = 1
        Button2.Text = "About Sistem"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(12, 117)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(164, 100)
        PictureBox1.TabIndex = 8
        PictureBox1.TabStop = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Cornsilk
        ClientSize = New Size(1041, 622)
        Controls.Add(PictureBox1)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Panel3)
        Controls.Add(Button1)
        Controls.Add(Panel1)
        ForeColor = SystemColors.ActiveCaptionText
        Name = "Form1"
        Text = "Dashboard"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents PictureBox1 As PictureBox

End Class
