Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Module DatabaseHelper

    Private connectionString As String = "server=localhost;userid=root;password=;database=thesis_buddy;"

    Public Function GetConnection() As MySqlConnection
        Return New MySqlConnection(connectionString)
    End Function

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
                                "qstep INT, " & _
                                "active TINYINT(1) DEFAULT 1) ENGINE=InnoDB;"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
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
                ' First try the newer schema (qstep)
                Try
                    Dim sql As String = "SELECT id, qkey, prompt, qtype, options, qstep, active FROM questions WHERE qstep = @step AND active = 1 ORDER BY id"
                    Using cmd As New MySqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@step", stepIndex)
                        Using rdr As MySqlDataReader = cmd.ExecuteReader()
                            While rdr.Read()
                                Dim m As New QuestionModel()
                                m.Id = Convert.ToInt32(rdr("id"))
                                m.QKey = Convert.ToString(rdr("qkey"))
                                m.Prompt = Convert.ToString(rdr("prompt"))
                                m.QType = Convert.ToString(rdr("qtype"))
                                m.Options = Convert.ToString(rdr("options"))
                                m.QStep = Convert.ToInt32(rdr("qstep"))
                                m.Active = Convert.ToInt32(rdr("active")) = 1
                                list.Add(m)
                            End While
                        End Using
                    End Using
                Catch ex As MySqlException
                    ' if qstep column doesn't exist, fall back to legacy 'step' column
                End Try

                ' If no rows found, try legacy schema (step)
                If list.Count = 0 Then
                    Try
                        Dim sql2 As String = "SELECT id, qkey, prompt, qtype, options, step, active FROM questions WHERE step = @step AND active = 1 ORDER BY id"
                        Using cmd2 As New MySqlCommand(sql2, conn)
                            cmd2.Parameters.AddWithValue("@step", stepIndex)
                            Using rdr2 As MySqlDataReader = cmd2.ExecuteReader()
                                While rdr2.Read()
                                    Dim m As New QuestionModel()
                                    m.Id = Convert.ToInt32(rdr2("id"))
                                    m.QKey = Convert.ToString(rdr2("qkey"))
                                    m.Prompt = Convert.ToString(rdr2("prompt"))
                                    m.QType = Convert.ToString(rdr2("qtype"))
                                    m.Options = Convert.ToString(rdr2("options"))
                                    ' map legacy step to QStep
                                    m.QStep = Convert.ToInt32(rdr2("step"))
                                    m.Active = Convert.ToInt32(rdr2("active")) = 1
                                    list.Add(m)
                                End While
                            End Using
                        End Using
                    Catch ex2 As Exception
                        ' ignore and return what we have
                    End Try
                End If
            End Using
        Catch ex As Exception
            ' ignore
        End Try
        Return list
    End Function

End Module