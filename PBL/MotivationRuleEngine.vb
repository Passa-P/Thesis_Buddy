Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Linq

Public Class MotivationRule
    Public Property FactKey As String
    Public Property Category As String
    Public Property Certainty As Double
    Public Property RuleCode As String
    Public Property Prompt As String
End Class

Public Class MotivationInferenceResult
    Public Property PrimaryCategory As String
    Public Property Scores As IReadOnlyDictionary(Of String, Double)
    Public Property Evidence As IReadOnlyDictionary(Of String, List(Of String))
End Class

Public Class MotivationInferenceEngine
    Private ReadOnly rules As IReadOnlyList(Of MotivationRule)
    Private Shared ReadOnly CategoryKeys As String() = {"nAch", "nAff", "nPow"}

    Public Sub New(sourceRules As IEnumerable(Of MotivationRule))
        If sourceRules Is Nothing Then Throw New ArgumentNullException(NameOf(sourceRules))
        Dim filtered = sourceRules.Where(Function(rule) rule IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(rule.FactKey) AndAlso Not String.IsNullOrWhiteSpace(rule.Category)).ToList()
        rules = New ReadOnlyCollection(Of MotivationRule)(filtered)
    End Sub

    Public Function Evaluate(answers As IReadOnlyDictionary(Of String, Object)) As MotivationInferenceResult
        Dim safeAnswers As IReadOnlyDictionary(Of String, Object) = answers
        If safeAnswers Is Nothing Then
            safeAnswers = New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
        End If

        Dim scores = CategoryKeys.ToDictionary(Function(k) k, Function(__) 0.0R, StringComparer.OrdinalIgnoreCase)
        Dim evidence = CategoryKeys.ToDictionary(Function(k) k, Function(__) New List(Of String)(), StringComparer.OrdinalIgnoreCase)

        For Each rule In rules
            Dim weight = ExtractAnswerWeight(safeAnswers, rule.FactKey)
            If weight <= 0 Then Continue For
            Dim contribution = Clamp(rule.Certainty) * weight
            Dim categoryKey = SanitizeCategory(rule.Category)
            Dim current = scores(categoryKey)
            scores(categoryKey) = CombineCertainty(current, contribution)
            evidence(categoryKey).Add(BuildEvidenceText(rule))
        Next

        Dim primary = scores.OrderByDescending(Function(kvp) kvp.Value).Select(Function(kvp) kvp.Key).FirstOrDefault()
        If String.IsNullOrWhiteSpace(primary) Then
            primary = "nAch"
        End If

        Return New MotivationInferenceResult With {
            .PrimaryCategory = primary,
            .Scores = New ReadOnlyDictionary(Of String, Double)(scores),
            .Evidence = New ReadOnlyDictionary(Of String, List(Of String))(evidence)
        }
    End Function

    Private Shared Function BuildEvidenceText(rule As MotivationRule) As String
        Dim marker = If(String.IsNullOrWhiteSpace(rule.RuleCode), rule.FactKey, rule.RuleCode)
        Dim prompt = If(String.IsNullOrWhiteSpace(rule.Prompt), "Jawaban positif", rule.Prompt)
        Return $"{marker} → {prompt}"
    End Function

    Private Shared Function ExtractAnswerWeight(answers As IReadOnlyDictionary(Of String, Object), factKey As String) As Double
        If answers Is Nothing OrElse String.IsNullOrWhiteSpace(factKey) Then Return 0
        Dim key = factKey
        Dim raw As Object = Nothing
        If Not answers.TryGetValue(key, raw) Then
            ' Allow matching by case-insensitive search when necessary
            Dim match = answers.Keys.FirstOrDefault(Function(k) String.Equals(k, key, StringComparison.OrdinalIgnoreCase))
            If match Is Nothing Then Return 0
            raw = answers(match)
        End If

        If raw Is Nothing Then Return 0

        If TypeOf raw Is Boolean Then
            Return If(CBool(raw), 1.0R, 0.0R)
        End If

        If TypeOf raw Is Double Then Return Clamp(CDbl(raw))
        If TypeOf raw Is Single Then Return Clamp(CSng(raw))
        If TypeOf raw Is Decimal Then Return Clamp(Convert.ToDouble(raw))
        If TypeOf raw Is Integer Then Return Clamp(CDbl(CInt(raw)))

        Dim dict = TryCast(raw, IDictionary)
        If dict IsNot Nothing Then
            Dim numericValues = dict.Values.Cast(Of Object)().Select(Function(v) TryConvertToDouble(v)).Where(Function(v) v.HasValue).Select(Function(v) v.Value).ToList()
            If numericValues.Count = 0 Then Return 0
            Return Clamp(numericValues.Average())
        End If

        Dim enumerable = TryCast(raw, IEnumerable)
        If enumerable IsNot Nothing AndAlso Not TypeOf raw Is String Then
            Dim numericValues = enumerable.Cast(Of Object)().Select(Function(v) TryConvertToDouble(v)).Where(Function(v) v.HasValue).Select(Function(v) v.Value).ToList()
            If numericValues.Count = 0 Then Return 0
            Return Clamp(numericValues.Average())
        End If

        Dim text = raw.ToString().Trim().ToLowerInvariant()
        If text.Length = 0 Then Return 0
        If text = "ya" OrElse text = "yes" OrElse text = "y" Then Return 1.0R
        If text = "tidak" OrElse text = "no" OrElse text = "n" Then Return 0
        If text = "1" OrElse text = "true" Then Return 1.0R
        If text = "0" OrElse text = "false" Then Return 0

        Dim parsed As Double
        If Double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, parsed) Then
            Return Clamp(parsed)
        End If

        Return 0
    End Function

    Private Shared Function TryConvertToDouble(value As Object) As Double?
        If value Is Nothing Then Return Nothing
        If TypeOf value Is Double Then Return CDbl(value)
        If TypeOf value Is Single Then Return CSng(value)
        If TypeOf value Is Decimal Then Return Convert.ToDouble(value)
        If TypeOf value Is Integer Then Return CDbl(CInt(value))
        If TypeOf value Is Boolean Then Return If(CBool(value), 1.0R, 0.0R)
        Dim text = value.ToString().Trim()
        Dim parsed As Double
        If Double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, parsed) Then
            Return parsed
        End If
        Return Nothing
    End Function

    Private Shared Function CombineCertainty(existing As Double, incoming As Double) As Double
        If existing = 0 Then Return incoming
        If existing > 0 AndAlso incoming > 0 Then
            Return existing + incoming * (1 - existing)
        End If
        If existing < 0 AndAlso incoming < 0 Then
            Return existing + incoming * (1 + existing)
        End If
        Dim numerator = existing + incoming
        Dim denominator = 1 - Math.Min(Math.Abs(existing), Math.Abs(incoming))
        If denominator = 0 Then Return 0
        Return numerator / denominator
    End Function

    Private Shared Function Clamp(value As Double) As Double
        If Double.IsNaN(value) Then Return 0
        If value > 1 Then Return 1
        If value < -1 Then Return -1
        Return value
    End Function

    Private Shared Function SanitizeCategory(category As String) As String
        If String.IsNullOrWhiteSpace(category) Then Return "nAch"
        Dim normalized = category.Trim()
        If CategoryKeys.Contains(normalized, StringComparer.OrdinalIgnoreCase) Then
            Return CategoryKeys.First(Function(k) String.Equals(k, normalized, StringComparison.OrdinalIgnoreCase))
        End If
        Return "nAch"
    End Function
