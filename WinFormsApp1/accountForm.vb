Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Asn1.Cmp

Public Class accountForm


    ' Store selected account for update
    Public selectedAccountID As Integer = 0

        ' =========================
        ' FORM LOAD
        ' =========================
        Private Sub accountForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadCustomers()
            LoadAccountTypes()
            LoadBranches()
            LoadStatus()

            dtpDateOpened.Value = Date.Now
        End Sub

        ' =========================
        ' LOAD COMBOBOX DATA
        ' =========================
        Private Sub LoadCustomers()
            Try
                Dim query As String = "
                SELECT CustomerID, CONCAT(FirstName, ' ', LastName) AS FullName 
                FROM Customers
            "

                Dim table = DBHelper.GetData(query)

                cmbCustomer.DataSource = table
                cmbCustomer.DisplayMember = "FullName"
                cmbCustomer.ValueMember = "CustomerID"

            Catch ex As Exception
                MessageBox.Show("Error loading customers: " & ex.Message)
            End Try
        End Sub

        Private Sub LoadAccountTypes()
            Try
                Dim query As String = "SELECT AccountTypeID, AccountTypeName FROM AccountTypes"
                Dim table = DBHelper.GetData(query)

                cmbAccountType.DataSource = table
                cmbAccountType.DisplayMember = "AccountTypeName"
                cmbAccountType.ValueMember = "AccountTypeID"

            Catch ex As Exception
                MessageBox.Show("Error loading account types: " & ex.Message)
            End Try
        End Sub

        Private Sub LoadBranches()
            Try
                Dim query As String = "SELECT BranchID, BranchName FROM Branches"
                Dim table = DBHelper.GetData(query)

                cmbBranch.DataSource = table
                cmbBranch.DisplayMember = "BranchName"
                cmbBranch.ValueMember = "BranchID"

            Catch ex As Exception
                MessageBox.Show("Error loading branches: " & ex.Message)
            End Try
        End Sub

        Private Sub LoadStatus()
            cmbStatus.Items.Clear()
            cmbStatus.Items.Add("Active")
            cmbStatus.Items.Add("Frozen")
            cmbStatus.Items.Add("Closed")
        End Sub

        ' =========================
        ' GENERATE ACCOUNT NUMBER
        ' =========================
        Private Function GenerateAccountNumber() As String
            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM Accounts", conn)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    Return "ACC" & (100000 + count + 1).ToString()
                End Using

            Catch
                ' fallback
                Dim rand As New Random()
                Return "ACC" & rand.Next(100000, 999999).ToString()
            End Try
        End Function

        ' =========================
        ' VALIDATION FUNCTION
        ' =========================
        Private Function ValidateForm() As Boolean

            If cmbCustomer.SelectedIndex = -1 Then
                MessageBox.Show("Please select a customer.")
                Return False
            End If

            If cmbAccountType.SelectedIndex = -1 Then
                MessageBox.Show("Please select account type.")
                Return False
            End If

            If cmbBranch.SelectedIndex = -1 Then
                MessageBox.Show("Please select branch.")
                Return False
            End If

            If txtBalance.Text = "" Or Not IsNumeric(txtBalance.Text) Then
                MessageBox.Show("Enter a valid balance.")
                Return False
            End If

            If cmbStatus.SelectedIndex = -1 Then
                MessageBox.Show("Select account status.")
                Return False
            End If

            Return True
        End Function

        ' =========================
        ' SAVE BUTTON
        ' =========================
        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

            If Not ValidateForm() Then Exit Sub

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    Dim accountNumber As String = GenerateAccountNumber()

                    Dim query As String = "
                    INSERT INTO Accounts 
                    (CustomerID, AccountNumber, AccountTypeID, BranchID, Balance, DateOpened, AccountStatus)
                    VALUES 
                    (@cust, @accNo, @type, @branch, @bal, @date, @status)
                "

                    Using cmd As New MySqlCommand(query, conn)

                        cmd.Parameters.AddWithValue("@cust", cmbCustomer.SelectedValue)
                        cmd.Parameters.AddWithValue("@accNo", accountNumber)
                        cmd.Parameters.AddWithValue("@type", cmbAccountType.SelectedValue)
                        cmd.Parameters.AddWithValue("@branch", cmbBranch.SelectedValue)
                        cmd.Parameters.AddWithValue("@bal", Convert.ToDecimal(txtBalance.Text))
                        cmd.Parameters.AddWithValue("@date", dtpDateOpened.Value)
                        cmd.Parameters.AddWithValue("@status", cmbStatus.Text)

                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show("Account created successfully!" & vbNewLine & "Account No: " & GenerateAccountNumber())

                ClearForm()

            Catch ex As Exception
                MessageBox.Show("Error saving account: " & ex.Message)
            End Try

        End Sub

        ' =========================
        ' UPDATE BUTTON
        ' =========================
        Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

            If selectedAccountID = 0 Then
                MessageBox.Show("Please select an account to update.")
                Exit Sub
            End If

            If Not ValidateForm() Then Exit Sub

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    Dim query As String = "
                    UPDATE Accounts SET
                        CustomerID = @cust,
                        AccountTypeID = @type,
                        BranchID = @branch,
                        Balance = @bal,
                        DateOpened = @date,
                        AccountStatus = @status
                    WHERE AccountID = @id
                "

                    Using cmd As New MySqlCommand(query, conn)

                        cmd.Parameters.AddWithValue("@cust", cmbCustomer.SelectedValue)
                        cmd.Parameters.AddWithValue("@type", cmbAccountType.SelectedValue)
                        cmd.Parameters.AddWithValue("@branch", cmbBranch.SelectedValue)
                        cmd.Parameters.AddWithValue("@bal", Convert.ToDecimal(txtBalance.Text))
                        cmd.Parameters.AddWithValue("@date", dtpDateOpened.Value)
                        cmd.Parameters.AddWithValue("@status", cmbStatus.Text)
                        cmd.Parameters.AddWithValue("@id", selectedAccountID)

                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show("Account updated successfully!")
                ClearForm()

            Catch ex As Exception
                MessageBox.Show("Error updating account: " & ex.Message)
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
            cmbAccountType.SelectedIndex = -1
            cmbBranch.SelectedIndex = -1
            cmbStatus.SelectedIndex = -1

            txtBalance.Clear()
            dtpDateOpened.Value = Date.Now

            selectedAccountID = 0
        End Sub

    End Class

