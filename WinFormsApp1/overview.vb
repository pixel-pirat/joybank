Imports Guna
Imports MySql.Data.MySqlClient

Public Class overview
    Private Sub overview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardData()



    End Sub

        Private Sub LoadDashboardData()

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    ' =========================
                    ' TOTAL CUSTOMERS
                    ' =========================
                    Dim cmdCustomers As New MySqlCommand("SELECT COUNT(*) FROM Customers", conn)
                    lblTotalCustomers.Text = cmdCustomers.ExecuteScalar().ToString()

                    ' =========================
                    ' TOTAL ACCOUNTS
                    ' =========================
                    Dim cmdAccounts As New MySqlCommand("SELECT COUNT(*) FROM Accounts", conn)
                    lblTotalAccounts.Text = cmdAccounts.ExecuteScalar().ToString()

                    ' =========================
                    ' TOTAL BALANCE
                    ' =========================
                    Dim cmdBalance As New MySqlCommand("SELECT IFNULL(SUM(Balance),0) FROM Accounts", conn)
                    lblTotalBalance.Text = Convert.ToDecimal(cmdBalance.ExecuteScalar()).ToString("N2")

                    ' =========================
                    ' TOTAL LOANS
                    ' =========================
                    Dim cmdLoans As New MySqlCommand("SELECT COUNT(*) FROM Loans", conn)
                    lblTotalLoans.Text = cmdLoans.ExecuteScalar().ToString()

                    ' =========================
                    ' TOTAL TRANSACTIONS
                    ' =========================
                    Dim cmdTransactions As New MySqlCommand("SELECT COUNT(*) FROM Transactions", conn)
                lblTotalBalance.Text = cmdTransactions.ExecuteScalar().ToString()

            End Using

            Catch ex As Exception
                MessageBox.Show("Error loading dashboard: " & ex.Message)
            End Try

        End Sub
    Private Sub LoadChart()

        Try
            Using conn = DBConnection.GetConnection()
                conn.Open()

                Dim query As String = "
                SELECT tt.TransactionTypeName, COUNT(*) AS Total
                FROM Transactions t
                JOIN TransactionTypes tt 
                ON t.TransactionTypeID = tt.TransactionTypeID
                GROUP BY tt.TransactionTypeName
            "

                Dim da As New MySqlDataAdapter(query, conn)
                Dim dt As New DataTable
                da.Fill(dt)

                ' CLEAR
                GunaChart2.Datasets.Clear()

                ' CREATE DATASET
                Dim dataset As New Guna.Charts.WinForms.GunaPieDataset()
                dataset.Label = "Transactions"


                ' ADD DATA
                For Each row As DataRow In dt.Rows
                    dataset.DataPoints.Add(New Guna.Charts.WinForms.LPoint(
                    row("TransactionTypeName").ToString(),
                    Convert.ToInt32(row("Total"))
                ))
                Next

                ' COLORS (optional but nice)


                ' ADD DATASET
                GunaChart2.Datasets.Add(dataset)

                ' MAKE DONUT
                'dataset.CutoutPercentage = 60

                ' SHOW LEGEND
                GunaChart2.Legend.Display = True

                ' UPDATE
                GunaChart2.Update()

            End Using

        Catch ex As Exception
            MessageBox.Show("Chart Error: " & ex.Message)
        End Try

    End Sub
End Class

