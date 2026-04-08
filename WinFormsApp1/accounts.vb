Public Class accounts


    Private Sub accounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAccounts()
    End Sub

    Private Sub LoadAccounts()
        Try
            Dim query As String = "SELECT * FROM Accounts"
            Guna2DataGridView1.DataSource = DBHelper.GetData(query)

            ' Optional formatting
            Guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Guna2DataGridView1.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error loading accounts: " & ex.Message)
        End Try
    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Controls.Clear
        Dim uc As New accountForm
        uc.Dock = DockStyle.Fill
        Controls.Add(uc)
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Controls.Clear()
        Dim uc As New cardAdministration
        uc.Dock = DockStyle.Fill
        Controls.Add(uc)
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Controls.Clear()
        Dim uc As New fixedDeposit
        uc.Dock = DockStyle.Fill
        Controls.Add(uc)
    End Sub
End Class
