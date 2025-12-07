Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Module DatabaseHelper

    Private connectionString As String = "server=localhost;userid=root;password=;database=thesis_buddy;"

    Public Function GetConnection() As MySqlConnection
        Return New MySqlConnection(connectionString)
    End Function

    Public Function GetAllActiveQuestions() As List(Of QuestionModel)
        Dim list As New List(Of QuestionModel)()
        Dim queries As String() = {
            "SELECT id, qkey, prompt, qtype, options, step, active, category, rule_code, cf_value FROM questions WHERE active = 1 ORDER BY step, id",
            "SELECT id, qkey, prompt, qtype, options, qstep, active, category, rule_code, cf_value FROM questions WHERE active = 1 ORDER BY qstep, id"
        }

        Try
            Using conn As MySqlConnection = GetConnection()
                conn.Open()
                For i As Integer = 0 To queries.Length - 1
                    Try
                        Using cmd As New MySqlCommand(queries(i), conn)
                            Using rdr As MySqlDataReader = cmd.ExecuteReader()
                                While rdr.Read()
                                    Dim usingStep As Boolean = (i = 0)
                                    list.Add(MapQuestion(rdr, usingStep))
                                End While
                            End Using
                        End Using

                        If list.Count > 0 OrElse i = 0 Then Exit For
                    Catch ex As MySqlException
                        list.Clear()
                        If i = queries.Length - 1 Then Throw
                    End Try
                Next
            End Using
        Catch ex2 As Exception
            ' swallow; caller can handle empty list
        End Try

        Return list
    End Function

    Public Sub SeedMcClellandQuestionnaire(Optional forceReset As Boolean = False)
        Try
            EnsureQuestionsTable()
            Dim assetPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "mcclelland_questionnaire.txt")
            If Not File.Exists(assetPath) Then Return

            Dim parsedQuestions = ParseMcClellandQuestionnaire(assetPath)
            If parsedQuestions.Count = 0 Then Return

            Using conn As MySqlConnection = GetConnection()
                conn.Open()
                Dim countSql As String = "SELECT COUNT(*) FROM questions WHERE rule_code IS NOT NULL"
                Dim existingCount As Integer = 0
                Using countCmd As New MySqlCommand(countSql, conn)
                    existingCount = Convert.ToInt32(countCmd.ExecuteScalar())
                End Using

                If existingCount >= parsedQuestions.Count AndAlso Not forceReset Then
                    Return
                End If

                If forceReset Then
                    Using deleteCmd As New MySqlCommand("DELETE FROM questions WHERE rule_code IS NOT NULL", conn)
                        deleteCmd.ExecuteNonQuery()
                    End Using
                End If

                For Each pq In parsedQuestions
                    Dim insertSql As String = "INSERT INTO questions (qkey, prompt, qtype, options, step, active, category, rule_code, cf_value) " &
                                              "VALUES (@qkey, @prompt, @qtype, NULL, @step, 1, @category, @rule, @cf) " &
                                              "ON DUPLICATE KEY UPDATE prompt = VALUES(prompt), qtype = VALUES(qtype), options = VALUES(options), " &
                                              "step = VALUES(step), active = VALUES(active), category = VALUES(category), rule_code = VALUES(rule_code), cf_value = VALUES(cf_value)"
                    Using insertCmd As New MySqlCommand(insertSql, conn)
                        insertCmd.Parameters.AddWithValue("@qkey", pq.QKey)
                        insertCmd.Parameters.AddWithValue("@prompt", pq.Prompt)
                        insertCmd.Parameters.AddWithValue("@qtype", "yesno")
                        insertCmd.Parameters.AddWithValue("@step", pq.StepIndex)
                        insertCmd.Parameters.AddWithValue("@category", pq.Category)
                        insertCmd.Parameters.AddWithValue("@rule", pq.RuleCode)
                        insertCmd.Parameters.AddWithValue("@cf", pq.CfValue)
                        insertCmd.ExecuteNonQuery()
                    End Using
                Next
            End Using
        Catch ex As Exception
            ' swallow seeding errors to avoid blocking UI
        End Try
    End Sub

    Private Function ParseMcClellandQuestionnaire(filePath As String) As List(Of ParsedQuestion)
        Dim results As New List(Of ParsedQuestion)()
        Dim counters As New Dictionary(Of String, Integer) From {{"nAch", 0}, {"nAff", 0}, {"nPow", 0}}
        Dim offsets As New Dictionary(Of String, Integer) From {{"nAch", 0}, {"nAff", 5}, {"nPow", 10}}

        For Each rawLine In File.ReadAllLines(filePath)
            Dim line As String = rawLine.Trim()
            If String.IsNullOrWhiteSpace(line) OrElse line.StartsWith("#") Then Continue For

            Dim parts = line.Split("|"c)
            If parts.Length < 4 Then Continue For

            Dim qKey As String = parts(0).Trim()
            Dim ruleCode As String = parts(1).Trim()
            Dim cfValue As Double
            Dim prompt As String = parts(3).Trim()

            If Not Double.TryParse(parts(2).Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, cfValue) Then
                cfValue = 0
            End If

            Dim category As String = DetermineCategory(qKey)
            counters(category) += 1
            Dim blockIndex As Integer = (counters(category) - 1) \ 10
            Dim stepIndex As Integer = 1 + offsets(category) + blockIndex

            Dim parsed As New ParsedQuestion() With {
                .QKey = qKey,
                .Prompt = prompt,
                .RuleCode = ruleCode,
                .Category = category,
                .CfValue = cfValue,
                .StepIndex = stepIndex
            }
            results.Add(parsed)
        Next

        Return results
    End Function

    Private Function DetermineCategory(qKey As String) As String
        If String.IsNullOrWhiteSpace(qKey) Then Return "nAch"
        If qKey.StartsWith("F_A", StringComparison.OrdinalIgnoreCase) Then
            Return "nAch"
        ElseIf qKey.StartsWith("F_B", StringComparison.OrdinalIgnoreCase) Then
            Return "nAff"
        Else
            Return "nPow"
        End If
    End Function

    Private Class ParsedQuestion
        Public Property QKey As String
        Public Property Prompt As String
        Public Property RuleCode As String
        Public Property Category As String
        Public Property CfValue As Double
        Public Property StepIndex As Integer
    End Class

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim salt As String = "ThesisBuddySalt" ' Simple salt, in production use random salt
            Dim combined As String = password & salt
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(combined)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Return Convert.ToBase64String(hash)
        End Using
    End Function

    Public Function LoginUser(username As String, password As String) As Boolean
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim query As String = "SELECT password FROM users WHERE username = @username"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@username", username)
                Dim hashedPassword As String = Convert.ToString(cmd.ExecuteScalar())
                If String.IsNullOrEmpty(hashedPassword) Then Return False
                Return hashedPassword = HashPassword(password)
            End Using
        End Using
    End Function

    Public Function RegisterUser(username As String, password As String) As Boolean
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            ' Check if exists
            Dim checkQuery As String = "SELECT COUNT(*) FROM users WHERE username = @username"
            Using checkCmd As New MySqlCommand(checkQuery, conn)
                checkCmd.Parameters.AddWithValue("@username", username)
                Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                If count > 0 Then Return False
            End Using
            ' Hash password
            Dim hashedPassword As String = HashPassword(password)
            ' Insert
            Dim insertQuery As String = "INSERT INTO users (username, password) VALUES (@username, @password)"
            Using insertCmd As New MySqlCommand(insertQuery, conn)
                insertCmd.Parameters.AddWithValue("@username", username)
                insertCmd.Parameters.AddWithValue("@password", hashedPassword)
                insertCmd.ExecuteNonQuery()
                Return True
            End Using
        End Using
    End Function

    ' --- Consultation history methods ---
    Public Sub EnsureConsultationTable()
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim sql As String = "CREATE TABLE IF NOT EXISTS consultations (" &
                                "id INT AUTO_INCREMENT PRIMARY KEY, " &
                                "username VARCHAR(255), " &
                                "timestamp DATETIME, " &
                                "answers TEXT, " &
                                "recommendations TEXT) ENGINE=InnoDB;"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub DropConsultationTable()
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim sql As String = "DROP TABLE IF EXISTS consultations;"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' Update schema by dropping and recreating the consultations table
    Public Sub UpdateConsultationTable()
        DropConsultationTable()
        EnsureConsultationTable()
    End Sub

    Public Function SaveConsultation(username As String, answers As String, recommendations As String) As Boolean
        Try
            EnsureConsultationTable()
            Using conn As MySqlConnection = GetConnection()
                conn.Open()
                Dim insertQuery As String = "INSERT INTO consultations (username, timestamp, answers, recommendations) VALUES (@username, @timestamp, @answers, @recs)"
                Using cmd As New MySqlCommand(insertQuery, conn)
                    cmd.Parameters.AddWithValue("@username", username)
                    cmd.Parameters.AddWithValue("@timestamp", DateTime.Now)
                    cmd.Parameters.AddWithValue("@answers", answers)
                    cmd.Parameters.AddWithValue("@recs", recommendations)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            Return True
        Catch ex As Exception
            ' In production log error
            Return False
        End Try
    End Function

    ' --- Questions table management ---
    Public Sub EnsureQuestionsTable()
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim sql As String = "CREATE TABLE IF NOT EXISTS questions (" & _
                                "id INT AUTO_INCREMENT PRIMARY KEY, " & _
                                "qkey VARCHAR(255), " & _
                                "prompt TEXT, " & _
                                "qtype VARCHAR(50), " & _
                                "options TEXT, " & _
                                "step INT, " & _
                                "active TINYINT(1) DEFAULT 1, " & _
                                "category VARCHAR(20), " & _
                                "rule_code VARCHAR(50), " & _
                                "cf_value DOUBLE) ENGINE=InnoDB;"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using

            EnsureColumnExists(conn, "questions", "step", "INT")
            EnsureColumnExists(conn, "questions", "category", "VARCHAR(20)")
            EnsureColumnExists(conn, "questions", "rule_code", "VARCHAR(50)")
            EnsureColumnExists(conn, "questions", "cf_value", "DOUBLE")
            EnsureIndexExists(conn, "questions", "ux_questions_qkey", "UNIQUE KEY ux_questions_qkey (qkey)")
        End Using
    End Sub

    Private Sub EnsureColumnExists(conn As MySqlConnection, tableName As String, columnName As String, columnDefinition As String)
        Dim checkSql As String = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @table AND COLUMN_NAME = @column"
        Using checkCmd As New MySqlCommand(checkSql, conn)
            checkCmd.Parameters.AddWithValue("@table", tableName)
            checkCmd.Parameters.AddWithValue("@column", columnName)
            Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
            If exists = 0 Then
                Dim alterSql As String = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnDefinition}"
                Using alterCmd As New MySqlCommand(alterSql, conn)
                    alterCmd.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

    Private Sub EnsureIndexExists(conn As MySqlConnection, tableName As String, indexName As String, indexDefinition As String)
        Dim checkSql As String = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.STATISTICS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @table AND INDEX_NAME = @index"
        Using checkCmd As New MySqlCommand(checkSql, conn)
            checkCmd.Parameters.AddWithValue("@table", tableName)
            checkCmd.Parameters.AddWithValue("@index", indexName)
            Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
            If exists = 0 Then
                Dim createSql As String = $"ALTER TABLE {tableName} ADD {indexDefinition}"
                Using createCmd As New MySqlCommand(createSql, conn)
                    createCmd.ExecuteNonQuery()
                End Using
            End If
        End Using
    End Sub

    Public Sub DropQuestionsTable()
        Using conn As MySqlConnection = GetConnection()
            conn.Open()
            Dim sql As String = "DROP TABLE IF EXISTS questions;"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub UpdateQuestionsTable()
        DropQuestionsTable()
        EnsureQuestionsTable()
    End Sub

    Public Function GetQuestionsByStep(stepIndex As Integer) As List(Of QuestionModel)
        Dim list As New List(Of QuestionModel)()
        Try
            Using conn As MySqlConnection = GetConnection()
                conn.Open()
                Dim triedLegacy As Boolean = False

                Do
                    Dim sql As String
                    If Not triedLegacy Then
                        sql = "SELECT id, qkey, prompt, qtype, options, step, active, category, rule_code, cf_value FROM questions WHERE step = @step AND active = 1 ORDER BY id"
                    Else
                        sql = "SELECT id, qkey, prompt, qtype, options, qstep, active, category, rule_code, cf_value FROM questions WHERE qstep = @step AND active = 1 ORDER BY id"
                    End If

                    Try
                        Using cmd As New MySqlCommand(sql, conn)
                            cmd.Parameters.AddWithValue("@step", stepIndex)
                            Using rdr As MySqlDataReader = cmd.ExecuteReader()
                                While rdr.Read()
                                    Dim m As QuestionModel = MapQuestion(rdr, Not triedLegacy)
                                    list.Add(m)
                                End While
                            End Using
                        End Using

                        Exit Do
                    Catch ex As MySqlException
                        If triedLegacy Then Exit Do
                        triedLegacy = True
                    End Try
                Loop
            End Using
        Catch ex As Exception
            ' ignore
        End Try
        Return list
    End Function

    Private Function MapQuestion(rdr As MySqlDataReader, usingStepColumn As Boolean) As QuestionModel
        Dim m As New QuestionModel()
        m.Id = Convert.ToInt32(rdr("id"))
        m.QKey = Convert.ToString(rdr("qkey"))
        m.Prompt = Convert.ToString(rdr("prompt"))
        m.QType = Convert.ToString(rdr("qtype"))
        m.Options = Convert.ToString(rdr("options"))
        Dim stepColumn As String = If(usingStepColumn, "step", "qstep")
        Dim stepOrdinal As Integer = -1
        Try
            stepOrdinal = rdr.GetOrdinal(stepColumn)
        Catch
            stepOrdinal = -1
        End Try
        If stepOrdinal >= 0 AndAlso Not rdr.IsDBNull(stepOrdinal) Then
            m.QStep = Convert.ToInt32(rdr(stepOrdinal))
        End If
        m.Active = Convert.ToInt32(rdr("active")) = 1

        Dim categoryOrdinal As Integer = -1
        Dim ruleOrdinal As Integer = -1
        Dim cfOrdinal As Integer = -1
        Try
            categoryOrdinal = rdr.GetOrdinal("category")
            ruleOrdinal = rdr.GetOrdinal("rule_code")
            cfOrdinal = rdr.GetOrdinal("cf_value")
        Catch
        End Try

        If categoryOrdinal >= 0 AndAlso Not rdr.IsDBNull(categoryOrdinal) Then
            m.Category = Convert.ToString(rdr(categoryOrdinal))
        End If
        If ruleOrdinal >= 0 AndAlso Not rdr.IsDBNull(ruleOrdinal) Then
            m.RuleCode = Convert.ToString(rdr(ruleOrdinal))
        End If
        If cfOrdinal >= 0 AndAlso Not rdr.IsDBNull(cfOrdinal) Then
            m.CertaintyFactor = Convert.ToDouble(rdr(cfOrdinal))
        End If

        Return m
    End Function

End Module