End Class

Public NotInheritable Class MotivationRuleRepository
    Private Shared embeddedRules As IReadOnlyList(Of MotivationRule)

    Public Shared Function FromQuestionBank(questionBank As IEnumerable(Of QuestionModel)) As IReadOnlyList(Of MotivationRule)
        If questionBank Is Nothing Then
            Return Array.Empty(Of MotivationRule)()
        End If

        Dim mapped = questionBank _
            .Where(Function(q) q IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(q.QKey)) _
            .Select(Function(q) New MotivationRule With {
                .FactKey = q.QKey.Trim(),
                .Category = If(String.IsNullOrWhiteSpace(q.Category), DetermineCategory(q.QKey), q.Category),
                .Certainty = If(q.CertaintyFactor = 0, 0.75R, q.CertaintyFactor),
                .RuleCode = If(String.IsNullOrWhiteSpace(q.RuleCode), q.QKey.Trim(), q.RuleCode.Trim()),
                .Prompt = q.Prompt
            }).ToList()

        Return New ReadOnlyCollection(Of MotivationRule)(mapped)
    End Function

    Public Shared Function LoadEmbeddedRules() As IReadOnlyList(Of MotivationRule)
        If embeddedRules Is Nothing Then
            embeddedRules = New ReadOnlyCollection(Of MotivationRule)(ParseQuestionnaireAsset().ToList())
        End If
        Return embeddedRules
    End Function

    Private Shared Function ParseQuestionnaireAsset() As IEnumerable(Of MotivationRule)
        Dim list As New List(Of MotivationRule)()
        Try
            Dim baseDir = AppDomain.CurrentDomain.BaseDirectory
            Dim assetPath = Path.Combine(baseDir, "assets", "mcclelland_questionnaire.txt")
            If Not File.Exists(assetPath) Then
                Return list
            End If

            For Each rawLine In File.ReadAllLines(assetPath)
                Dim line = rawLine.Trim()
                If String.IsNullOrWhiteSpace(line) OrElse line.StartsWith("#") Then Continue For
                Dim parts = line.Split("|"c)
                If parts.Length < 4 Then Continue For

                Dim factKey = parts(0).Trim()
                Dim ruleCode = parts(1).Trim()
                Dim cfValue As Double
                If Not Double.TryParse(parts(2).Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, cfValue) Then
                    cfValue = 0.75R
                End If
                Dim prompt = parts(3).Trim()

                list.Add(New MotivationRule With {
                    .FactKey = factKey,
                    .RuleCode = ruleCode,
                    .Certainty = cfValue,
                    .Category = DetermineCategory(factKey),
                    .Prompt = prompt
                })
            Next
        Catch
            ' swallow read errors; fallback to empty rules
        End Try
        Return list
    End Function

    Private Shared Function DetermineCategory(factKey As String) As String
        If String.IsNullOrWhiteSpace(factKey) Then Return "nAch"
        If factKey.StartsWith("F_A", StringComparison.OrdinalIgnoreCase) Then Return "nAch"
        If factKey.StartsWith("F_B", StringComparison.OrdinalIgnoreCase) Then Return "nAff"
        Return "nPow"
    End Function
End Class
