Public Class dasboardAdmin
    Private Sub Guna2Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel3.Paint

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Guna2Panel3.Controls.Clear()
        Dim uc As New overview()
        uc.Dock = DockStyle.Fill
        Guna2Panel3.Controls.Add(uc)
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Guna2Panel3.Controls.Clear()
        Dim uc As New customer()
        uc.Dock = DockStyle.Fill
        Guna2Panel3.Controls.Add(uc)
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Guna2Panel3.Controls.Clear()
        Dim uc As New accounts()
        uc.Dock = DockStyle.Fill
        Guna2Panel3.Controls.Add(uc)

    End Sub
End Class