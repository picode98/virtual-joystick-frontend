<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Joystick
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
        Me.JoystickOutline = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.JoystickIndicator = New Microsoft.VisualBasic.PowerPacks.OvalShape()
        Me.SuspendLayout()
        '
        'JoystickOutline
        '
        Me.JoystickOutline.Location = New System.Drawing.Point(0, 0)
        Me.JoystickOutline.Name = "JoystickOutline"
        Me.JoystickOutline.SelectionColor = System.Drawing.Color.Transparent
        Me.JoystickOutline.Size = New System.Drawing.Size(100, 100)
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.JoystickIndicator, Me.JoystickOutline})
        Me.ShapeContainer1.Size = New System.Drawing.Size(101, 101)
        Me.ShapeContainer1.TabIndex = 0
        Me.ShapeContainer1.TabStop = False
        '
        'JoystickIndicator
        '
        Me.JoystickIndicator.BorderColor = System.Drawing.Color.Red
        Me.JoystickIndicator.FillColor = System.Drawing.Color.Red
        Me.JoystickIndicator.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.JoystickIndicator.Location = New System.Drawing.Point(46, 46)
        Me.JoystickIndicator.Name = "JoystickIndicator"
        Me.JoystickIndicator.Size = New System.Drawing.Size(8, 8)
        '
        'Joystick
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Name = "Joystick"
        Me.Size = New System.Drawing.Size(101, 101)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents JoystickOutline As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents JoystickIndicator As Microsoft.VisualBasic.PowerPacks.OvalShape

End Class
