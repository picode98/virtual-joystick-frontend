Imports System.IO
Imports System.IO.Pipes
Imports System.Collections.Generic

Imports Forms = System.Windows.Forms

Public Class Form1
    'Dim pipeServer As NamedPipeServerStream
    'Dim sw As StreamWriter
    Dim clientConnected = False
    Dim JoystickMoving As Boolean = False
    Dim throttleMoving As Boolean = False
    'Dim POVMoving As Boolean = False
    'Const pathToClient As String = "..\..\..\..\vJoy218SDK-291116-Copy\SDK\src\Debug\vJoyClient.exe"
    Dim originalTitle As String = ""
    Dim deviceID As Integer = 0
    Dim buttonHolds(10) As Boolean
    Dim vJoyObject As vJoyControl = New vJoyControl()

    Class controllerData
        Public joystickPosition As Point = New Point(100, 100)
        Public throttlePosition As UInteger = 0
        Public buttons As UInt32 = 0
        'Public POV As Integer = -1
    End Class

    Dim previousControllerData As controllerData = New controllerData
    Dim currentControllerData As controllerData = New controllerData

    Private Sub setDisconnectedState()
        'If Not (IsNothing(pipeServer)) Then
        '    If pipeServer.IsConnected Then
        '        pipeServer.Disconnect()
        '        pipeServer.Close()
        '    End If
        'End If

        If vJoyObject.getConnected() Then
            vJoyObject.releaseVJoy()
        End If

        DisconnectedLabel.Show()
        ConnectLabel.Show()

        ConnectControllerItem.Enabled = True
        DisconnectControllerItem.Enabled = False

        clientConnected = False
        Me.Text = originalTitle & " - Disconnected"
    End Sub

    Public Function vJoyCommand(command As String, arguments As String, runElevated As Boolean) As Integer
        Dim vJoyDirectory As String
        Dim programFiles As String

        If Environment.Is64BitOperatingSystem Then
            programFiles = Environment.GetEnvironmentVariable("ProgramW6432")
            vJoyDirectory = programFiles & "\vJoy\x64"
        Else
            programFiles = Environment.GetEnvironmentVariable("ProgramFiles")
            vJoyDirectory = programFiles & "\vJoy\x86"
        End If

        Dim commandProcess As Process = New Process

        commandProcess.StartInfo.FileName = vJoyDirectory & "\" & command
        commandProcess.StartInfo.WorkingDirectory = vJoyDirectory
        commandProcess.StartInfo.Arguments = arguments

        If runElevated Then
            commandProcess.StartInfo.Verb = "runas"
            commandProcess.StartInfo.UseShellExecute = True
        End If

        commandProcess.Start()
        commandProcess.WaitForExit()

        Return commandProcess.ExitCode
    End Function

    Private Sub flushDeviceData(sendAll As Boolean)
        vJoyObject.UpdateJoystickData(currentControllerData, previousControllerData, sendAll)
        CopyJoystickData(currentControllerData, previousControllerData)
    End Sub

    Private Sub connectDevice(newPipe As Boolean, showIDDialog As Boolean)
        Dim defaultID As Integer = vJoyObject.findNextAvailableID()
        Dim connectionCancel As Boolean = False

        If defaultID = -1 Then
            defaultID = vJoyObject.findNextUnusedID()
        End If

        If showIDDialog Then
            If deviceID = 0 Then
                If defaultID = -1 Then
                    Form3.DeviceIDInput.Text = ""
                Else
                    Form3.DeviceIDInput.Text = defaultID.ToString()
                End If
            Else
                Form3.DeviceIDInput.Text = deviceID.ToString()
            End If

            Form3.ShowDialog()

            If Form3.OKPressed Then
                Dim invalidIDErrorStr As String = "The vJoy ID specified is invalid."

                Try
                    Dim userDeviceID As Integer = Convert.ToInt32(Form3.DeviceIDInput.Text)

                    If userDeviceID >= vJoyControl.vJoyMinID And userDeviceID <= vJoyControl.vJoyMaxID Then
                        deviceID = userDeviceID
                    Else
                        MessageBox.Show(invalidIDErrorStr, "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        connectionCancel = True
                        setDisconnectedState()
                    End If
                Catch ex As System.FormatException
                    MessageBox.Show(invalidIDErrorStr, "Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    connectionCancel = True
                    setDisconnectedState()
                End Try
            Else
                connectionCancel = True
                setDisconnectedState()
            End If
        Else
            If deviceID = 0 Then
                deviceID = defaultID
            End If
        End If

        If Not (connectionCancel) Then
            Me.Text += (" - Waiting for vJoy device " + Convert.ToString(deviceID))

            Dim initResult As Integer = vJoyObject.initVJoy(deviceID)

            If vJoyObject.getConnected() Then
                clientConnected = True
                DisconnectedLabel.Hide()
                ConnectLabel.Hide()

                DisconnectControllerItem.Enabled = True
                ConnectControllerItem.Enabled = False

                Me.Text = originalTitle & " - vJoy Device ID " & Convert.ToString(deviceID)
                flushDeviceData(True)
            Else
                If initResult = -4 Or initResult = -6 Then
                    Dim configureDialogResult As DialogResult
                    Dim dialogMessage As String = ""

                    If initResult = -4 Then
                        dialogMessage = "Device ID " & deviceID.ToString() & " does not exist. Create it?"
                    ElseIf initResult = -6 Then
                        dialogMessage = "Device ID " & deviceID.ToString() & " is not configured properly for use with this application. Configure it?"
                    End If

                    configureDialogResult = MessageBox.Show(dialogMessage, "vJoy Configuration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

                    If configureDialogResult = Forms.DialogResult.Yes Then
                        Try
                            vJoyCommand("vJoyConfig.exe", deviceID.ToString() & " -f -a x y z -b 11", True)
                            connectDevice(False, False)
                        Catch ex As System.ComponentModel.Win32Exception
                            setDisconnectedState()
                        End Try
                    Else
                        setDisconnectedState()
                    End If
                Else
                    MessageBox.Show(vJoyObject.initErrorStr(initResult), "vJoy Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    setDisconnectedState()
                End If
            End If
        End If

        'Dim vJoyClientProcess As Process = New Process()
        'vJoyClientProcess.StartInfo.FileName = pathToClient
        'vJoyClientProcess.StartInfo.Arguments = Convert.ToString(deviceID) + " X-Box_Controller"
        'vJoyClientProcess.StartInfo.UseShellExecute = False
        'vJoyClientProcess.StartInfo.RedirectStandardError = True
        'vJoyClientProcess.StartInfo.CreateNoWindow = True
        'vJoyClientMonitorWorker.RunWorkerAsync(vJoyClientProcess)

        'If newPipe And Not (BackgroundWorker1.IsBusy) Then
        '    pipeServer = New NamedPipeServerStream("vJoyPipe" & Convert.ToString(deviceID), PipeDirection.Out, 1)
        '    BackgroundWorker1.RunWorkerAsync(New String("Init"))
        'End If
    End Sub


    Private Function SetButton(existingValue As UInt32, buttonNumber As UInteger, newValue As Boolean)
        Dim returnVal As UInt32
        If newValue Then
            returnVal = existingValue Or (1 << buttonNumber)
        Else
            returnVal = existingValue And (Not (1 << buttonNumber))
        End If

        Return returnVal
    End Function

    Private Function GetButton(existingValue As UInt32, buttonNumber As UInteger)
        Dim filteredValue As UInt32 = existingValue And (1 << buttonNumber)
        Dim returnValue As Boolean

        If filteredValue = 0 Then
            returnValue = False
        Else
            returnValue = True
        End If

        Return returnValue
    End Function

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Dim thisProcessName As String = Process.GetCurrentProcess().ProcessName
        'Dim existingFeeders As Process() = Process.GetProcessesByName(thisProcessName)

        'deviceID = existingFeeders.Length

        originalTitle = Me.Text

        Try
            connectDevice(True, False)
        Catch ex As System.IO.IOException
            setDisconnectedState()
        End Try

        'Me.Text += (" - Waiting for vJoy device " + Convert.ToString(existingFeeders.Length))

        'Dim vJoyClientProcess As Process = New Process()
        'vJoyClientProcess.StartInfo.FileName = pathToClient
        'vJoyClientProcess.StartInfo.Arguments = Convert.ToString(existingFeeders.Length) + " Joystick"
        'vJoyClientProcess.StartInfo.UseShellExecute = False
        'vJoyClientProcess.StartInfo.RedirectStandardError = True
        'vJoyClientMonitorWorker.RunWorkerAsync(vJoyClientProcess)

        'pipeServer = New NamedPipeServerStream("vJoyPipe" & Convert.ToString(existingFeeders.Length), PipeDirection.Out, 1)
        'Joystick1Indicator.SendToBack()
        'Joystick1Indicator.Location = Joystick1.Location + New Point(100, 100) - (New Point(Joystick1Indicator.Size.Width / 2, Joystick1Indicator.Size.Height / 2))
        ThrottleLevel.SendToBack()
        SetTriggerPosition(ThrottleOutline, ThrottleLevel, 0, currentControllerData.throttlePosition)

        For Each thisControl As Forms.Control In Me.Controls
            If thisControl.GetType() Is GetType(Forms.Button) Then
                Dim buttonControl As Forms.Button = CType(thisControl, Forms.Button)
                If Microsoft.VisualBasic.Left(buttonControl.Name, 1) = "B" Then
                    AddHandler buttonControl.MouseUp, AddressOf Button_MouseUp
                    AddHandler buttonControl.MouseDown, AddressOf Button_MouseDown
                End If
            End If
        Next

        'For Each thisControl As Forms.Control In Me.Controls
        '    If thisControl.GetType() Is GetType(Microsoft.VisualBasic.PowerPacks.ShapeContainer) Then
        '        Dim shapeContainer As Microsoft.VisualBasic.PowerPacks.ShapeContainer = CType(thisControl, Microsoft.VisualBasic.PowerPacks.ShapeContainer)
        '        For Each thisShape As Microsoft.VisualBasic.PowerPacks.Shape In shapeContainer.Shapes
        '            If thisShape.GetType() Is GetType(Microsoft.VisualBasic.PowerPacks.RectangleShape) Then
        '                Dim rectangleShape As Microsoft.VisualBasic.PowerPacks.RectangleShape = CType(thisShape, Microsoft.VisualBasic.PowerPacks.RectangleShape)
        '                If Microsoft.VisualBasic.Left(rectangleShape.Name, 3) = "POV" Then
        '                    AddHandler rectangleShape.MouseDown, AddressOf POV_MouseDown
        '                    AddHandler rectangleShape.MouseMove, AddressOf POV_MouseMove
        '                    AddHandler rectangleShape.MouseUp, AddressOf POV_MouseUp
        '                    AddHandler rectangleShape.MouseLeave, AddressOf POV_MouseLeave
        '                End If
        '            End If
        '        Next
        '    End If
        'Next

        'Dim centerPOVPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(-1)
        'centerPOVPanel.FillStyle = PowerPacks.FillStyle.Solid
        'centerPOVPanel.FillColor = Color.DarkGray

        'Dim topLeftPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(315)
        'Dim bottomRightPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(135)

        'POVBoundingRect = New Rectangle(topLeftPanel.Location, New Point(bottomRightPanel.Right - topLeftPanel.Left, bottomRightPanel.Bottom - topLeftPanel.Top))

        'BackgroundWorker1.RunWorkerAsync(New String("Init"))
    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'pipeServer.Close()
        vJoyObject.releaseVJoy()
    End Sub

    Private Sub CopyJoystickData(ByRef source As controllerData, ByRef destination As controllerData)
        destination.joystickPosition = source.joystickPosition
        destination.throttlePosition = source.throttlePosition
        destination.buttons = source.buttons
        'destination.POV = source.POV
    End Sub

    Private Function CompareJoystickData(argument1 As controllerData, argument2 As controllerData) As Boolean
        If Not (argument1.joystickPosition = argument2.joystickPosition) Then
            Return False
        End If

        If Not (argument1.throttlePosition = argument2.throttlePosition) Then
            Return False
        End If

        If Not (argument1.buttons = argument2.buttons) Then
            Return False
        End If

        'If Not (argument1.POV = argument2.POV) Then
        '    Return False
        'End If

        Return True
    End Function

    'Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs)
    '    PipeOperation(CType(e.Argument, String))
    'End Sub

    'Private Sub PipeOperation(operation As String)
    '    If operation = "Init" Then
    '        pipeServer.WaitForConnection()
    '        Me.Invoke(Sub()
    '                      Me.Text = originalTitle & " - vJoy Device ID " & Convert.ToString(deviceID)
    '                  End Sub)

    '        sw = New StreamWriter(pipeServer)
    '        sw.AutoFlush = True

    '        clientConnected = True

    '        PipeOperation("SendAllData")
    '    ElseIf operation = "SendData" Or operation = "SendAllData" Then
    '        Dim changeList As List(Of String) = New List(Of String)
    '        Dim dataString As String = New String("")

    '        If operation = "SendAllData" Or Not (previousControllerData.joystickPosition = currentControllerData.joystickPosition) Then
    '            changeList.Add("J:" & currentControllerData.joystickPosition.X & "," & currentControllerData.joystickPosition.Y)
    '        End If

    '        If operation = "SendAllData" Or Not (previousControllerData.throttlePosition = currentControllerData.throttlePosition) Then
    '            changeList.Add("T:" & currentControllerData.throttlePosition)
    '        End If

    '        If operation = "SendAllData" Or Not (previousControllerData.buttons = currentControllerData.buttons) Then
    '            changeList.Add("B:" & currentControllerData.buttons)
    '        End If

    '        'If operation = "SendAllData" Or Not (previousControllerData.POV = currentControllerData.POV) Then
    '        '    changeList.Add("POV:" & currentControllerData.POV)
    '        'End If

    '        CopyJoystickData(currentControllerData, previousControllerData)

    '        dataString = String.Join(";", changeList.ToArray())
    '        If Not (dataString = "") Then
    '            sw.Write(dataString + vbNullChar)
    '            'pipeServer.WaitForPipeDrain()

    '            If Not CompareJoystickData(previousControllerData, currentControllerData) Then
    '                Debug.WriteLine("Data updated while pipe was busy; calling again.")
    '                flushDeviceData(False)
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub Joystick1_MouseMove(sender As System.Object, e As Forms.MouseEventArgs)
    '    If JoystickMoving Then
    '        currentControllerData.joystickPosition = New Point(e.X, e.Y)
    '        Joystick1Indicator.Location = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape).Location + currentControllerData.joystickPosition - (New Point(Joystick1Indicator.Size.Width / 2, Joystick1Indicator.Size.Height / 2))
    '        If clientConnected Then
    '            flushDeviceData(False)
    '        End If
    '        'If Not BackgroundWorker1.IsBusy Then
    '        '    BackgroundWorker1.RunWorkerAsync("SendData")
    '        'End If
    '    End If
    'End Sub

    'Private Sub Joystick1_MouseDown(sender As System.Object, e As Forms.MouseEventArgs)
    '    'If e.Button = Forms.MouseButtons.Left Then

    '    currentControllerData.joystickPosition = New Point(e.X, e.Y)
    '    Joystick1Indicator.Location = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape).Location + currentControllerData.joystickPosition - (New Point(Joystick1Indicator.Size.Width / 2, Joystick1Indicator.Size.Height / 2))
    '    If clientConnected Then
    '        flushDeviceData(False)
    '    End If

    '    'If Not BackgroundWorker1.IsBusy Then
    '    '    BackgroundWorker1.RunWorkerAsync("SendData")
    '    'End If

    '    JoystickMoving = True
    '    'ElseIf e.Button = Forms.MouseButtons.Middle Then
    '    '    currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, True)

    '    '    If clientConnected Then
    '    '        flushDeviceData(False)
    '    '    End If

    '    '    Dim joystickControl As Microsoft.VisualBasic.PowerPacks.RectangleShape = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape)
    '    '    joystickControl.FillColor = Color.DarkGray
    '    '    joystickControl.FillStyle = PowerPacks.FillStyle.SmallCheckerBoard
    '    'End If
    'End Sub

    'Private Sub Joystick1_MouseUp(sender As System.Object, e As Forms.MouseEventArgs)
    '    'If e.Button = Forms.MouseButtons.Left Then

    '    currentControllerData.joystickPosition = New Point(100, 100)
    '    Joystick1Indicator.Location = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape).Location + currentControllerData.joystickPosition - (New Point(Joystick1Indicator.Size.Width / 2, Joystick1Indicator.Size.Height / 2))
    '    If clientConnected Then
    '        flushDeviceData(False)
    '    End If

    '    'If Not BackgroundWorker1.IsBusy Then
    '    '    BackgroundWorker1.RunWorkerAsync("SendData")
    '    'Else
    '    '    Debug.WriteLine("Pipe is busy!")
    '    'End If

    '    JoystickMoving = False
    '    'ElseIf e.Button = Forms.MouseButtons.Middle Then
    '    '    currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, False)

    '    '    If clientConnected Then
    '    '        flushDeviceData(False)
    '    '    End If

    '    '    Dim joystickControl As Microsoft.VisualBasic.PowerPacks.RectangleShape = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape)
    '    '    joystickControl.FillStyle = PowerPacks.FillStyle.Transparent
    '    'End If
    'End Sub

    Private Sub Form1_MouseUp(sender As System.Object, e As Forms.MouseEventArgs) Handles MyBase.MouseUp
        Joystick1.parentFormMouseUp(sender, e)
        'If JoystickMoving Then
        '    If e.Button = Forms.MouseButtons.Left Then
        '        currentControllerData.joystickPosition = New Point(100, 100)
        '        Joystick1Indicator.Location = Joystick1.Location + currentControllerData.joystickPosition - (New Point(Joystick1Indicator.Size.Width / 2, Joystick1Indicator.Size.Height / 2))
        '        If clientConnected Then
        '            flushDeviceData(False)
        '        End If
        '        'If Not BackgroundWorker1.IsBusy Then
        '        '    BackgroundWorker1.RunWorkerAsync("SendData")
        '        'End If

        '        JoystickMoving = False
        '    ElseIf e.Button = Forms.MouseButtons.Middle Then
        '        currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, False)

        '        If clientConnected Then
        '            flushDeviceData(False)
        '        End If

        '        Joystick1.FillStyle = PowerPacks.FillStyle.Transparent
        '    End If
        'End If

        If throttleMoving Then
            throttleMoving = False
            'SetTriggerPosition(Trigger1Outline, Trigger1Level, 0, currentControllerData.trigger1Position)
        End If

        'If POVMoving Then
        '    Dim centerPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(-1)
        '    centerPanel.FillColor = Color.DarkGray
        '    centerPanel.FillStyle = PowerPacks.FillStyle.Solid

        '    Dim highlightedPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(currentControllerData.POV)
        '    highlightedPanel.FillStyle = PowerPacks.FillStyle.Transparent

        '    POVMoving = False
        '    currentControllerData.POV = -1

        '    If clientConnected Then
        '        flushDeviceData(False)
        '    End If
        '    'For angle As Integer = 45 To 315 Step 45
        '    '    Dim thisPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(angle)
        '    '    thisPanel.FillStyle = PowerPacks.FillStyle.Transparent
        '    'Next
        'End If
    End Sub

    Private Sub SetTriggerPosition(ByRef outlineObject As Microsoft.VisualBasic.PowerPacks.RectangleShape, ByRef levelObject As Microsoft.VisualBasic.PowerPacks.RectangleShape, position As UInteger, ByRef positionDestination As UInteger)
        levelObject.Location = New Point(levelObject.Location.X, outlineObject.Location.Y + (outlineObject.Size.Height - position))
        levelObject.Size = New Size(outlineObject.Size.Width, position)

        'If levelObject.Name = "Trigger1Level" Then
        '    currentControllerData.trigger1Position = GetTriggerPosition(Trigger1Outline, Trigger1Level)
        'End If
        positionDestination = position

        If clientConnected Then
            flushDeviceData(False)
        End If
    End Sub

    Private Function GetTriggerPosition(ByRef outlineObject As Microsoft.VisualBasic.PowerPacks.RectangleShape, ByRef levelObject As Microsoft.VisualBasic.PowerPacks.RectangleShape) As UInteger
        Return (outlineObject.Size.Height - (levelObject.Location.Y - outlineObject.Location.Y))
    End Function

    Private Sub Trigger1Outline_MouseMove(sender As System.Object, e As Forms.MouseEventArgs) Handles ThrottleOutline.MouseMove
        If throttleMoving Then
            SetTriggerPosition(ThrottleOutline, ThrottleLevel, (CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape).Size.Height - e.Y), currentControllerData.throttlePosition)
        End If
    End Sub

    Private Sub Trigger1Outline_MouseDown(sender As System.Object, e As Forms.MouseEventArgs) Handles ThrottleOutline.MouseDown
        throttleMoving = True
        SetTriggerPosition(ThrottleOutline, ThrottleLevel, (CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape).Size.Height - e.Y), currentControllerData.throttlePosition)
    End Sub

    Private Sub Trigger1Outline_MouseUp(sender As System.Object, e As Forms.MouseEventArgs) Handles ThrottleOutline.MouseUp
        throttleMoving = False
        'SetTriggerPosition(ThrottleOutline, ThrottleLevel, 0, currentControllerData.throttlePosition)
    End Sub

    Private Sub Button_MouseDown(sender As System.Object, e As Forms.MouseEventArgs)
        If sender.GetType() Is GetType(Forms.Button) Then
            Dim buttonControl As Forms.Button = CType(sender, Forms.Button)
            If Microsoft.VisualBasic.Left(buttonControl.Name, 1) = "B" Then
                Dim buttonNumStr As String = Microsoft.VisualBasic.Right(buttonControl.Name, buttonControl.Name.Length - 1)
                Dim buttonNumInt As Integer = Convert.ToInt32(buttonNumStr)

                If e.Button = Forms.MouseButtons.Left Then
                    currentControllerData.buttons = SetButton(currentControllerData.buttons, buttonNumInt, True)

                    If buttonHolds(buttonNumInt) Then
                        buttonControl.Text = Microsoft.VisualBasic.Left(buttonControl.Text, buttonControl.Text.Length - 1)
                        buttonHolds(buttonNumInt) = False
                    End If
                End If

                If clientConnected Then
                    flushDeviceData(False)
                End If
            End If
        End If
    End Sub

    Private Sub Button_MouseUp(sender As System.Object, e As Forms.MouseEventArgs)
        If sender.GetType() Is GetType(Forms.Button) Then
            Dim buttonControl As Forms.Button = CType(sender, Forms.Button)
            If Microsoft.VisualBasic.Left(buttonControl.Name, 1) = "B" Then
                Dim buttonNumStr As String = Microsoft.VisualBasic.Right(buttonControl.Name, buttonControl.Name.Length - 1)
                Dim buttonNumInt As Integer = Convert.ToInt32(buttonNumStr)

                If e.Button = Forms.MouseButtons.Left And Not (buttonHolds(buttonNumInt)) Then
                    currentControllerData.buttons = SetButton(currentControllerData.buttons, buttonNumInt, False)
                ElseIf e.Button = Forms.MouseButtons.Right Then
                    If Not (buttonHolds(buttonNumInt)) Then
                        If GetButton(currentControllerData.buttons, buttonNumInt) Then
                            buttonControl.Text += "*"
                            buttonHolds(buttonNumInt) = True
                        End If
                    Else
                        currentControllerData.buttons = SetButton(currentControllerData.buttons, buttonNumInt, False)
                        buttonControl.Text = Microsoft.VisualBasic.Left(buttonControl.Text, buttonControl.Text.Length - 1)
                        buttonHolds(buttonNumInt) = False
                    End If
                End If

                If clientConnected Then
                    flushDeviceData(False)
                End If
            End If
        End If
    End Sub

    'Private Function FindPOVPanel(POVValue As Integer) As Microsoft.VisualBasic.PowerPacks.RectangleShape
    '    Dim POVPanelName As String = "POV" & Convert.ToString(POVValue).Replace("-", "n")
    '    Dim result As Microsoft.VisualBasic.PowerPacks.RectangleShape = Nothing

    '    For Each thisControl As Forms.Control In Me.Controls
    '        If thisControl.GetType() Is GetType(Microsoft.VisualBasic.PowerPacks.ShapeContainer) Then
    '            Dim shapeContainer As Microsoft.VisualBasic.PowerPacks.ShapeContainer = CType(thisControl, Microsoft.VisualBasic.PowerPacks.ShapeContainer)
    '            For Each thisShape As Microsoft.VisualBasic.PowerPacks.Shape In shapeContainer.Shapes
    '                If thisShape.GetType() Is GetType(Microsoft.VisualBasic.PowerPacks.RectangleShape) Then
    '                    Dim rectangleShape As Microsoft.VisualBasic.PowerPacks.RectangleShape = CType(thisShape, Microsoft.VisualBasic.PowerPacks.RectangleShape)

    '                    If rectangleShape.Name = POVPanelName Then
    '                        result = rectangleShape
    '                    End If
    '                End If
    '            Next
    '        End If
    '    Next

    '    Return result
    'End Function

    'Private Sub POV_MouseDown(sender As System.Object, e As Forms.MouseEventArgs)
    '    If sender.GetType() Is GetType(Microsoft.VisualBasic.PowerPacks.RectangleShape) Then
    '        Dim POVPanelControl As Microsoft.VisualBasic.PowerPacks.RectangleShape = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape)

    '        If Microsoft.VisualBasic.Left(POVPanelControl.Name, 3) = "POV" Then
    '            Dim POVNumStr As String = Microsoft.VisualBasic.Right(POVPanelControl.Name, POVPanelControl.Name.Length - 3)
    '            Dim POVNumInt As Integer = Convert.ToInt32(POVNumStr.Replace("n", "-"))

    '            If Not (currentControllerData.POV = POVNumInt) Then
    '                Dim previousPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(currentControllerData.POV)
    '                previousPanel.FillStyle = PowerPacks.FillStyle.Transparent
    '            End If

    '            POVMoving = True
    '            currentControllerData.POV = POVNumInt

    '            POVPanelControl.FillColor = Color.DarkGray
    '            POVPanelControl.FillStyle = PowerPacks.FillStyle.Solid

    '            If clientConnected Then
    '                flushDeviceData(False)
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub POV_MouseMove(sender As System.Object, e As Forms.MouseEventArgs)
    '    If POVMoving Then
    '        If sender.GetType() Is GetType(Microsoft.VisualBasic.PowerPacks.RectangleShape) Then
    '            Dim POVPanelControl As Microsoft.VisualBasic.PowerPacks.RectangleShape = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape)

    '            If Microsoft.VisualBasic.Left(POVPanelControl.Name, 3) = "POV" Then
    '                Dim POVNumStr As String = Microsoft.VisualBasic.Right(POVPanelControl.Name, POVPanelControl.Name.Length - 3)
    '                Dim POVNumInt As Integer = Convert.ToInt32(POVNumStr.Replace("n", "-"))

    '                If Not (currentControllerData.POV = POVNumInt) Then
    '                    Dim previousPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(currentControllerData.POV)
    '                    previousPanel.FillStyle = PowerPacks.FillStyle.Transparent
    '                End If

    '                currentControllerData.POV = POVNumInt

    '                POVPanelControl.FillColor = Color.DarkGray
    '                POVPanelControl.FillStyle = PowerPacks.FillStyle.Solid

    '                If clientConnected Then
    '                    flushDeviceData(False)
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub POV_MouseUp(sender As System.Object, e As Forms.MouseEventArgs)
    '    If sender.GetType() Is GetType(Microsoft.VisualBasic.PowerPacks.RectangleShape) Then
    '        Dim POVPanelControl As Microsoft.VisualBasic.PowerPacks.RectangleShape = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape)

    '        If Microsoft.VisualBasic.Left(POVPanelControl.Name, 3) = "POV" Then
    '            Dim POVNumStr As String = Microsoft.VisualBasic.Right(POVPanelControl.Name, POVPanelControl.Name.Length - 3)
    '            Dim POVNumInt As Integer = Convert.ToInt32(POVNumStr.Replace("n", "-"))

    '            POVMoving = False
    '            currentControllerData.POV = -1

    '            POVPanelControl.FillStyle = PowerPacks.FillStyle.Transparent

    '            Dim centerPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(-1)
    '            centerPanel.FillColor = Color.DarkGray
    '            centerPanel.FillStyle = PowerPacks.FillStyle.Solid

    '            'Me.Capture = False
    '            'POVPanelControl.GetContainerControl().ActiveControl.Capture = True

    '            If clientConnected Then
    '                flushDeviceData(False)
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub POV_MouseLeave(sender As System.Object, e As System.EventArgs)
    '    If sender.GetType() Is GetType(Microsoft.VisualBasic.PowerPacks.RectangleShape) Then
    '        If POVMoving And Not POVBoundingRect.Contains(PointToClient(MousePosition)) Then
    '            Me.Capture = True
    '        End If
    '    End If
    'End Sub

    'Private Sub vJoyClientMonitorWorker_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs)
    '    Dim vJoyClientProcess As Process = CType(e.Argument, Process)

    '    vJoyClientProcess.Start()
    '    vJoyClientProcess.WaitForExit()

    '    Dim errorText As String = vJoyClientProcess.StandardError.ReadToEnd()

    '    If Not (errorText = "") Then
    '        Me.Invoke(Sub()
    '                      MessageBox.Show(errorText, "vJoy Client Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                  End Sub)
    '    End If

    '    Me.Close()
    'End Sub

    Private Sub ThrottleOutline_MouseLeave(sender As System.Object, e As System.EventArgs) Handles ThrottleOutline.MouseLeave
        If throttleMoving Then
            Me.Capture = True
        End If
    End Sub

    'Private Sub Joystick1_MouseLeave(sender As System.Object, e As System.EventArgs)
    '    If JoystickMoving Then
    '        Me.Capture = True
    '    End If
    'End Sub

    Private Function coerceToRange(minimum As Long, maximium As Long, value As Long)
        Dim returnValue As Long

        If value < minimum Then
            returnValue = minimum
        ElseIf value > maximium Then
            returnValue = maximium
        Else
            returnValue = value
        End If

        Return returnValue
    End Function

    'Private Sub Trigger1Outline_MouseEnter(sender As System.Object, e As System.EventArgs) Handles Trigger1Outline.MouseEnter
    '    If Trigger1Moving Then
    '        Trigger1Outline.Container.Capture = True
    '        Me.Capture = False
    '    End If
    'End Sub

    Private Sub Form1_MouseMove(sender As System.Object, e As Forms.MouseEventArgs) Handles MyBase.MouseMove
        Joystick1.parentFormMouseMove(sender, e)
        'If JoystickMoving Then
        '    If Joystick1.Bounds.Contains(e.Location) Then
        '        Joystick1.GetContainerControl().ActiveControl.Capture = True
        '        Me.Capture = False
        '    Else
        '        Dim newJoystickPosition As Point = New Point(coerceToRange(0, Joystick1.Width, e.X - Joystick1.Location.X), coerceToRange(0, Joystick1.Height, e.Y - Joystick1.Location.Y))
        '        currentControllerData.joystickPosition = newJoystickPosition
        '        Joystick1Indicator.Location = Joystick1.Location + currentControllerData.joystickPosition - (New Point(Joystick1Indicator.Size.Width / 2, Joystick1Indicator.Size.Height / 2))

        '        If clientConnected Then
        '            flushDeviceData(False)
        '        End If
        '    End If
        'End If

        If throttleMoving Then
            If ThrottleOutline.Bounds.Contains(e.Location) Then
                ThrottleOutline.GetContainerControl().ActiveControl.Capture = True
                Me.Capture = False
            Else
                SetTriggerPosition(ThrottleOutline, ThrottleLevel, coerceToRange(0, ThrottleOutline.Height, ThrottleOutline.Size.Height - (e.Y - ThrottleOutline.Location.Y)), currentControllerData.throttlePosition)
            End If
        End If

        'If POVMoving Then
        '    If POVBoundingRect.Contains(e.Location) Then
        '        Dim mousePanel, centerPOVPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape
        '        mousePanel = Nothing
        '        centerPOVPanel = FindPOVPanel(-1)

        '        If centerPOVPanel.Bounds.Contains(e.Location) Then
        '            mousePanel = centerPOVPanel
        '        Else
        '            For angle As Integer = 0 To 315 Step 45
        '                Dim thisPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(angle)

        '                If thisPanel.Bounds.Contains(e.Location) Then
        '                    mousePanel = thisPanel
        '                End If
        '            Next
        '        End If

        '        If Not IsNothing(mousePanel) Then
        '            mousePanel.GetContainerControl().ActiveControl.Capture = True
        '            Me.Capture = False
        '        End If
        '    Else
        '        Dim topLeftPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(315)
        '        Dim topRightPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(45)
        '        Dim POVAngle As Integer = Nothing

        '        If e.Location.X < topLeftPanel.Right Then
        '            Dim bottomLeftPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(225)
        '            If e.Location.Y < topLeftPanel.Bottom Then
        '                POVAngle = 315
        '            ElseIf e.Location.Y > bottomLeftPanel.Top Then
        '                POVAngle = 225
        '            Else
        '                POVAngle = 270
        '            End If
        '        ElseIf e.Location.X > topRightPanel.Left Then
        '            Dim bottomRightPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(135)
        '            If e.Location.Y < topRightPanel.Bottom Then
        '                POVAngle = 45
        '            ElseIf e.Location.Y > bottomRightPanel.Top Then
        '                POVAngle = 135
        '            Else
        '                POVAngle = 90
        '            End If
        '        Else
        '            If e.Location.Y < POVBoundingRect.Top Then
        '                POVAngle = 0
        '            ElseIf e.Location.Y > POVBoundingRect.Bottom Then
        '                POVAngle = 180
        '            End If
        '        End If

        '        If Not IsNothing(POVAngle) Then
        '            If Not (POVAngle = currentControllerData.POV) Then
        '                Dim currentPOVPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(currentControllerData.POV)
        '                currentPOVPanel.FillStyle = PowerPacks.FillStyle.Transparent

        '                Dim newPOVPanel As Microsoft.VisualBasic.PowerPacks.RectangleShape = FindPOVPanel(POVAngle)

        '                newPOVPanel.FillColor = Color.DarkGray
        '                newPOVPanel.FillStyle = PowerPacks.FillStyle.Solid

        '                currentControllerData.POV = POVAngle

        '                If clientConnected Then
        '                    flushDeviceData(False)
        '                End If
        '            End If
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As Forms.MouseEventArgs) Handles Me.MouseDown
        Joystick1.parentFormMouseDown(sender, e)
        'If JoystickMoving Then
        '    If e.Button = Forms.MouseButtons.Middle Then
        '        currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, True)

        '        If clientConnected Then
        '            flushDeviceData(False)
        '        End If

        '        Joystick1.FillColor = Color.DarkGray
        '        Joystick1.FillStyle = PowerPacks.FillStyle.SmallCheckerBoard
        '    End If
        'End If

        'If Joystick2Moving Then
        '    If e.Button = Forms.MouseButtons.Middle Then
        '        currentControllerData.buttons = SetButton(currentControllerData.buttons, 9, True)

        '        If clientConnected Then
        '            flushDeviceData(False)
        '        End If

        '        Joystick2.FillColor = Color.DarkGray
        '        Joystick2.FillStyle = PowerPacks.FillStyle.SmallCheckerBoard
        '    End If
        'End If
    End Sub

    Private Sub AlwaysOnTopMenuItem_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles AlwaysOnTopMenuItem.CheckedChanged
        Dim menuItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Me.TopMost = menuItem.Checked
    End Sub

    Private Sub AboutMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AboutMenuItem.Click
        AboutDialog.ShowDialog()
    End Sub

    Private Sub ExitMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitMenuItem.Click
        Me.Close()
    End Sub

    Private Sub Joystick1_UpdateAxes() Handles Joystick1.UpdateAxes
        currentControllerData.joystickPosition = Joystick1.JoystickPosition

        If clientConnected Then
            flushDeviceData(False)
        End If
    End Sub

    Private Sub ConnectControllerItem_Click(sender As System.Object, e As System.EventArgs) Handles ConnectControllerItem.Click
        'If Not (vJoyClientMonitorWorker.IsBusy) Then
        '    Try
        '        connectDevice(True, True)
        '    Catch ex As System.IO.IOException
        '        setDisconnectedState()
        '    End Try
        'End If

        If Not (vJoyObject.getConnected()) Then
            connectDevice(True, True)
        End If
    End Sub

    Private Sub DisconnectControllerItem_Click(sender As System.Object, e As System.EventArgs) Handles DisconnectControllerItem.Click
        setDisconnectedState()
    End Sub

    Private Sub ConnectLabel_LinkClicked(sender As System.Object, e As Forms.LinkLabelLinkClickedEventArgs) Handles ConnectLabel.LinkClicked
        If Not (vJoyObject.getConnected()) Then
            connectDevice(True, True)
        End If
    End Sub
End Class
