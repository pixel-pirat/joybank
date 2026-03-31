Public Class accounts
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Controls.Clear()
        Dim uc As New accountForm()
        uc.Dock = DockStyle.Fill
        Controls.Add(uc)
    End Sub
End Class
