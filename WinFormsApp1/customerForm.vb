Imports MySql.Data.MySqlClient



Public Class customerForm

        ' Store selected Customer ID for update
        Public selectedCustomerID As Integer = 0

        ' =========================
        ' FORM LOAD
        ' =========================
        Private Sub customerForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Populate Gender ComboBox
        Guna2ComboBox1.Items.Clear()
        Guna2ComboBox1.Items.Add("Male")
        Guna2ComboBox1.Items.Add("Female")
    End Sub

        ' =========================
        ' SAVE BUTTON
        ' =========================
        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

            ' Validation
            If txtFirstName.Text = "" Or txtLastName.Text = "" Then
                MessageBox.Show("Please fill all required fields.")
                Exit Sub
            End If

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    Dim query As String = "
                    INSERT INTO Customers 
                    (FirstName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address, NationalID)
                    VALUES 
                    (@fname, @lname, @dob, @gender, @phone, @email, @address, @nid)
                "

                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@fname", txtFirstName.Text)
                        cmd.Parameters.AddWithValue("@lname", txtLastName.Text)
                        cmd.Parameters.AddWithValue("@dob", dtpDOB.Value)
                    cmd.Parameters.AddWithValue("@gender", Guna2ComboBox1.Text)
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text)
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text)
                        cmd.Parameters.AddWithValue("@nid", txtNationalID.Text)

                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show("Customer saved successfully!")
                ClearForm()

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try

        End Sub

        ' =========================
        ' UPDATE BUTTON
        ' =========================
        Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

            If selectedCustomerID = 0 Then
                MessageBox.Show("Please select a customer to update.")
                Exit Sub
            End If

            Try
                Using conn = DBConnection.GetConnection()
                    conn.Open()

                    Dim query As String = "
                    UPDATE Customers SET
                        FirstName = @fname,
                        LastName = @lname,
                        DateOfBirth = @dob,
                        Gender = @gender,
                        PhoneNumber = @phone,
                        Email = @email,
                        Address = @address,
                        NationalID = @nid
                    WHERE CustomerID = @id
                "

                    Using cmd As New MySqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@fname", txtFirstName.Text)
                        cmd.Parameters.AddWithValue("@lname", txtLastName.Text)
                        cmd.Parameters.AddWithValue("@dob", dtpDOB.Value)
                    cmd.Parameters.AddWithValue("@gender", Guna2ComboBox1.Text)
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text)
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text)
                        cmd.Parameters.AddWithValue("@nid", txtNationalID.Text)
                        cmd.Parameters.AddWithValue("@id", selectedCustomerID)

                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show("Customer updated successfully!")
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
            txtFirstName.Clear()
            txtLastName.Clear()
        Guna2ComboBox1.SelectedIndex = -1
        txtPhone.Clear()
            txtEmail.Clear()
            txtAddress.Clear()
            txtNationalID.Clear()

            dtpDOB.Value = Date.Now

            selectedCustomerID = 0
        End Sub

End Class
