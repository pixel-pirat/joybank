Imports MySql.Data.MySqlClient

Public Class loan


    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Controls.Clear()
        Dim uc As New loanRepayment()
        uc.Dock = DockStyle.Fill
        Controls.Add(uc)
    End Sub




    ' =========================
    ' ADD LOAN TYPE BUTTON
    ' =========================
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        ' ========================
        ' VALIDATION
        ' ========================
        If txtLoanType.Text.Trim() = "" Then
            MessageBox.Show("Please enter loan type name.")
            Exit Sub
        End If

        If txtInterestRate.Text.Trim() = "" Or Not IsNumeric(txtInterestRate.Text) Then
            MessageBox.Show("Please enter a valid interest rate.")
            Exit Sub
        End If

        If txtMaxAmount.Text.Trim() = "" Or Not IsNumeric(txtMaxAmount.Text) Then
            MessageBox.Show("Please enter a valid maximum loan amount.")
            Exit Sub
        End If

        Try
            Using conn = DBConnection.GetConnection()
                conn.Open()

                ' =========================
                ' CHECK FOR DUPLICATE
                ' =========================
                Dim checkQuery As String = "SELECT COUNT(*) FROM LoanTypes WHERE LoanTypeName = @name"

                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@name", txtLoanType.Text.Trim())

                    Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                    If exists > 0 Then
                        MessageBox.Show("Loan type already exists.")
                        Exit Sub
                    End If
                End Using

                ' =========================
                ' INSERT LOAN TYPE
                ' =========================
                Dim insertQuery As String = "
                    INSERT INTO LoanTypes
                    (LoanTypeName, InterestRate, MaxLoanAmount)
                    VALUES
                    (@name, @rate, @max)
                "

                Using cmd As New MySqlCommand(insertQuery, conn)

                    cmd.Parameters.AddWithValue("@name", txtLoanType.Text.Trim())
                    cmd.Parameters.AddWithValue("@rate", Convert.ToDecimal(txtInterestRate.Text))
                    cmd.Parameters.AddWithValue("@max", Convert.ToDecimal(txtMaxAmount.Text))

                    cmd.ExecuteNonQuery()

                End Using

            End Using

            MessageBox.Show("Loan type added successfully!")

            ClearForm()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub

    ' =========================
    ' CLEAR FORM FUNCTION
    ' =========================
    Private Sub ClearForm()
        txtLoanType.Clear()
        txtInterestRate.Clear()
        txtMaxAmount.Clear()
    End Sub

    ' =========================
    ' CLEAR BUTTON (OPTIONAL)
    ' =========================


    Private Sub accounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAccounts()
    End Sub

    Private Sub LoadAccounts()
        Try
            Dim query As String = "SELECT * FROM LoanTypes"
            Guna2DataGridView1.DataSource = DBHelper.GetData(query)

            ' Optional formatting
            Guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Guna2DataGridView1.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error loading accounts: " & ex.Message)
        End Try
    End Sub




    Private Sub loan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLoans()
    End Sub

    Private Sub LoadLoans()
        Try
            Dim query As String = "SELECT * FROM Loans"
            Guna2DataGridView2.DataSource = DBHelper.GetData(query)

            ' Optional formatting
            Guna2DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Guna2DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Guna2DataGridView2.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show("Error loading accounts: " & ex.Message)
        End Try
    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Controls.Clear()
        Dim uc As New loanMgt()
        uc.Dock = DockStyle.Fill
        Controls.Add(uc)
    End Sub


End Class