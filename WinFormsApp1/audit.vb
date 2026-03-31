Public Class audit
    Private Sub audit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAudit()
    End Sub

    Private Sub LoadAudit()
        Try
            Dim query As String = "SELECT * FROM auditlogs"
            Guna2DataGridView1.DataSource = DBHelper.GetData(query)

            ' Optional formatting
            Guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Guna2DataGridView1.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error loading audit logs: " & ex.Message)
        End Try
    End Sub
End Class
