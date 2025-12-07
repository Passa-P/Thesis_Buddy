Imports System.Text.Json
Imports System.Text
Imports System.Linq

Public Module ExpertEngine

    Public Class Consultation
        Public Property Username As String
        Public Property Timestamp As DateTime
        Public Property Answers As Dictionary(Of String, Object)
        Public Property Profile As MotivationProfileResult
    End Class

    Public Class MotivationProfileResult
        Public Property PrimaryCategory As String
        Public Property Scores As Dictionary(Of String, Double)
        Public Property Recommendations As List(Of String)
        Public Property Evidence As Dictionary(Of String, List(Of String))
    End Class

    ' Normalize a 0-1 value
    Public Function Normalize(value As Double) As Double
        If value < 0 Then Return 0
        If value > 1 Then Return 1
        Return value
    End Function

    Private Function CombineCertainty(existing As Double, incoming As Double) As Double
        If existing = 0 Then Return incoming
        Return existing + incoming * (1 - existing)
    End Function

    Public Function Evaluate(answers As Dictionary(Of String, Object), questionBank As IEnumerable(Of QuestionModel)) As MotivationProfileResult
        If questionBank Is Nothing Then
            questionBank = Enumerable.Empty(Of QuestionModel)()
        End If

        Dim scores As New Dictionary(Of String, Double) From {
            {"nAch", 0.0},
            {"nAff", 0.0},
            {"nPow", 0.0}
        }

        Dim evidence As New Dictionary(Of String, List(Of String)) From {
            {"nAch", New List(Of String)()},
            {"nAff", New List(Of String)()},
            {"nPow", New List(Of String)()}
        }

        For Each question In questionBank
            If question Is Nothing OrElse String.IsNullOrWhiteSpace(question.QKey) Then Continue For
            Dim answeredYes As Boolean = IsAffirmativeAnswer(answers, question.QKey)
            If Not answeredYes Then Continue For

            Dim category As String = If(String.IsNullOrWhiteSpace(question.Category), DetermineLocalCategory(question.QKey), question.Category)
            If Not scores.ContainsKey(category) Then Continue For

            Dim cfRule As Double = Normalize(question.CertaintyFactor)
            scores(category) = CombineCertainty(scores(category), cfRule)
            Dim marker As String = If(String.IsNullOrWhiteSpace(question.RuleCode), question.QKey, question.RuleCode)
            evidence(category).Add($"{marker} → {question.Prompt}")
        Next

        Dim ordered = scores.OrderByDescending(Function(kvp) kvp.Value).ToList()
        Dim primaryCategory As String = If(ordered.Count > 0, ordered(0).Key, "nAch")

        Dim result As New MotivationProfileResult()
        result.PrimaryCategory = primaryCategory
        result.Scores = scores
        result.Recommendations = GetRecommendations(primaryCategory)
        result.Evidence = evidence
        Return result
    End Function

    Private Function IsAffirmativeAnswer(answers As Dictionary(Of String, Object), qKey As String) As Boolean
        If answers Is Nothing OrElse Not answers.ContainsKey(qKey) Then Return False
        Dim raw As Object = answers(qKey)
        If raw Is Nothing Then Return False
        Dim text As String = raw.ToString().Trim().ToLower()
        Return text = "ya" OrElse text = "yes" OrElse text = "1" OrElse text = "true"
    End Function

    Private Function DetermineLocalCategory(qKey As String) As String
        If String.IsNullOrWhiteSpace(qKey) Then Return "nAch"
        If qKey.StartsWith("F_A", StringComparison.OrdinalIgnoreCase) Then
            Return "nAch"
        ElseIf qKey.StartsWith("F_B", StringComparison.OrdinalIgnoreCase) Then
            Return "nAff"
        Else
            Return "nPow"
        End If
    End Function

    Private Function GetRecommendations(category As String) As List(Of String)
        Select Case category
            Case "nAch"
                Return New List(Of String) From {
                    "Pengembangan Model Prediksi Harga Komoditas berbasis Deep Learning dengan Arsitektur Transformer",
                    "Optimasi Waktu Eksekusi Algoritma Kriptografi Elliptic Curve pada Perangkat IoT",
                    "Perbandingan Kinerja Metode Sampling Data pada Klasifikasi Teks dengan Imbalanced Data"
                }
            Case "nAff"
                Return New List(Of String) From {
                    "Evaluasi User Experience Aplikasi Layanan Publik menggunakan Heuristic Evaluation dan SUS",
                    "Perancangan Sistem Informasi Kolaborasi Antar Divisi dengan Pendekatan Sociotechnical",
                    "Analisis Sentimen Ulasan Pengguna Aplikasi E-Commerce untuk Peningkatan Layanan"
                }
            Case Else
                Return New List(Of String) From {
                    "Analisis dan Perancangan Master Plan Sistem Informasi berbasis TOGAF ADM",
                    "Evaluasi Kematangan Tata Kelola TI menggunakan COBIT 2019 di Sektor Publik",
                    "Perancangan Model Manajemen Risiko Keamanan Informasi berdasarkan ISO 27005"
                }
        End Select
    End Function

    Public Function SaveConsultationToDb(username As String, answers As Dictionary(Of String, Object), profile As MotivationProfileResult) As Boolean
        Try
            Dim cons As New Consultation()
            cons.Username = username
            cons.Timestamp = DateTime.Now
            cons.Answers = answers
            cons.Profile = profile
            Dim answersJson As String = JsonSerializer.Serialize(answers)
            Dim profileJson As String = JsonSerializer.Serialize(profile)
            Return DatabaseHelper.SaveConsultation(username, answersJson, profileJson)
        Catch ex As Exception
            Return False
        End Try
    End Function

End Module
