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
        Me.FormOKButton = New System.Windows.Forms.Button()
        Me.FormCancelButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'DeviceIDInput
        '
        Me.DeviceIDInput.Location = New System.Drawing.Point(12, 38)
        Me.DeviceIDInput.Name = "DeviceIDInput"
        Me.DeviceIDInput.Size = New System.Drawing.Size(155, 20)
        Me.DeviceIDInput.TabIndex = 0
        '
        'FormOKButton
        '
        Me.FormOKButton.Location = New System.Drawing.Point(12, 64)
        Me.FormOKButton.Name = "FormOKButton"
        Me.FormOKButton.Size = New System.Drawing.Size(75, 23)
        Me.FormOKButton.TabIndex = 1
        Me.FormOKButton.Text = "OK"
        Me.FormOKButton.UseVisualStyleBackColor = True
        '
        'FormCancelButton
        '
        Me.FormCancelButton.Location = New System.Drawing.Point(93, 64)
        Me.FormCancelButton.Name = "FormCancelButton"
        Me.FormCancelButton.Size = New System.Drawing.Size(75, 23)
        Me.FormCancelButton.TabIndex = 2
        Me.FormCancelButton.Text = "Cancel"
        Me.FormCancelButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 26)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Enter the vJoy device ID to" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "connect to:"
        '
        'Form3
        '
        Me.AcceptButton = Me.FormOKButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.FormCancelButton
        Me.ClientSize = New System.Drawing.Size(180, 98)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FormCancelButton)
        Me.Controls.Add(Me.FormOKButton)
        Me.Controls.Add(Me.DeviceIDInput)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form3"
        Me.Text = "Connect Device"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DeviceIDInput As System.Windows.Forms.TextBox
    Friend WithEvents FormOKButton As System.Windows.Forms.Button
    Friend WithEvents FormCancelButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
