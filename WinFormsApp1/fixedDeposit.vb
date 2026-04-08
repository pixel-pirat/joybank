Imports MySql.Data.MySqlClient

Public Class fixedDeposit


    Private selectedCustomerID As Integer = 0
        Private selectedAccountID As Integer = 0

        ' =========================
        ' FORM LOAD
        ' =========================
        Private Sub FixedDepositForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadCustomers()
            LoadStatus()
            LoadInterestRates()
        End Sub

        ' =========================
        ' LOAD CUSTOMERS
        ' =========================
        Private Sub LoadCustomers()
            Dim query As String = "
            SELECT CustomerID, CONCAT(FirstName, ' ', LastName) AS FullName 
            FROM Customers
        "

            Dim table = DBHelper.GetData(query)

            cmbCustomer.DataSource = table
            cmbCustomer.DisplayMember = "FullName"
            cmbCustomer.ValueMember = "CustomerID"
        End Sub

        ' =========================
        ' LOAD STATUS
        ' =========================
        Private Sub LoadStatus()
            cmbStatus.Items.Clear()
            cmbStatus.Items.Add("Active")
            cmbStatus.Items.Add("Pending")
            cmbStatus.Items.Add("Closed")
        End Sub

        ' =========================
        ' LOAD INTEREST RATES
        ' =========================
        Private Sub LoadInterestRates()
            cmbInterestRate.Items.Clear()
            cmbInterestRate.Items.Add("5")
            cmbInterestRate.Items.Add("10")
            cmbInterestRate.Items.Add("15")
        End Sub

        ' =========================
        ' WHEN CUSTOMER SELECTED
        ' =========================
        Private Sub cmbCustomer_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbCustomer.SelectionChangeCommitted

            selectedCustomerID = Convert.ToInt32(cmbCustomer.SelectedValue)

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    Dim cmd As New MySqlCommand("
                    SELECT AccountID, AccountNumber 
                    FROM Accounts 
                    WHERE CustomerID = @cust 
                    LIMIT 1
                ", conn)

                    cmd.Parameters.AddWithValue("@cust", selectedCustomerID)

                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            selectedAccountID = reader("AccountID")
                            txtAccountNumber.Text = reader("AccountNumber").ToString()
                        Else
                            txtAccountNumber.Text = "No Account"
                            selectedAccountID = 0
                        End If
                    End Using

                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        ' =========================
        ' CALCULATE FORECAST
        ' =========================
        Private Sub CalculateForecast()

            If txtAmount.Text = "" Or Not IsNumeric(txtAmount.Text) Then Exit Sub
            If cmbInterestRate.SelectedIndex = -1 Then Exit Sub

            Dim principal As Decimal = Convert.ToDecimal(txtAmount.Text)
            Dim rate As Decimal = Convert.ToDecimal(cmbInterestRate.Text)

            Dim months As Integer = DateDiff(DateInterval.Month, dtpStartDate.Value, dtpMaturityDate.Value)

            If months <= 0 Then Exit Sub

            ' Simple Interest Formula
            Dim interest As Decimal = (principal * rate * months) / (100 * 12)
            Dim total As Decimal = principal + interest

            ' DISPLAY
            lblProjectedYieldValue.Text = "GHS " & total.ToString("N2")

            lblInterestAccumulationDesc.Text =
            "Interest Earned: GHS " & interest.ToString("N2")

            lblLiquidityImpactDesc.Text =
            "Locked for " & months & " months"

            ' PROGRESS BAR (visual)
            Dim percent As Integer = Math.Min(100, months * 5)
            progressYield.Value = percent

        End Sub

        ' =========================
        ' AUTO RECALCULATE
        ' =========================
        Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
            CalculateForecast()
        End Sub

        Private Sub cmbInterestRate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbInterestRate.SelectedIndexChanged
            CalculateForecast()
        End Sub

        Private Sub dtpMaturityDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpMaturityDate.ValueChanged
            CalculateForecast()
        End Sub

        ' =========================
        ' EXECUTE DEPOSIT
        ' =========================
        Private Sub btnExecuteDeposit_Click(sender As Object, e As EventArgs) Handles btnExecuteDeposit.Click

            If selectedAccountID = 0 Then
                MessageBox.Show("No valid account selected.")
                Exit Sub
            End If

            If txtAmount.Text = "" Or Not IsNumeric(txtAmount.Text) Then
                MessageBox.Show("Enter valid amount.")
                Exit Sub
            End If

            If cmbInterestRate.SelectedIndex = -1 Then
                MessageBox.Show("Select interest rate.")
                Exit Sub
            End If

            If cmbStatus.SelectedIndex = -1 Then
                MessageBox.Show("Select status.")
                Exit Sub
            End If

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    Dim query As String = "
                    INSERT INTO FixedDeposits
                    (AccountID, DepositAmount, InterestRate, StartDate, MaturityDate)
                    VALUES
                    (@acc, @amount, @rate, @start, @end)
                "

                    Using cmd As New MySqlCommand(query, conn)

                        cmd.Parameters.AddWithValue("@acc", selectedAccountID)
                        cmd.Parameters.AddWithValue("@amount", Convert.ToDecimal(txtAmount.Text))
                        cmd.Parameters.AddWithValue("@rate", Convert.ToDecimal(cmbInterestRate.Text))
                        cmd.Parameters.AddWithValue("@start", dtpStartDate.Value)
                        cmd.Parameters.AddWithValue("@end", dtpMaturityDate.Value)

                        cmd.ExecuteNonQuery()

                    End Using

                End Using

                MessageBox.Show("Fixed Deposit Created Successfully!")



        Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End Sub

        ' =========================
        ' CLEAR FORM
        ' =========================
        Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

            cmbCustomer.SelectedIndex = -1
            cmbInterestRate.SelectedIndex = -1
        cmbStatus.SelectedIndex = -1

        txtAccountNumber.Text = ""
        txtAmount.Text = "0.00"

            lblProjectedYieldValue.Text = "GHS 0.00"
            lblInterestAccumulationDesc.Text = ""
            lblLiquidityImpactDesc.Text = ""

            progressYield.Value = 0

        End Sub

    End Class

