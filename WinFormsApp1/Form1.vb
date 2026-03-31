Public Class Form1


    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

            Dim username As String = txtUsername.Text.Trim()
            Dim password As String = txtPassword.Text.Trim()

            ' 1. Check if fields are empty
            If username = "" Or password = "" Then
                MessageBox.Show("Please enter both username and password.", "Error")
                Exit Sub
            End If

            ' 2. Hardcoded credentials (for demo)
            Dim validUsername As String = "admin"
            Dim validPassword As String = "password123"

            ' 3. Authentication check
            If username = validUsername AndAlso password = validPassword Then
                MessageBox.Show("Login successful!", "Success")

                Me.Hide()
            dasboardAdmin.Show()

        Else
                MessageBox.Show("Access Denied! Invalid login details.", "Authentication Failed")

                txtUsername.Clear()
                txtPassword.Clear()
                txtUsername.Focus()
            End If

        End Sub

    End Class

