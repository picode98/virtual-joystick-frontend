Imports System.Runtime.InteropServices
Imports vJoyInterfaceWrap

Public Class vJoyControl
    'Private Declare Function AcquireVJD Lib "vJoyInterface.dll" (rID As UInteger) As Boolean
    'Private Declare Sub RelinquishVJD Lib "vJoyInterface.dll" (rID As UInteger)
    'Private Declare Sub SetContPov Lib "vJoyInterface.dll" (Value As ULong)
    Private joystick As vJoy = New vJoy()
    Private joystickData As vJoy.JoystickState
    Private deviceID As Integer = 0
    Private isConnected As Boolean = False
    Public Const vJoyMinID As Integer = 1
    Public Const vJoyMaxID As Integer = 16

    Private Function isValidController()
        Dim returnValue As Boolean = True

        If (Not (joystick.GetVJDButtonNumber(deviceID) = 10) Or Not (joystick.GetVJDAxisExist(deviceID, HID_USAGES.HID_USAGE_X)) Or Not (joystick.GetVJDAxisExist(deviceID, HID_USAGES.HID_USAGE_Y)) _
              Or Not (joystick.GetVJDAxisExist(deviceID, HID_USAGES.HID_USAGE_Z)) Or Not (joystick.GetVJDAxisExist(deviceID, HID_USAGES.HID_USAGE_RX)) Or Not (joystick.GetVJDAxisExist(deviceID, HID_USAGES.HID_USAGE_RY)) _
              Or Not (joystick.GetVJDAxisExist(deviceID, HID_USAGES.HID_USAGE_RZ)) Or Not (joystick.GetVJDContPovNumber(deviceID) = 1)) Then
            returnValue = False
        End If

        Return returnValue
    End Function

    Public Function getConnected()
        Return Me.isConnected
    End Function

    Public Function initErrorStr(errorNum As Integer) As String
        Dim errorStr As String

        Select Case errorNum
            Case 0
                errorStr = "No error detected."
                Exit Select
            Case -2
                errorStr = "vJoy is not installed or disabled."
                Exit Select
            Case -3
                errorStr = String.Format("vJoy device {0} is already owned by another feeder.", deviceID)
                Exit Select
            Case -4
                errorStr = String.Format("vJoy device {0} is not installed or disabled.", deviceID)
                Exit Select
            Case -5
                errorStr = String.Format("vJoy device {0} could not be acquired.", deviceID)
                Exit Select
            Case -6
                errorStr = String.Format("vJoy device {0} is not configured as a valid controller for this application.", deviceID)
                Exit Select
            Case -7
                errorStr = "No vJoy devices are available."
            Case Else
                errorStr = String.Format("vJoy device {0} general error.", deviceID)
        End Select

        Return errorStr
    End Function

    Public Function initVJoy(ID As Integer) As Integer
        Dim statusCode As Integer = 0
        deviceID = ID

        If Not (joystick.vJoyEnabled()) Then
            statusCode = -2
        Else
            Select Case joystick.GetVJDStatus(deviceID)
                Case VjdStat.VJD_STAT_OWN
                    Debug.WriteLine(String.Format("vJoy device {0} is already owned by this feeder", deviceID))
                    Exit Select
                Case VjdStat.VJD_STAT_FREE
                    Debug.WriteLine(String.Format("vJoy device {0} is free", deviceID))
                    Exit Select
                Case VjdStat.VJD_STAT_BUSY
                    Debug.WriteLine(String.Format("vJoy device {0} is already owned by another feeder" & vbNewLine & "Cannot continue", deviceID))
                    statusCode = -3
                    Exit Select
                Case VjdStat.VJD_STAT_MISS
                    Debug.WriteLine(String.Format("vJoy device {0} is not installed or disabled" & vbNewLine & "Cannot continue", deviceID))
                    statusCode = -4
                    Exit Select
                Case Else
                    Debug.WriteLine(String.Format("vJoy device {0} general error" & vbNewLine & "Cannot continue", deviceID))
                    statusCode = -1
            End Select

            If statusCode = 0 Then
                If Not (Me.isValidController()) Then
                    statusCode = -6
                ElseIf Not (joystick.AcquireVJD(deviceID)) Then
                    statusCode = -5
                End If
            End If
        End If

        If statusCode = 0 Then
            isConnected = True
        End If

        Return statusCode
    End Function

    Public Function findNextAvailableID() As Integer
        Dim foundID As Integer = -1
        Dim freeIDFound As Boolean = False, maxIDReached As Boolean = False

        If joystick.vJoyEnabled() Then
            Dim currentID As Integer = 1

            Do While Not (freeIDFound Or maxIDReached)
                If joystick.GetVJDStatus(currentID) = VjdStat.VJD_STAT_FREE Then
                    freeIDFound = True
                    foundID = currentID
                ElseIf currentID = vJoyMaxID Then
                    maxIDReached = True
                End If

                currentID += 1
            Loop
        End If

        Return foundID
    End Function

    Public Function findNextUnusedID() As Integer
        Dim foundID As Integer = -1
        Dim freeIDFound As Boolean = False, maxIDReached As Boolean = False

        If joystick.vJoyEnabled() Then
            Dim currentID As Integer = 1

            Do While Not (freeIDFound Or maxIDReached)
                If joystick.GetVJDStatus(currentID) = VjdStat.VJD_STAT_MISS Then
                    freeIDFound = True
                    foundID = currentID
                ElseIf currentID = vJoyMaxID Then
                    maxIDReached = True
                End If

                currentID += 1
            Loop
        End If

        Return foundID
    End Function

    Public Sub UpdateJoystickData(newControllerData As Form1.controllerData, oldControllerData As Form1.controllerData, sendAll As Boolean)
        Dim POVUpdateFailed As Boolean = False, UpdateFailed As Boolean = False

        If sendAll Or Not (newControllerData.joystick1Position = oldControllerData.joystick1Position) Then
            joystickData.AxisX = Math.Round(327.67 * newControllerData.joystick1Position.X)
            joystickData.AxisY = Math.Round(327.67 * newControllerData.joystick1Position.Y)
        End If

        If sendAll Or Not (newControllerData.joystick2Position = oldControllerData.joystick2Position) Then
            joystickData.AxisYRot = Math.Round(327.67 * newControllerData.joystick2Position.X)
            joystickData.AxisZRot = Math.Round(327.67 * newControllerData.joystick2Position.Y)
        End If

        If sendAll Or Not (newControllerData.trigger1Position = oldControllerData.trigger1Position) Then
            joystickData.AxisZ = Math.Round(327.67 * (50.0 + newControllerData.trigger1Position / 2))
        End If

        If sendAll Or Not (newControllerData.trigger2Position = oldControllerData.trigger2Position) Then
            joystickData.AxisXRot = Math.Round(327.67 * (50.0 + newControllerData.trigger2Position / 2))
        End If

        If sendAll Or Not (newControllerData.buttons = oldControllerData.buttons) Then
            joystickData.Buttons = newControllerData.buttons
        End If

        If sendAll Or Not (newControllerData.POV = oldControllerData.POV) Then
            If (newControllerData.POV = -1) Then
                'POVUpdateFailed = Not (joystick.ResetPovs(deviceID)) 'Not (joystick.SetContPov(-1, deviceID, 1))
                joystickData.bHats = UInteger.MaxValue
            Else
                joystickData.bHats = 100 * newControllerData.POV
            End If
        End If

        UpdateFailed = Not (joystick.UpdateVJD(deviceID, joystickData))

        If POVUpdateFailed Or UpdateFailed Then
            Me.releaseVJoy()

            If (Me.initVJoy(deviceID) = 0) Then
                If (newControllerData.POV = -1) Then
                    joystickData.bHats = UInteger.MaxValue
                End If

                UpdateFailed = Not (joystick.UpdateVJD(deviceID, joystickData))

                If POVUpdateFailed Or UpdateFailed Then
                    isConnected = False
                Else
                    isConnected = True
                End If
            End If
        End If
    End Sub

    Public Sub releaseVJoy()
        joystick.RelinquishVJD(deviceID)
        isConnected = False
    End Sub
End Class
