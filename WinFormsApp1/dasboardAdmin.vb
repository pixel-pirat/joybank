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

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Guna2Panel3.Controls.Clear()
        Dim uc As New transactions()
        uc.Dock = DockStyle.Fill
        Guna2Panel3.Controls.Add(uc)
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Guna2Panel3.Controls.Clear()
        Dim uc As New loan()
        uc.Dock = DockStyle.Fill
        Guna2Panel3.Controls.Add(uc)
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        Guna2Panel3.Controls.Clear()
        Dim uc As New branch()
        uc.Dock = DockStyle.Fill
        Guna2Panel3.Controls.Add(uc)
    End Sub

    Private Sub Guna2Button7_Click(sender As Object, e As EventArgs) Handles Guna2Button7.Click
        Guna2Panel3.Controls.Clear()
        Dim uc As New audit()
        uc.Dock = DockStyle.Fill
        Guna2Panel3.Controls.Add(uc)
    End Sub

    Private Sub Guna2Button8_Click(sender As Object, e As EventArgs) Handles Guna2Button8.Click
        Guna2Panel3.Controls.Clear()
        Dim uc As New settings()
        uc.Dock = DockStyle.Fill
        Guna2Panel3.Controls.Add(uc)
    End Sub
End Class