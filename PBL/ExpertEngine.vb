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

    Public Function Evaluate(answers As Dictionary(Of String, Object), questionBank As IEnumerable(Of QuestionModel)) As MotivationProfileResult
        Dim bank = If(questionBank, Enumerable.Empty(Of QuestionModel)())
        Dim ruleSet = MotivationRuleRepository.FromQuestionBank(bank)
        If ruleSet Is Nothing OrElse ruleSet.Count = 0 Then
            ruleSet = MotivationRuleRepository.LoadEmbeddedRules()
        End If

        Dim inference As MotivationInferenceResult
        Try
            Dim engine As New MotivationInferenceEngine(ruleSet)
            inference = engine.Evaluate(answers)
        Catch
            inference = New MotivationInferenceResult With {
                .PrimaryCategory = "nAch",
                .Scores = New Dictionary(Of String, Double) From {
                    {"nAch", 0.0},
                    {"nAff", 0.0},
                    {"nPow", 0.0}
                },
                .Evidence = New Dictionary(Of String, List(Of String)) From {
                    {"nAch", New List(Of String)()},
                    {"nAff", New List(Of String)()},
                    {"nPow", New List(Of String)()}
                }
            }
        End Try

        Dim profile As New MotivationProfileResult()
        profile.PrimaryCategory = inference.PrimaryCategory
        profile.Scores = inference.Scores.ToDictionary(Function(kvp) kvp.Key, Function(kvp) kvp.Value)
        profile.Recommendations = GetRecommendations(profile.PrimaryCategory)
        profile.Evidence = inference.Evidence.ToDictionary(Function(kvp) kvp.Key, Function(kvp) kvp.Value.ToList())
        Return profile
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
