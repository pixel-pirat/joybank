Imports MySql.Data.MySqlClient

Public Class branch
    Private selectedBranchID As Integer = 0
    Private Sub branch_Load(sender As Object, e As EventArgs) Handles MyBase.Load




        ' =========================
        ' FORM LOAD
        ' =========================

        LoadManagers()
        LoadBranches()
        LoadStatus()
    End Sub

    ' =========================
    ' LOAD MANAGERS
    ' =========================
    Private Sub LoadManagers()

        Dim query As String = "
            SELECT EmployeeID, CONCAT(FirstName, ' ', LastName) AS FullName 
            FROM Employees
        "

        Dim table = DBHelper.GetData(query)

        cmbManager.DataSource = table
        cmbManager.DisplayMember = "FullName"
        cmbManager.ValueMember = "EmployeeID"

    End Sub

    ' =========================
    ' LOAD STATUS
    ' =========================
    Private Sub LoadStatus()
        cmbStatus.Items.Clear()
        cmbStatus.Items.Add("Active")
        cmbStatus.Items.Add("Inactive")
    End Sub

    ' =========================
    ' LOAD BRANCHES INTO GRID
    ' =========================
    Private Sub LoadBranches()

        Dim query As String = "
            SELECT 
                b.BranchID,
                b.BranchName,
                b.BranchPhone,
                b.BranchAddress,
                CONCAT(e.FirstName, ' ', e.LastName) AS ManagerName
            FROM Branches b
            LEFT JOIN Employees e ON b.ManagerID = e.EmployeeID
        "

        dgvBranches.DataSource = DBHelper.GetData(query)

    End Sub

    ' =========================
    ' REGISTER BRANCH
    ' =========================
    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click

        If txtBranchName.Text = "" Then
            MessageBox.Show("Enter branch name")
            Exit Sub
        End If

        If cmbManager.SelectedIndex = -1 Then
            MessageBox.Show("Select manager")
            Exit Sub
        End If

        Try
            Using conn = DBConnection.GetConnection()
                conn.Open()

                Dim query As String = "
                    INSERT INTO Branches 
                    (BranchName, BranchAddress, BranchPhone, ManagerID)
                    VALUES 
                    (@name, @address, @phone, @manager)
                "

                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@name", txtBranchName.Text)
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text)
                    cmd.Parameters.AddWithValue("@phone", txtBranchPhone.Text)
                    cmd.Parameters.AddWithValue("@manager", cmbManager.SelectedValue)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

            MessageBox.Show("Branch Registered Successfully")

            LoadBranches()
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    ' =========================
    ' GRID ROW CLICK → LOAD FORM
    ' =========================
    Private Sub dgvBranches_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBranches.CellClick

        If e.RowIndex < 0 Then Exit Sub

        Dim row = dgvBranches.Rows(e.RowIndex)

        selectedBranchID = Convert.ToInt32(row.Cells("BranchID").Value)

        txtBranchName.Text = row.Cells("BranchName").Value.ToString()
        txtBranchPhone.Text = row.Cells("BranchPhone").Value.ToString()
        txtAddress.Text = row.Cells("BranchAddress").Value.ToString()

        cmbManager.Text = row.Cells("ManagerName").Value.ToString()

        btnUpdate.Enabled = True

    End Sub

    ' =========================
    ' UPDATE BRANCH
    ' =========================
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        If selectedBranchID = 0 Then
            MessageBox.Show("Select a branch first")
            Exit Sub
        End If

        Try
            Using conn = DBConnection.GetConnection()
                conn.Open()

                Dim query As String = "
                    UPDATE Branches
                    SET 
                        BranchName = @name,
                        BranchAddress = @address,
                        BranchPhone = @phone,
                        ManagerID = @manager
                    WHERE BranchID = @id
                "

                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@name", txtBranchName.Text)
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text)
                    cmd.Parameters.AddWithValue("@phone", txtBranchPhone.Text)
                    cmd.Parameters.AddWithValue("@manager", cmbManager.SelectedValue)
                    cmd.Parameters.AddWithValue("@id", selectedBranchID)

                    cmd.ExecuteNonQuery()

                End Using

            End Using

            MessageBox.Show("Branch Updated Successfully")

            LoadBranches()
            ClearForm()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    ' =========================
    ' CLEAR FORM
    ' =========================
    Private Sub ClearForm()

        txtBranchName.Clear()
        txtBranchPhone.Clear()
        txtAddress.Clear()

        cmbManager.SelectedIndex = -1
        cmbStatus.SelectedIndex = -1

        selectedBranchID = 0
        btnUpdate.Enabled = False

    End Sub

End Class
