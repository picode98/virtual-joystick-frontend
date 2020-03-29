<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutDialog
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ApplicationIconBox = New System.Windows.Forms.PictureBox()
        Me.ApplicationVersionLabel = New System.Windows.Forms.Label()
        Me.ApplicationNameLabel = New System.Windows.Forms.Label()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.BuildDateTimeLabel = New System.Windows.Forms.Label()
        CType(Me.ApplicationIconBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(183, 38)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(457, 34)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Written by Saaman Khalilollahi primarily for FIRST Robotics Competition" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Team 254" &
    "4 (Harbor Creek Robotics Club) and other FRC teams."
        '
        'ApplicationIconBox
        '
        Me.ApplicationIconBox.Location = New System.Drawing.Point(16, 11)
        Me.ApplicationIconBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ApplicationIconBox.Name = "ApplicationIconBox"
        Me.ApplicationIconBox.Size = New System.Drawing.Size(159, 146)
        Me.ApplicationIconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ApplicationIconBox.TabIndex = 1
        Me.ApplicationIconBox.TabStop = False
        '
        'ApplicationVersionLabel
        '
        Me.ApplicationVersionLabel.AutoSize = True
        Me.ApplicationVersionLabel.Location = New System.Drawing.Point(183, 82)
        Me.ApplicationVersionLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ApplicationVersionLabel.Name = "ApplicationVersionLabel"
        Me.ApplicationVersionLabel.Size = New System.Drawing.Size(137, 17)
        Me.ApplicationVersionLabel.TabIndex = 2
        Me.ApplicationVersionLabel.Text = "Application Version: "
        '
        'ApplicationNameLabel
        '
        Me.ApplicationNameLabel.AutoSize = True
        Me.ApplicationNameLabel.Location = New System.Drawing.Point(183, 11)
        Me.ApplicationNameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ApplicationNameLabel.Name = "ApplicationNameLabel"
        Me.ApplicationNameLabel.Size = New System.Drawing.Size(118, 17)
        Me.ApplicationNameLabel.TabIndex = 3
        Me.ApplicationNameLabel.Text = "Application Name"
        '
        'OKButton
        '
        Me.OKButton.Location = New System.Drawing.Point(544, 131)
        Me.OKButton.Margin = New System.Windows.Forms.Padding(4)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(100, 28)
        Me.OKButton.TabIndex = 4
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = True
        '
        'BuildDateTimeLabel
        '
        Me.BuildDateTimeLabel.AutoSize = True
        Me.BuildDateTimeLabel.Location = New System.Drawing.Point(183, 106)
        Me.BuildDateTimeLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.BuildDateTimeLabel.Name = "BuildDateTimeLabel"
        Me.BuildDateTimeLabel.Size = New System.Drawing.Size(116, 17)
        Me.BuildDateTimeLabel.TabIndex = 5
        Me.BuildDateTimeLabel.Text = "Build Date/Time: "
        '
        'AboutDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(657, 172)
        Me.Controls.Add(Me.BuildDateTimeLabel)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.ApplicationNameLabel)
        Me.Controls.Add(Me.ApplicationVersionLabel)
        Me.Controls.Add(Me.ApplicationIconBox)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutDialog"
        Me.Text = "About This Application"
        CType(Me.ApplicationIconBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ApplicationIconBox As System.Windows.Forms.PictureBox
    Friend WithEvents ApplicationVersionLabel As System.Windows.Forms.Label
    Friend WithEvents ApplicationNameLabel As System.Windows.Forms.Label
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents BuildDateTimeLabel As System.Windows.Forms.Label
End Class
