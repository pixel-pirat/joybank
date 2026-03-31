Public Class loanRepayment

    Private Sub loanrepayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLoanRepayment()
    End Sub

    Private Sub LoadLoanRepayment()
        Try
            Dim query As String = "SELECT * FROM LoanRepayments"
            Guna2DataGridView1.DataSource = DBHelper.GetData(query)

            ' Optional formatting
            Guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Guna2DataGridView1.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error loading loanrepayments: " & ex.Message)
        End Try
    End Sub
End Class
