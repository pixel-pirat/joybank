Public Class customer
    Private Sub Guna2Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel3.Paint

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Controls.Clear()
        Dim uc As New customerForm()
        uc.Dock = DockStyle.Fill
        Controls.Add(uc)
    End Sub
End Class
