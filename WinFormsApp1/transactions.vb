Imports MySql.Data.MySqlClient

Public Class transactions

    Private Sub transactions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTransactions()
    End Sub

    Private Sub LoadTransactions()
        Try
            Dim query As String = "SELECT * FROM Transactions"
            Guna2DataGridView1.DataSource = DBHelper.GetData(query)

            ' Optional formatting
            Guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Guna2DataGridView1.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error loading accounts: " & ex.Message)
        End Try
    End Sub




    Private selectedTransactionType As Integer = 0 '1=Deposit,2=Withdraw,3=Transfer

        ' =========================
        ' FORM LOAD
        ' =========================
        Private Sub transactionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadAccounts()
            LoadUsers()

            cmbReceiverAccount.Enabled = False
        End Sub

        ' =========================
        ' LOAD ACCOUNTS
        ' =========================
        Private Sub LoadAccounts()
            Dim query As String = "SELECT AccountID, AccountNumber FROM Accounts"
            Dim table = DBHelper.GetData(query)

            cmbSenderAccount.DataSource = table.Copy()
            cmbSenderAccount.DisplayMember = "AccountNumber"
            cmbSenderAccount.ValueMember = "AccountID"

            cmbReceiverAccount.DataSource = table
            cmbReceiverAccount.DisplayMember = "AccountNumber"
            cmbReceiverAccount.ValueMember = "AccountID"
        End Sub

        ' =========================
        ' LOAD USERS
        ' =========================
        Private Sub LoadUsers()
            Dim query As String = "SELECT UserID, Username FROM Users"
            Dim table = DBHelper.GetData(query)

            cmbUser.DataSource = table
            cmbUser.DisplayMember = "Username"
            cmbUser.ValueMember = "UserID"
        End Sub

        ' =========================
        ' TRANSACTION TYPE BUTTONS
        ' =========================
        Private Sub btnDeposit_Click(sender As Object, e As EventArgs) Handles btnDeposit.Click
            selectedTransactionType = 1
            cmbReceiverAccount.Enabled = False
        End Sub

        Private Sub btnWithdraw_Click(sender As Object, e As EventArgs) Handles btnWithdraw.Click
            selectedTransactionType = 2
            cmbReceiverAccount.Enabled = False
        End Sub

        Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
            selectedTransactionType = 3
            cmbReceiverAccount.Enabled = True
        End Sub

        ' =========================
        ' CONFIRM TRANSACTION
        ' =========================
        Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click

            If cmbSenderAccount.SelectedIndex = -1 Then
                MessageBox.Show("Select sender account.")
                Exit Sub
            End If

            If txtAmount.Text = "" Or Not IsNumeric(txtAmount.Text) Then
                MessageBox.Show("Enter valid amount.")
                Exit Sub
            End If

            If selectedTransactionType = 0 Then
                MessageBox.Show("Select transaction type.")
                Exit Sub
            End If

            Dim senderID As Integer = cmbSenderAccount.SelectedValue
            Dim amount As Decimal = Convert.ToDecimal(txtAmount.Text)

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    Dim dbTransaction = conn.BeginTransaction()

                    Try
                        Select Case selectedTransactionType

                        ' =========================
                        ' DEPOSIT
                        ' =========================
                            Case 1
                                Dim cmd As New MySqlCommand("
                                UPDATE Accounts 
                                SET Balance = Balance + @amt 
                                WHERE AccountID = @id
                            ", conn, dbTransaction)

                                cmd.Parameters.AddWithValue("@amt", amount)
                                cmd.Parameters.AddWithValue("@id", senderID)
                                cmd.ExecuteNonQuery()

                                InsertTransaction(conn, dbTransaction, senderID, 1, amount, "Deposit")

                        ' =========================
                        ' WITHDRAW
                        ' =========================
                            Case 2
                                Dim checkCmd As New MySqlCommand("
                                SELECT Balance FROM Accounts WHERE AccountID = @id
                            ", conn, dbTransaction)

                                checkCmd.Parameters.AddWithValue("@id", senderID)
                                Dim balance As Decimal = Convert.ToDecimal(checkCmd.ExecuteScalar())

                                If balance < amount Then
                                    MessageBox.Show("Insufficient balance.")
                                    dbTransaction.Rollback()
                                    Exit Sub
                                End If

                                Dim cmd As New MySqlCommand("
                                UPDATE Accounts 
                                SET Balance = Balance - @amt 
                                WHERE AccountID = @id
                            ", conn, dbTransaction)

                                cmd.Parameters.AddWithValue("@amt", amount)
                                cmd.Parameters.AddWithValue("@id", senderID)
                                cmd.ExecuteNonQuery()

                                InsertTransaction(conn, dbTransaction, senderID, 2, amount, "Withdrawal")

                        ' =========================
                        ' TRANSFER
                        ' =========================
                            Case 3

                                If cmbReceiverAccount.SelectedIndex = -1 Then
                                    MessageBox.Show("Select receiver account.")
                                    dbTransaction.Rollback()
                                    Exit Sub
                                End If

                                Dim receiverID As Integer = cmbReceiverAccount.SelectedValue

                                If receiverID = senderID Then
                                    MessageBox.Show("Cannot transfer to same account.")
                                    dbTransaction.Rollback()
                                    Exit Sub
                                End If

                                ' Check sender balance
                                Dim checkCmd As New MySqlCommand("
                                SELECT Balance FROM Accounts WHERE AccountID = @id
                            ", conn, dbTransaction)

                                checkCmd.Parameters.AddWithValue("@id", senderID)
                                Dim balance As Decimal = Convert.ToDecimal(checkCmd.ExecuteScalar())

                                If balance < amount Then
                                    MessageBox.Show("Insufficient balance.")
                                    dbTransaction.Rollback()
                                    Exit Sub
                                End If

                                ' Deduct sender
                                Dim deductCmd As New MySqlCommand("
                                UPDATE Accounts 
                                SET Balance = Balance - @amt 
                                WHERE AccountID = @id
                            ", conn, dbTransaction)

                                deductCmd.Parameters.AddWithValue("@amt", amount)
                                deductCmd.Parameters.AddWithValue("@id", senderID)
                                deductCmd.ExecuteNonQuery()

                                ' Add receiver
                                Dim addCmd As New MySqlCommand("
                                UPDATE Accounts 
                                SET Balance = Balance + @amt 
                                WHERE AccountID = @id
                            ", conn, dbTransaction)

                                addCmd.Parameters.AddWithValue("@amt", amount)
                                addCmd.Parameters.AddWithValue("@id", receiverID)
                                addCmd.ExecuteNonQuery()

                                ' Log sender
                                InsertTransaction(conn, dbTransaction, senderID, 3, amount, "Transfer Sent")

                                ' Log receiver
                                InsertTransaction(conn, dbTransaction, receiverID, 3, amount, "Transfer Received")

                        End Select

                        dbTransaction.Commit()
                        MessageBox.Show("Transaction successful!")

                        ClearForm()

                    Catch ex As Exception
                        dbTransaction.Rollback()
                        MessageBox.Show("Transaction failed: " & ex.Message)
                    End Try

                End Using

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try

        End Sub

        ' =========================
        ' INSERT TRANSACTION
        ' =========================
        Private Sub InsertTransaction(conn As MySqlConnection, trans As MySqlTransaction,
                                 accID As Integer, typeID As Integer,
                                 amount As Decimal, desc As String)

            Dim cmd As New MySqlCommand("
            INSERT INTO Transactions 
            (AccountID, TransactionTypeID, Amount, Description, PerformedBy)
            VALUES 
            (@acc, @type, @amt, @desc, @user)
        ", conn, trans)

            cmd.Parameters.AddWithValue("@acc", accID)
            cmd.Parameters.AddWithValue("@type", typeID)
            cmd.Parameters.AddWithValue("@amt", amount)
            cmd.Parameters.AddWithValue("@desc", desc)
            cmd.Parameters.AddWithValue("@user", cmbUser.SelectedValue)

            cmd.ExecuteNonQuery()

        End Sub

        ' =========================
        ' CANCEL / CLEAR
        ' =========================
        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            ClearForm()
        End Sub

        Private Sub ClearForm()
            cmbSenderAccount.SelectedIndex = -1
            cmbReceiverAccount.SelectedIndex = -1
            cmbUser.SelectedIndex = -1

            txtAmount.Clear()

            selectedTransactionType = 0
            cmbReceiverAccount.Enabled = False
        End Sub

End Class