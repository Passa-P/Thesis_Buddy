Imports System.Drawing
Imports System.Windows.Forms

Public Class MotivationProfileDialog
    Inherits Form

    Private labelTitle As Label
    Private labelDominant As Label
    Private labelScores As Label
    Private labelRekomendasi As Label
    Private textRekomendasi As TextBox
    Private buttonOK As Button

    Public Sub New(dominant As String, scores As Dictionary(Of String, Double), rekomendasi As List(Of String))
        Me.Text = "Profil Motivasi"
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.ClientSize = New Size(480, 420)
        Me.Font = New Font("Segoe UI", 11.0F)
        Me.BackColor = Color.FromArgb(250, 250, 255)

        labelTitle = New Label() With {
            .Text = "Profil Motivasi",
            .Font = New Font("Segoe UI", 18.0F, FontStyle.Bold),
            .AutoSize = True,
            .Location = New Point(24, 18),
            .ForeColor = Color.FromArgb(59, 130, 246)
        }
        Me.Controls.Add(labelTitle)

        labelDominant = New Label() With {
            .Text = $"Kategori Dominan: {dominant}",
            .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
            .AutoSize = True,
            .Location = New Point(24, 65),
            .ForeColor = Color.FromArgb(31, 41, 55)
        }
        Me.Controls.Add(labelDominant)

        labelScores = New Label() With {
            .Text = "Skor Certainty Factor:",
            .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
            .AutoSize = True,
            .Location = New Point(24, 105),
            .ForeColor = Color.FromArgb(31, 41, 55)
        }
        Me.Controls.Add(labelScores)

        Dim yScore = 135
        For Each kvp In scores
            Dim lbl = New Label() With {
                .Text = $"- {kvp.Key}: {kvp.Value * 100:0.0}%",
                .Font = New Font("Segoe UI", 11.0F),
                .AutoSize = True,
                .Location = New Point(40, yScore),
                .ForeColor = Color.FromArgb(55, 65, 81)
            }
            Me.Controls.Add(lbl)
            yScore += 28
        Next

        labelRekomendasi = New Label() With {
            .Text = "Rekomendasi Judul Skripsi:",
            .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
            .AutoSize = True,
            .Location = New Point(24, yScore + 10),
            .ForeColor = Color.FromArgb(31, 41, 55)
        }
        Me.Controls.Add(labelRekomendasi)

        textRekomendasi = New TextBox() With {
            .Multiline = True,
            .ReadOnly = True,
            .BorderStyle = BorderStyle.None,
            .BackColor = Me.BackColor,
            .Font = New Font("Segoe UI", 11.0F),
            .Location = New Point(40, yScore + 40),
            .Size = New Size(400, 90),
            .ForeColor = Color.FromArgb(55, 65, 81),
            .TabStop = False
        }
        textRekomendasi.Text = String.Join(Environment.NewLine & "- ", "- " & String.Join(Environment.NewLine & "- ", rekomendasi))
        Me.Controls.Add(textRekomendasi)

        buttonOK = New Button() With {
            .Text = "OK",
            .Font = New Font("Segoe UI", 12.0F, FontStyle.Bold),
            .Size = New Size(120, 40),
            .Location = New Point(Me.ClientSize.Width \ 2 - 60, Me.ClientSize.Height - 60),
            .BackColor = Color.FromArgb(59, 130, 246),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        buttonOK.FlatAppearance.BorderSize = 0
        AddHandler buttonOK.Click, Sub() Me.DialogResult = DialogResult.OK
        Me.Controls.Add(buttonOK)
    End Sub
End Class
