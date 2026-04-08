Imports MySql.Data.MySqlClient
Imports System.Data

Public Class cardAdministration


    Private selectedCustomerID As Integer = 0
    Private selectedAccountID As Integer = 0
    Private Sub cardAdministration_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        ' =========================
        ' FORM LOAD
        ' =========================

        LoadCustomers()
        LoadCardTypes()
        LoadCardStatus()
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
    ' LOAD CARD TYPES
    ' =========================
    Private Sub LoadCardTypes()
        cmbCardType.Items.Clear()
        cmbCardType.Items.Add("Debit")
        cmbCardType.Items.Add("Credit")
        cmbCardType.Items.Add("ATM")
    End Sub

    ' =========================
    ' LOAD STATUS
    ' =========================
    Private Sub LoadCardStatus()
        cmbCardStatus.Items.Clear()
        cmbCardStatus.Items.Add("Active")
        cmbCardStatus.Items.Add("Blocked")
        cmbCardStatus.Items.Add("Expired")
    End Sub

    ' =========================
    ' WHEN CUSTOMER SELECTED
    ' =========================
    Private Sub cmbCustomer_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbCustomer.SelectionChangeCommitted

        If cmbCustomer.SelectedValue Is Nothing Then Exit Sub

        selectedCustomerID = Convert.ToInt32(cmbCustomer.SelectedValue)

        Try
            Using conn = DBConnection.GetConnection()
                conn.Open()

                ' GET ACCOUNT
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
                        lblAccountNumber.Text = reader("AccountNumber").ToString()
                    Else
                        lblAccountNumber.Text = "No Account"
                        selectedAccountID = 0
                    End If
                End Using

            End Using

            ' GENERATE CARD NUMBER
            GenerateCardNumber()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    ' =========================
    ' GENERATE CARD NUMBER
    ' =========================
    Private Sub GenerateCardNumber()

        Dim rnd As New Random()

        Dim cardNumber As String =
        rnd.Next(4000, 4999).ToString() & " " &
        rnd.Next(1000, 9999).ToString() & " " &
        rnd.Next(1000, 9999).ToString() & " " &
        rnd.Next(1000, 9999).ToString()

        lblCardNumber.Text = cardNumber

    End Sub

    ' =========================
    ' CREATE CARD
    ' =========================
    Private Sub btnCreateCard_Click(sender As Object, e As EventArgs) Handles btnCreateCard.Click

        If selectedAccountID = 0 Then
            MessageBox.Show("Customer has no account.")
            Exit Sub
        End If

        If cmbCardType.SelectedIndex = -1 Then
            MessageBox.Show("Select card type.")
            Exit Sub
        End If

        If cmbCardStatus.SelectedIndex = -1 Then
            MessageBox.Show("Select card status.")
            Exit Sub
        End If

        Try
            Using conn = DBConnection.GetConnection()
                conn.Open()

                Dim expiryDate As Date = Date.Now.AddYears(3)

                Dim query As String = "
                    INSERT INTO Cards
                    (AccountID, CardNumber, CardType, ExpiryDate, CardStatus)
                    VALUES
                    (@acc, @card, @type, @exp, @status)
                "

                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@acc", selectedAccountID)
                    cmd.Parameters.AddWithValue("@card", lblCardNumber.Text.Replace(" ", ""))
                    cmd.Parameters.AddWithValue("@type", cmbCardType.Text)
                    cmd.Parameters.AddWithValue("@exp", expiryDate)
                    cmd.Parameters.AddWithValue("@status", cmbCardStatus.Text)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

            MessageBox.Show("Card created successfully!")

            ' UPDATE PREVIEW PANEL
            lblPreviewCardNumber.Text = lblCardNumber.Text
            lblPreviewCustomer.Text = cmbCustomer.Text
            lblPreviewCardType.Text = cmbCardType.Text
            lblPreviewExpiry.Text = Date.Now.AddYears(3).ToString("MM/yyyy")



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    ' =========================
    ' CLEAR FORM
    ' =========================
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        cmbCustomer.SelectedIndex = -1
        cmbCardType.SelectedIndex = -1
        cmbCardStatus.SelectedIndex = -1

        lblAccountNumber.Text = ""
        lblCardNumber.Text = ""

        selectedAccountID = 0
        selectedCustomerID = 0

    End Sub

End Class

