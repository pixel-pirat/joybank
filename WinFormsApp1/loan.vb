Public Class loan
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Controls.Clear()
        Dim uc As New loanMgt()
        uc.Dock = DockStyle.Fill
        Controls.Add(uc)
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Controls.Clear()
        Dim uc As New loanRepayment()
        uc.Dock = DockStyle.Fill
        Controls.Add(uc)
    End Sub
End Class
