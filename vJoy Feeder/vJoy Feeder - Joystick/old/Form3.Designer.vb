<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.DeviceIDInput = New System.Windows.Forms.TextBox()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.CancelButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'DeviceIDInput
        '
        Me.DeviceIDInput.Location = New System.Drawing.Point(13, 13)
        Me.DeviceIDInput.Name = "DeviceIDInput"
        Me.DeviceIDInput.Size = New System.Drawing.Size(155, 20)
        Me.DeviceIDInput.TabIndex = 0
        '
        'OKButton
        '
        Me.OKButton.Location = New System.Drawing.Point(12, 39)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(75, 23)
        Me.OKButton.TabIndex = 1
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = True
        '
        'CancelButton
        '
        Me.CancelButton.Location = New System.Drawing.Point(93, 39)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(75, 23)
        Me.CancelButton.TabIndex = 2
        Me.CancelButton.Text = "Cancel"
        Me.CancelButton.UseVisualStyleBackColor = True
        '
        'Form3
        '
        Me.AcceptButton = Me.OKButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CancelButton
        Me.ClientSize = New System.Drawing.Size(178, 70)
        Me.Controls.Add(Me.CancelButton)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.DeviceIDInput)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form3"
        Me.Text = "Device ID"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DeviceIDInput As System.Windows.Forms.TextBox
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents CancelButton As System.Windows.Forms.Button
End Class
