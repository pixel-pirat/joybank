Public Class customer


    Private Sub customer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomers()
    End Sub

    Private Sub LoadCustomers()
        Try
            Dim query As String = "SELECT * FROM Customers"
            dgvCustomers.DataSource = DBHelper.GetData(query)

            ' Optional formatting
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvCustomers.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error loading customers: " & ex.Message)
        End Try
    End Sub

End Class
