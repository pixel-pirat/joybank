Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Asn1.Cmp

Public Class loanMgt




    ' =========================
    ' FORM LOAD
    ' =========================
    Private Sub loanForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadCustomers()
            LoadLoanTypes()
            LoadStatus()
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
        ' LOAD LOAN TYPES
        ' =========================
        Private Sub LoadLoanTypes()
            Dim query As String = "SELECT LoanTypeID, LoanTypeName FROM LoanTypes"
            Dim table = DBHelper.GetData(query)

            cmbLoanType.DataSource = table
            cmbLoanType.DisplayMember = "LoanTypeName"
            cmbLoanType.ValueMember = "LoanTypeID"
        End Sub

        ' =========================
        ' LOAD STATUS OPTIONS
        ' =========================
        Private Sub LoadStatus()
            cmbStatus.Items.Clear()
            cmbStatus.Items.Add("Pending")
            cmbStatus.Items.Add("Approved")
            cmbStatus.Items.Add("Rejected")
        End Sub

        ' =========================
        ' AUTO LOAD ACCOUNT NUMBER WHEN CUSTOMER SELECTED
        ' =========================
        Private Sub cmbCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCustomer.SelectedIndexChanged

            If cmbCustomer.SelectedValue Is Nothing Then Exit Sub

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    Dim query As String = "
                    SELECT AccountNumber
                    FROM Accounts
                    WHERE CustomerID = @cust
                    LIMIT 1
                "

                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@cust", cmbCustomer.SelectedValue)

                        Dim result = cmd.ExecuteScalar()

                        If result IsNot Nothing Then
                            txtAccountNumber.Text = result.ToString()
                        Else
                            txtAccountNumber.Text = "No Account Found"
                        End If
                    End Using
                End Using

            Catch ex As Exception
                MessageBox.Show("Error loading account: " & ex.Message)
            End Try

        End Sub

        ' =========================
        ' VALIDATION FUNCTION
        ' =========================
        Private Function ValidateForm() As Boolean

            If cmbCustomer.SelectedIndex = -1 Then
                MessageBox.Show("Select a customer.")
                Return False
            End If

            If cmbLoanType.SelectedIndex = -1 Then
                MessageBox.Show("Select loan type.")
                Return False
            End If

            If txtAmount.Text = "" Or Not IsNumeric(txtAmount.Text) Then
                MessageBox.Show("Enter valid loan amount.")
                Return False
            End If

            If cmbStatus.SelectedIndex = -1 Then
                MessageBox.Show("Select loan status.")
                Return False
            End If

            If dtpEndDate.Value <= dtpStartDate.Value Then
                MessageBox.Show("End date must be after start date.")
                Return False
            End If

            Return True

        End Function

        ' =========================
        ' PROCESS APPLICATION
        ' =========================
        Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click

            If Not ValidateForm() Then Exit Sub

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    ' Get interest rate from LoanTypes
                    Dim interestRate As Decimal = 0

                    Dim rateQuery As String = "SELECT InterestRate FROM LoanTypes WHERE LoanTypeID = @type"

                    Using rateCmd As New MySqlCommand(rateQuery, conn)
                        rateCmd.Parameters.AddWithValue("@type", cmbLoanType.SelectedValue)
                        interestRate = Convert.ToDecimal(rateCmd.ExecuteScalar())
                    End Using

                    ' Insert Loan
                    Dim insertQuery As String = "
                    INSERT INTO Loans
                    (CustomerID, LoanTypeID, LoanAmount, InterestRate, LoanStartDate, LoanEndDate, LoanStatus)
                    VALUES
                    (@cust, @type, @amount, @rate, @start, @end, @status)
                "

                    Using cmd As New MySqlCommand(insertQuery, conn)

                        cmd.Parameters.AddWithValue("@cust", cmbCustomer.SelectedValue)
                        cmd.Parameters.AddWithValue("@type", cmbLoanType.SelectedValue)
                        cmd.Parameters.AddWithValue("@amount", Convert.ToDecimal(txtAmount.Text))
                        cmd.Parameters.AddWithValue("@rate", interestRate)
                        cmd.Parameters.AddWithValue("@start", dtpStartDate.Value)
                        cmd.Parameters.AddWithValue("@end", dtpEndDate.Value)
                        cmd.Parameters.AddWithValue("@status", cmbStatus.Text)

                        cmd.ExecuteNonQuery()

                    End Using

                End Using

                MessageBox.Show("Loan application processed successfully!")

                ClearForm()

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try

        End Sub

        ' =========================
        ' CLEAR BUTTON
        ' =========================
        Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
            ClearForm()
        End Sub

        ' =========================
        ' CLEAR FUNCTION
        ' =========================
        Private Sub ClearForm()
            cmbCustomer.SelectedIndex = -1
            cmbLoanType.SelectedIndex = -1
            cmbStatus.SelectedIndex = -1

            txtAmount.Clear()


        dtpStartDate.Value = Date.Now
            dtpEndDate.Value = Date.Now.AddMonths(1)
        End Sub

End Class