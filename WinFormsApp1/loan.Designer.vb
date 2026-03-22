<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class loan
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
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Guna2HtmlLabel17 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Guna2HtmlLabel18 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        SuspendLayout()
        ' 
        ' Guna2HtmlLabel17
        ' 
        Guna2HtmlLabel17.BackColor = Color.Transparent
        Guna2HtmlLabel17.Font = New Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Guna2HtmlLabel17.ForeColor = Color.Black
        Guna2HtmlLabel17.Location = New Point(29, 38)
        Guna2HtmlLabel17.Name = "Guna2HtmlLabel17"
        Guna2HtmlLabel17.Size = New Size(264, 38)
        Guna2HtmlLabel17.TabIndex = 22
        Guna2HtmlLabel17.Text = "Loan Management"
        ' 
        ' Guna2HtmlLabel18
        ' 
        Guna2HtmlLabel18.BackColor = Color.Transparent
        Guna2HtmlLabel18.Font = New Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Guna2HtmlLabel18.ForeColor = Color.FromArgb(CByte(115), CByte(130), CByte(146))
        Guna2HtmlLabel18.Location = New Point(35, 82)
        Guna2HtmlLabel18.Name = "Guna2HtmlLabel18"
        Guna2HtmlLabel18.Size = New Size(276, 24)
        Guna2HtmlLabel18.TabIndex = 23
        Guna2HtmlLabel18.Text = "Approve, reject and manage loans"
        ' 
        ' Guna2Panel1
        ' 
        Guna2Panel1.CustomizableEdges = CustomizableEdges1
        Guna2Panel1.Location = New Point(32, 142)
        Guna2Panel1.Name = "Guna2Panel1"
        Guna2Panel1.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        Guna2Panel1.Size = New Size(1528, 786)
        Guna2Panel1.TabIndex = 24
        ' 
        ' loan
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Guna2Panel1)
        Controls.Add(Guna2HtmlLabel17)
        Controls.Add(Guna2HtmlLabel18)
        Name = "loan"
        Size = New Size(1584, 954)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Guna2HtmlLabel17 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel18 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel

End Class
