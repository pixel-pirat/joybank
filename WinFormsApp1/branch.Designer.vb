<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class branch
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Guna2HtmlLabel18 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Guna2HtmlLabel17 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        SuspendLayout()
        ' 
        ' Guna2HtmlLabel18
        ' 
        Guna2HtmlLabel18.BackColor = Color.Transparent
        Guna2HtmlLabel18.Font = New Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Guna2HtmlLabel18.ForeColor = Color.FromArgb(CByte(115), CByte(130), CByte(146))
        Guna2HtmlLabel18.Location = New Point(23, 87)
        Guna2HtmlLabel18.Name = "Guna2HtmlLabel18"
        Guna2HtmlLabel18.Size = New Size(306, 24)
        Guna2HtmlLabel18.TabIndex = 22
        Guna2HtmlLabel18.Text = "Manage branch and staff assignments"
        ' 
        ' Guna2HtmlLabel17
        ' 
        Guna2HtmlLabel17.BackColor = Color.Transparent
        Guna2HtmlLabel17.Font = New Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Guna2HtmlLabel17.ForeColor = Color.Black
        Guna2HtmlLabel17.Location = New Point(23, 43)
        Guna2HtmlLabel17.Name = "Guna2HtmlLabel17"
        Guna2HtmlLabel17.Size = New Size(472, 38)
        Guna2HtmlLabel17.TabIndex = 21
        Guna2HtmlLabel17.Text = "Branch & Employee Management"
        ' 
        ' branch
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Guna2HtmlLabel18)
        Controls.Add(Guna2HtmlLabel17)
        Name = "branch"
        Size = New Size(1584, 954)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Guna2HtmlLabel18 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel17 As Guna.UI2.WinForms.Guna2HtmlLabel

End Class
