<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.ThrottleLevel = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.ThrottleOutline = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.B3 = New System.Windows.Forms.Button()
        Me.B4 = New System.Windows.Forms.Button()
        Me.B1 = New System.Windows.Forms.Button()
        Me.B0 = New System.Windows.Forms.Button()
        Me.B5 = New System.Windows.Forms.Button()
        Me.B2 = New System.Windows.Forms.Button()
        Me.B6 = New System.Windows.Forms.Button()
        Me.B9 = New System.Windows.Forms.Button()
        Me.B10 = New System.Windows.Forms.Button()
        Me.B7 = New System.Windows.Forms.Button()
        Me.B8 = New System.Windows.Forms.Button()
        Me.ConnectLabel = New System.Windows.Forms.LinkLabel()
        Me.DisconnectedLabel = New System.Windows.Forms.Label()
        Me.ApplicationToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ApplicationDropDown = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ConnectControllerItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DisconnectControllerItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewDropDown = New System.Windows.Forms.ToolStripDropDownButton()
        Me.AlwaysOnTopMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Joystick1 = New vJoy_Feeder.Joystick()
        Me.ApplicationToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.ThrottleLevel, Me.ThrottleOutline})
        Me.ShapeContainer1.Size = New System.Drawing.Size(375, 509)
        Me.ShapeContainer1.TabIndex = 0
        Me.ShapeContainer1.TabStop = False
        '
        'ThrottleLevel
        '
        Me.ThrottleLevel.FillColor = System.Drawing.Color.Blue
        Me.ThrottleLevel.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.ThrottleLevel.Location = New System.Drawing.Point(174, 476)
        Me.ThrottleLevel.Name = "ThrottleLevel"
        Me.ThrottleLevel.Size = New System.Drawing.Size(25, 25)
        '
        'ThrottleOutline
        '
        Me.ThrottleOutline.Location = New System.Drawing.Point(174, 401)
        Me.ThrottleOutline.Name = "ThrottleOutline"
        Me.ThrottleOutline.SelectionColor = System.Drawing.Color.Transparent
        Me.ThrottleOutline.Size = New System.Drawing.Size(25, 100)
        '
        'B3
        '
        Me.B3.Location = New System.Drawing.Point(48, 155)
        Me.B3.Name = "B3"
        Me.B3.Size = New System.Drawing.Size(32, 100)
        Me.B3.TabIndex = 1
        Me.B3.Text = "4"
        Me.B3.UseVisualStyleBackColor = True
        '
        'B4
        '
        Me.B4.Location = New System.Drawing.Point(294, 155)
        Me.B4.Name = "B4"
        Me.B4.Size = New System.Drawing.Size(32, 100)
        Me.B4.TabIndex = 2
        Me.B4.Text = "5"
        Me.B4.UseVisualStyleBackColor = True
        '
        'B1
        '
        Me.B1.Location = New System.Drawing.Point(137, 319)
        Me.B1.Name = "B1"
        Me.B1.Size = New System.Drawing.Size(100, 32)
        Me.B1.TabIndex = 3
        Me.B1.Text = "2"
        Me.B1.UseVisualStyleBackColor = True
        '
        'B0
        '
        Me.B0.Location = New System.Drawing.Point(127, 38)
        Me.B0.Name = "B0"
        Me.B0.Size = New System.Drawing.Size(120, 32)
        Me.B0.TabIndex = 4
        Me.B0.Text = "Trigger"
        Me.B0.UseVisualStyleBackColor = True
        '
        'B5
        '
        Me.B5.Location = New System.Drawing.Point(10, 105)
        Me.B5.Name = "B5"
        Me.B5.Size = New System.Drawing.Size(32, 97)
        Me.B5.TabIndex = 7
        Me.B5.Text = "6"
        Me.B5.UseVisualStyleBackColor = True
        '
        'B2
        '
        Me.B2.Location = New System.Drawing.Point(137, 75)
        Me.B2.Name = "B2"
        Me.B2.Size = New System.Drawing.Size(100, 32)
        Me.B2.TabIndex = 8
        Me.B2.Text = "3"
        Me.B2.UseVisualStyleBackColor = True
        '
        'B6
        '
        Me.B6.Location = New System.Drawing.Point(10, 208)
        Me.B6.Name = "B6"
        Me.B6.Size = New System.Drawing.Size(32, 97)
        Me.B6.TabIndex = 9
        Me.B6.Text = "7"
        Me.B6.UseVisualStyleBackColor = True
        '
        'B9
        '
        Me.B9.Location = New System.Drawing.Point(332, 208)
        Me.B9.Name = "B9"
        Me.B9.Size = New System.Drawing.Size(32, 97)
        Me.B9.TabIndex = 11
        Me.B9.Text = "10"
        Me.B9.UseVisualStyleBackColor = True
        '
        'B10
        '
        Me.B10.Location = New System.Drawing.Point(332, 105)
        Me.B10.Name = "B10"
        Me.B10.Size = New System.Drawing.Size(32, 97)
        Me.B10.TabIndex = 10
        Me.B10.Text = "11"
        Me.B10.UseVisualStyleBackColor = True
        '
        'B7
        '
        Me.B7.Location = New System.Drawing.Point(86, 357)
        Me.B7.Name = "B7"
        Me.B7.Size = New System.Drawing.Size(97, 32)
        Me.B7.TabIndex = 12
        Me.B7.Text = "8"
        Me.B7.UseVisualStyleBackColor = True
        '
        'B8
        '
        Me.B8.Location = New System.Drawing.Point(189, 357)
        Me.B8.Name = "B8"
        Me.B8.Size = New System.Drawing.Size(97, 32)
        Me.B8.TabIndex = 13
        Me.B8.Text = "9"
        Me.B8.UseVisualStyleBackColor = True
        '
        'ConnectLabel
        '
        Me.ConnectLabel.AutoSize = True
        Me.ConnectLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConnectLabel.Location = New System.Drawing.Point(112, 446)
        Me.ConnectLabel.Name = "ConnectLabel"
        Me.ConnectLabel.Size = New System.Drawing.Size(32, 15)
        Me.ConnectLabel.TabIndex = 16
        Me.ConnectLabel.TabStop = True
        Me.ConnectLabel.Text = "here"
        '
        'DisconnectedLabel
        '
        Me.DisconnectedLabel.AutoSize = True
        Me.DisconnectedLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DisconnectedLabel.Location = New System.Drawing.Point(15, 401)
        Me.DisconnectedLabel.Name = "DisconnectedLabel"
        Me.DisconnectedLabel.Size = New System.Drawing.Size(132, 75)
        Me.DisconnectedLabel.TabIndex = 15
        Me.DisconnectedLabel.Text = "This controller is" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "disconnected; choose" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Application >> Connect" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Controller or c" & _
    "lick" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to connect it."
        '
        'ApplicationToolStrip
        '
        Me.ApplicationToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ApplicationDropDown, Me.ViewDropDown})
        Me.ApplicationToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ApplicationToolStrip.Name = "ApplicationToolStrip"
        Me.ApplicationToolStrip.ShowItemToolTips = False
        Me.ApplicationToolStrip.Size = New System.Drawing.Size(375, 25)
        Me.ApplicationToolStrip.TabIndex = 17
        Me.ApplicationToolStrip.Text = "ApplicationToolStrip"
        '
        'ApplicationDropDown
        '
        Me.ApplicationDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ApplicationDropDown.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConnectControllerItem, Me.DisconnectControllerItem, Me.ToolStripSeparator1, Me.AboutMenuItem, Me.ToolStripSeparator2, Me.ExitMenuItem})
        Me.ApplicationDropDown.Image = CType(resources.GetObject("ApplicationDropDown.Image"), System.Drawing.Image)
        Me.ApplicationDropDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ApplicationDropDown.Name = "ApplicationDropDown"
        Me.ApplicationDropDown.Size = New System.Drawing.Size(81, 22)
        Me.ApplicationDropDown.Text = "&Application"
        '
        'ConnectControllerItem
        '
        Me.ConnectControllerItem.Name = "ConnectControllerItem"
        Me.ConnectControllerItem.Size = New System.Drawing.Size(171, 22)
        Me.ConnectControllerItem.Text = "&Connect Device"
        '
        'DisconnectControllerItem
        '
        Me.DisconnectControllerItem.Name = "DisconnectControllerItem"
        Me.DisconnectControllerItem.Size = New System.Drawing.Size(171, 22)
        Me.DisconnectControllerItem.Text = "&Disconnect Device"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(168, 6)
        '
        'AboutMenuItem
        '
        Me.AboutMenuItem.Name = "AboutMenuItem"
        Me.AboutMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.AboutMenuItem.Text = "&About..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(168, 6)
        '
        'ExitMenuItem
        '
        Me.ExitMenuItem.Name = "ExitMenuItem"
        Me.ExitMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ExitMenuItem.Text = "&Exit"
        '
        'ViewDropDown
        '
        Me.ViewDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ViewDropDown.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AlwaysOnTopMenuItem})
        Me.ViewDropDown.Image = CType(resources.GetObject("ViewDropDown.Image"), System.Drawing.Image)
        Me.ViewDropDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ViewDropDown.Name = "ViewDropDown"
        Me.ViewDropDown.Size = New System.Drawing.Size(45, 22)
        Me.ViewDropDown.Text = "&View"
        '
        'AlwaysOnTopMenuItem
        '
        Me.AlwaysOnTopMenuItem.CheckOnClick = True
        Me.AlwaysOnTopMenuItem.Name = "AlwaysOnTopMenuItem"
        Me.AlwaysOnTopMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AlwaysOnTopMenuItem.Text = "&Always on Top"
        '
        'Joystick1
        '
        Me.Joystick1.EnableButton = False
        Me.Joystick1.Location = New System.Drawing.Point(86, 113)
        Me.Joystick1.Name = "Joystick1"
        Me.Joystick1.Size = New System.Drawing.Size(201, 201)
        Me.Joystick1.TabIndex = 14
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 509)
        Me.Controls.Add(Me.ConnectLabel)
        Me.Controls.Add(Me.DisconnectedLabel)
        Me.Controls.Add(Me.Joystick1)
        Me.Controls.Add(Me.ApplicationToolStrip)
        Me.Controls.Add(Me.B10)
        Me.Controls.Add(Me.B9)
        Me.Controls.Add(Me.B8)
        Me.Controls.Add(Me.B7)
        Me.Controls.Add(Me.B6)
        Me.Controls.Add(Me.B5)
        Me.Controls.Add(Me.B4)
        Me.Controls.Add(Me.B3)
        Me.Controls.Add(Me.B0)
        Me.Controls.Add(Me.B1)
        Me.Controls.Add(Me.B2)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "Virtual Logitech Attack 3"
        Me.ApplicationToolStrip.ResumeLayout(False)
        Me.ApplicationToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents ThrottleLevel As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents ThrottleOutline As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents B3 As System.Windows.Forms.Button
    Friend WithEvents B4 As System.Windows.Forms.Button
    Friend WithEvents B1 As System.Windows.Forms.Button
    Friend WithEvents B0 As System.Windows.Forms.Button
    Friend WithEvents B5 As System.Windows.Forms.Button
    Friend WithEvents B2 As System.Windows.Forms.Button
    Friend WithEvents B6 As System.Windows.Forms.Button
    Friend WithEvents B9 As System.Windows.Forms.Button
    Friend WithEvents B10 As System.Windows.Forms.Button
    Friend WithEvents B7 As System.Windows.Forms.Button
    Friend WithEvents B8 As System.Windows.Forms.Button
    Friend WithEvents Joystick1 As vJoy_Feeder.Joystick
    Friend WithEvents ConnectLabel As System.Windows.Forms.LinkLabel
    Friend WithEvents DisconnectedLabel As System.Windows.Forms.Label
    Friend WithEvents ApplicationToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ApplicationDropDown As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ConnectControllerItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisconnectControllerItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewDropDown As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents AlwaysOnTopMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
