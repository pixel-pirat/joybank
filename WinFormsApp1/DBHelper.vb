Imports MySql.Data.MySqlClient
Imports System.Data

Public Class DBHelper

    Public Shared Function GetData(query As String) As DataTable
        Dim table As New DataTable()

        Try
            Using conn = DBConnection.GetConnection()
                conn.Open()
                Using adapter As New MySqlDataAdapter(query, conn)
                    adapter.Fill(table)
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

        Return table
    End Function

End Class