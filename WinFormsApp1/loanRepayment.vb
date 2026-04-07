Imports MySql.Data.MySqlClient

Public Class loanRepayment
    Private selectedLoanID As Integer = 0
    Private currentBalance As Decimal = 0
    Private Sub loanrepayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLoanRepayment()

        LoadLoans()
        LoadDashboardStats()

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

        ' =========================
        ' LOAD LOANS INTO COMBOBOX
        ' =========================
        Private Sub LoadLoans()

            Dim query As String = "
            SELECT l.LoanID,
            CONCAT(c.FirstName, ' ', c.LastName, ' - GHS ', l.LoanAmount) AS DisplayName
            FROM Loans l
            JOIN Customers c ON l.CustomerID = c.CustomerID
            WHERE l.LoanStatus = 'Approved'
        "

            Dim table = DBHelper.GetData(query)

            cmbLoan.DataSource = table
            cmbLoan.DisplayMember = "DisplayName"
            cmbLoan.ValueMember = "LoanID"

        End Sub

        ' =========================
        ' LOAD BALANCE WHEN SELECTED
        ' =========================
        Private Sub cmbLoan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLoan.SelectedIndexChanged
        If TypeOf cmbLoan.SelectedValue Is Integer Then
            selectedLoanID = Convert.ToInt32(cmbLoan.SelectedValue)
        Else
            Exit Sub
        End If

        Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    ' Total Loan Amount
                    Dim loanCmd As New MySqlCommand("SELECT LoanAmount FROM Loans WHERE LoanID = @id", conn)
                    loanCmd.Parameters.AddWithValue("@id", selectedLoanID)

                    Dim loanAmount As Decimal = Convert.ToDecimal(loanCmd.ExecuteScalar())

                    ' Total Paid
                    Dim paidCmd As New MySqlCommand("
                    SELECT IFNULL(SUM(AmountPaid),0) 
                    FROM LoanRepayments 
                    WHERE LoanID = @id
                ", conn)

                    paidCmd.Parameters.AddWithValue("@id", selectedLoanID)

                    Dim totalPaid As Decimal = Convert.ToDecimal(paidCmd.ExecuteScalar())

                    currentBalance = loanAmount - totalPaid

                    lblAmountLeft.Text = currentBalance.ToString("N2")

                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        ' =========================
        ' PAY LOAN BUTTON
        ' =========================
        Private Sub btnPayLoan_Click(sender As Object, e As EventArgs) Handles btnPayLoan.Click

            If selectedLoanID = 0 Then
                MessageBox.Show("Select a loan.")
                Exit Sub
            End If

            If txtAmountPaid.Text = "" Or Not IsNumeric(txtAmountPaid.Text) Then
                MessageBox.Show("Enter valid amount.")
                Exit Sub
            End If

            Dim payment As Decimal = Convert.ToDecimal(txtAmountPaid.Text)

            If payment <= 0 Then
                MessageBox.Show("Amount must be greater than zero.")
                Exit Sub
            End If

            If payment > currentBalance Then
                MessageBox.Show("Cannot pay more than remaining balance.")
                Exit Sub
            End If

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    Dim dbTransaction = conn.BeginTransaction()

                    Try
                        ' INSERT REPAYMENT
                        Dim insertCmd As New MySqlCommand("
                        INSERT INTO LoanRepayments
                        (LoanID, PaymentDate, AmountPaid, RemainingBalance)
                        VALUES
                        (@loan, @date, @amount, @remain)
                    ", conn, dbTransaction)

                        Dim newBalance = currentBalance - payment

                        insertCmd.Parameters.AddWithValue("@loan", selectedLoanID)
                        insertCmd.Parameters.AddWithValue("@date", dtpPaymentDate.Value)
                        insertCmd.Parameters.AddWithValue("@amount", payment)
                        insertCmd.Parameters.AddWithValue("@remain", newBalance)

                        insertCmd.ExecuteNonQuery()

                        ' OPTIONAL: MARK LOAN AS PAID
                        If newBalance = 0 Then
                            Dim updateLoan As New MySqlCommand("
                            UPDATE Loans SET LoanStatus = 'Completed'
                            WHERE LoanID = @id
                        ", conn, dbTransaction)

                            updateLoan.Parameters.AddWithValue("@id", selectedLoanID)
                            updateLoan.ExecuteNonQuery()
                        End If

                        dbTransaction.Commit()

                        MessageBox.Show("Payment successful!")

                        ' Refresh
                        txtAmountPaid.Clear()
                        cmbLoan_SelectedIndexChanged(Nothing, Nothing)
                        LoadDashboardStats()

                    Catch ex As Exception
                        dbTransaction.Rollback()
                        MessageBox.Show("Transaction failed: " & ex.Message)
                    End Try

                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        ' =========================
        ' DASHBOARD STATS (RIGHT SIDE)
        ' =========================
        Private Sub LoadDashboardStats()

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    ' MTD COLLECTIONS
                    Dim cmdMTD As New MySqlCommand("
                    SELECT IFNULL(SUM(AmountPaid),0)
                    FROM LoanRepayments
                    WHERE MONTH(PaymentDate) = MONTH(CURRENT_DATE())
                    AND YEAR(PaymentDate) = YEAR(CURRENT_DATE())
                ", conn)

                    lblMTD.Text = Convert.ToDecimal(cmdMTD.ExecuteScalar()).ToString("N2")

                    ' PENDING LOANS
                    Dim cmdPending As New MySqlCommand("
                    SELECT IFNULL(SUM(LoanAmount),0)
                    FROM Loans
                    WHERE LoanStatus = 'Approved'
                ", conn)

                    lblPending.Text = Convert.ToDecimal(cmdPending.ExecuteScalar()).ToString("N2")

                    ' CRITICAL ACCOUNTS (example: unpaid > 50%)
                    Dim cmdCritical As New MySqlCommand("
                    SELECT COUNT(*) 
                    FROM Loans l
                    WHERE l.LoanStatus = 'Approved'
                ", conn)

                    lblCritical.Text = cmdCritical.ExecuteScalar().ToString() & " Critical Accounts"

                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

End Class

