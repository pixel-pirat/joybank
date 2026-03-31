<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class customer
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Guna2HtmlLabel18 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Guna2HtmlLabel17 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Guna2Panel3 = New Guna.UI2.WinForms.Guna2Panel()
        dgvCustomers = New Guna.UI2.WinForms.Guna2DataGridView()
        Guna2Button1 = New Guna.UI2.WinForms.Guna2Button()
        Guna2Panel3.SuspendLayout()
        CType(dgvCustomers, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Guna2HtmlLabel18
        ' 
        Guna2HtmlLabel18.BackColor = Color.Transparent
        Guna2HtmlLabel18.Font = New Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Guna2HtmlLabel18.ForeColor = Color.FromArgb(CByte(115), CByte(130), CByte(146))
        Guna2HtmlLabel18.Location = New Point(32, 93)
        Guna2HtmlLabel18.Name = "Guna2HtmlLabel18"
        Guna2HtmlLabel18.Size = New Size(341, 24)
        Guna2HtmlLabel18.TabIndex = 14
        Guna2HtmlLabel18.Text = "View and manage all registered customers"
        ' 
        ' Guna2HtmlLabel17
        ' 
        Guna2HtmlLabel17.BackColor = Color.Transparent
        Guna2HtmlLabel17.Font = New Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Guna2HtmlLabel17.ForeColor = Color.Black
        Guna2HtmlLabel17.Location = New Point(32, 49)
        Guna2HtmlLabel17.Name = "Guna2HtmlLabel17"
        Guna2HtmlLabel17.Size = New Size(328, 38)
        Guna2HtmlLabel17.TabIndex = 13
        Guna2HtmlLabel17.Text = "Customer Management"
        ' 
        ' Guna2Panel3
        ' 
        Guna2Panel3.BackColor = Color.Transparent
        Guna2Panel3.BorderRadius = 14
        Guna2Panel3.Controls.Add(dgvCustomers)
        Guna2Panel3.CustomizableEdges = CustomizableEdges1
        Guna2Panel3.FillColor = Color.White
        Guna2Panel3.Location = New Point(32, 172)
        Guna2Panel3.Name = "Guna2Panel3"
        Guna2Panel3.ShadowDecoration.BorderRadius = 14
        Guna2Panel3.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        Guna2Panel3.ShadowDecoration.Depth = 3
        Guna2Panel3.ShadowDecoration.Enabled = True
        Guna2Panel3.Size = New Size(1520, 619)
        Guna2Panel3.TabIndex = 15
        ' 
        ' dgvCustomers
        ' 
        DataGridViewCellStyle1.BackColor = Color.White
        dgvCustomers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvCustomers.BackgroundColor = Color.Gainsboro
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(100), CByte(88), CByte(255))
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = Color.White
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        dgvCustomers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvCustomers.ColumnHeadersHeight = 4
        dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = Color.White
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle3.ForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        DataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        dgvCustomers.DefaultCellStyle = DataGridViewCellStyle3
        dgvCustomers.GridColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvCustomers.Location = New Point(15, 21)
        dgvCustomers.Name = "dgvCustomers"
        dgvCustomers.RowHeadersVisible = False
        dgvCustomers.RowHeadersWidth = 51
        dgvCustomers.Size = New Size(1488, 580)
        dgvCustomers.TabIndex = 0
        dgvCustomers.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White
        dgvCustomers.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        dgvCustomers.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty
        dgvCustomers.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty
        dgvCustomers.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty
        dgvCustomers.ThemeStyle.BackColor = Color.Gainsboro
        dgvCustomers.ThemeStyle.GridColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvCustomers.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(CByte(100), CByte(88), CByte(255))
        dgvCustomers.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None
        dgvCustomers.ThemeStyle.HeaderStyle.Font = New Font("Segoe UI", 9F)
        dgvCustomers.ThemeStyle.HeaderStyle.ForeColor = Color.White
        dgvCustomers.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        dgvCustomers.ThemeStyle.HeaderStyle.Height = 4
        dgvCustomers.ThemeStyle.ReadOnly = False
        dgvCustomers.ThemeStyle.RowsStyle.BackColor = Color.White
        dgvCustomers.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvCustomers.ThemeStyle.RowsStyle.Font = New Font("Segoe UI", 9F)
        dgvCustomers.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        dgvCustomers.ThemeStyle.RowsStyle.Height = 29
        dgvCustomers.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvCustomers.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        ' 
        ' Guna2Button1
        ' 
        Guna2Button1.CustomizableEdges = CustomizableEdges3
        Guna2Button1.DisabledState.BorderColor = Color.DarkGray
        Guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray
        Guna2Button1.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        Guna2Button1.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        Guna2Button1.Font = New Font("Segoe UI", 9F)
        Guna2Button1.ForeColor = Color.White
        Guna2Button1.Location = New Point(1327, 93)
        Guna2Button1.Name = "Guna2Button1"
        Guna2Button1.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        Guna2Button1.Size = New Size(225, 56)
        Guna2Button1.TabIndex = 20
        Guna2Button1.Text = "Create New User"
        ' 
        ' customer
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(Guna2Button1)
        Controls.Add(Guna2Panel3)
        Controls.Add(Guna2HtmlLabel18)
        Controls.Add(Guna2HtmlLabel17)
        Name = "customer"
        Size = New Size(1584, 954)
        Guna2Panel3.ResumeLayout(False)
        CType(dgvCustomers, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Guna2HtmlLabel18 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2HtmlLabel17 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2Panel3 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents dgvCustomers As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents Guna2Button1 As Guna.UI2.WinForms.Guna2Button

End Class
