Imports MySql.Data.MySqlClient
Public Class DBConnection
    Public Shared Function GetConnection() As MySqlConnection
        Dim conn As New MySqlConnection("server=localhost;userid=root;password=Xcvbn@10104!!;database=bankingsystem")
        Return conn
    End Function

End Class
