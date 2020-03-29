Imports Forms = System.Windows.Forms

Public Class Joystick
    Public Event UpdateAxes()
    Public Event UpdateButton()

    <System.ComponentModel.Category("Misc"), System.ComponentModel.Description("Specifies whether or not to enable the use of a button on the joystick that can be used with the middle mouse button.")>
    Public Property EnableButton As Boolean = False

    Public JoystickMoving As Boolean = False
    Public ButtonHold As Boolean = False
    Public ButtonPressed As Boolean = False
    Public JoystickPosition As Point = New Point()

    Private Function coerceToRange(minimum As Long, maximum As Long, value As Long)
        Dim returnValue As Long

        If value < minimum Then
            returnValue = minimum
        ElseIf value > maximum Then
            returnValue = maximum
        Else
            returnValue = value
        End If

        Return returnValue
    End Function

    Private Function positionOnForm() As Point
        Dim pnt As Point
        pnt = Me.PointToScreen(New Point(0, 0))
        pnt = Me.ParentForm.PointToClient(pnt)

        Return pnt
    End Function

    Private Sub setJoystickIndicatorPosition(joystickPosition As Point)
        JoystickIndicator.Location = JoystickOutline.Location + joystickPosition - (New Point(JoystickIndicator.Size.Width / 2, JoystickIndicator.Size.Height / 2))
    End Sub

    Private Sub Joystick_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        JoystickIndicator.SendToBack()
        setJoystickIndicatorPosition(New Point(JoystickOutline.Size.Width / 2, JoystickOutline.Size.Height / 2))
        JoystickPosition = New Point(Me.JoystickOutline.Size.Width / 2, Me.JoystickOutline.Size.Height / 2)
        'Me.Controls.Remove(JoystickIndicator.Parent())
        'Me.ParentForm.Controls.Add(JoystickIndicator.Parent())
    End Sub

    Private Sub JoystickOutline_MouseDown(sender As System.Object, e As Forms.MouseEventArgs) Handles JoystickOutline.MouseDown
        If e.Button = Forms.MouseButtons.Left Then
            setJoystickIndicatorPosition(New Point(e.X, e.Y))
            Me.JoystickPosition = New Point(e.X, e.Y)

            RaiseEvent UpdateAxes()
            'currentControllerData.joystick1Position = New Point(e.X, e.Y)
            'If clientConnected Then
            '    PipeOperation("SendData")
            'End If

            JoystickMoving = True
        ElseIf e.Button = Forms.MouseButtons.Middle And EnableButton Then
            ButtonHold = False
            ButtonPressed = True

            RaiseEvent UpdateButton()
            'buttonHolds(8) = False

            'currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, True)

            'If clientConnected Then
            '    PipeOperation("SendData")
            'End If

            Dim joystickControl As Microsoft.VisualBasic.PowerPacks.RectangleShape = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape)
            joystickControl.FillColor = Color.DarkGray
            joystickControl.FillStyle = PowerPacks.FillStyle.SmallCheckerBoard
        End If
    End Sub

    Private Sub JoystickOutline_MouseMove(sender As System.Object, e As Forms.MouseEventArgs) Handles JoystickOutline.MouseMove
        If JoystickMoving Then
            setJoystickIndicatorPosition(New Point(e.X, e.Y) - JoystickOutline.Location)
            Me.JoystickPosition = New Point(e.X, e.Y) - JoystickOutline.Location

            RaiseEvent UpdateAxes()
            'currentControllerData.joystick1Position = New Point(e.X, e.Y)
            'If clientConnected Then
            '    PipeOperation("SendData")
            'End If
        End If
    End Sub

    Private Sub JoystickOutline_MouseUp(sender As System.Object, e As Forms.MouseEventArgs) Handles JoystickOutline.MouseUp
        If JoystickMoving Then
            If e.Button = Forms.MouseButtons.Left Then
                JoystickPosition = New Point(Me.JoystickOutline.Size.Width / 2, Me.JoystickOutline.Size.Height / 2)
                setJoystickIndicatorPosition(JoystickPosition)

                RaiseEvent UpdateAxes()
                'If clientConnected Then
                '    PipeOperation("SendData")
                'End If

                JoystickMoving = False
            End If
        End If

        If e.Button = Forms.MouseButtons.Right Then
            If Not ButtonHold Then
                If ButtonPressed Then
                    ButtonHold = True
                End If
            Else
                If EnableButton Then
                    Dim joystickControl As Microsoft.VisualBasic.PowerPacks.RectangleShape = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape)
                    joystickControl.FillStyle = PowerPacks.FillStyle.Transparent

                    ButtonHold = False
                    ButtonPressed = False

                    RaiseEvent UpdateButton()
                    'currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, False)
                End If
            End If

            If Not (JoystickMoving) And Not (ButtonPressed) Then
                JoystickPosition = New Point(Me.JoystickOutline.Size.Width / 2, Me.JoystickOutline.Size.Height / 2)
                setJoystickIndicatorPosition(JoystickPosition)
            End If

            RaiseEvent UpdateAxes()
            'If clientConnected Then
            '    PipeOperation("SendData")
            'End If

            JoystickMoving = False
        End If

        If e.Button = Forms.MouseButtons.Middle And Not (ButtonHold) And EnableButton Then
            ButtonPressed = False
            RaiseEvent UpdateButton()
            'currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, False)

            'If clientConnected Then
            '    PipeOperation("SendData")
            'End If

            Dim joystickControl As Microsoft.VisualBasic.PowerPacks.RectangleShape = CType(sender, Microsoft.VisualBasic.PowerPacks.RectangleShape)
            joystickControl.FillStyle = PowerPacks.FillStyle.Transparent
        End If
    End Sub

    Public Sub parentFormMouseDown(sender As Object, e As Forms.MouseEventArgs)
        If JoystickMoving Then
            If e.Button = Forms.MouseButtons.Middle And EnableButton Then
                ButtonPressed = True

                RaiseEvent UpdateButton()
                'currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, True)

                'If clientConnected Then
                '    PipeOperation("SendData")
                'End If

                JoystickOutline.FillColor = Color.DarkGray
                JoystickOutline.FillStyle = PowerPacks.FillStyle.SmallCheckerBoard
            End If
        End If
    End Sub

    Public Sub parentFormMouseMove(sender As Object, e As Forms.MouseEventArgs)
        If JoystickMoving Then
            Dim formPosition As Point = Me.positionOnForm()
            Dim outlineRectOnForm As Rectangle = JoystickOutline.Bounds
            outlineRectOnForm.Offset(formPosition)

            If outlineRectOnForm.Contains(e.Location) Then
                JoystickOutline.GetContainerControl().ActiveControl.Capture = True
                Me.ParentForm.Capture = False
            Else
                Dim newJoystickPosition As Point = New Point(coerceToRange(0, JoystickOutline.Width, e.X - formPosition.X), coerceToRange(0, JoystickOutline.Height, e.Y - formPosition.Y))
                setJoystickIndicatorPosition(newJoystickPosition)
                JoystickPosition = newJoystickPosition

                RaiseEvent UpdateAxes()

                'currentControllerData.joystick1Position = newJoystickPosition
                'If clientConnected Then
                '    PipeOperation("SendData")
                'End If
            End If
        End If
    End Sub

    Public Sub parentFormMouseUp(sender As Object, e As Forms.MouseEventArgs)
        If JoystickMoving Or ButtonPressed Then
            If e.Button = Forms.MouseButtons.Left And JoystickMoving Then
                JoystickPosition = New Point(Me.JoystickOutline.Size.Width / 2, Me.JoystickOutline.Size.Height / 2)
                setJoystickIndicatorPosition(JoystickPosition)

                RaiseEvent UpdateAxes()
                'If clientConnected Then
                '    PipeOperation("SendData")
                'End If

                JoystickMoving = False
            ElseIf e.Button = Forms.MouseButtons.Middle And Not (ButtonHold) Then
                If EnableButton Then
                    ButtonPressed = False

                    RaiseEvent UpdateButton()
                    'currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, False)

                    'If clientConnected Then
                    '    PipeOperation("SendData")
                    'End If

                    JoystickOutline.FillStyle = PowerPacks.FillStyle.Transparent
                End If
            ElseIf e.Button = Forms.MouseButtons.Right Then
                If Not (ButtonHold) Then
                    If ButtonPressed Then
                        ButtonHold = True
                    End If
                Else
                    If EnableButton Then
                        JoystickOutline.FillStyle = PowerPacks.FillStyle.Transparent

                        ButtonHold = False
                        ButtonPressed = False

                        RaiseEvent UpdateButton()
                        'currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, False)
                    End If
                End If

                If Not (JoystickMoving) And Not (ButtonPressed) Then
                    JoystickPosition = New Point(Me.JoystickOutline.Size.Width / 2, Me.JoystickOutline.Size.Height / 2)
                    setJoystickIndicatorPosition(JoystickPosition)

                    RaiseEvent UpdateAxes()
                End If

                'If clientConnected Then
                '    PipeOperation("SendData")
                'End If

                JoystickMoving = False
            End If
        End If
    End Sub

    'Public Sub parentFormMouseDown(sender As Object, e As Forms.MouseEventArgs)
    '    If JoystickMoving Then
    '        If e.Button = Forms.MouseButtons.Middle Then
    '            ButtonPressed = True

    '            RaiseEvent UpdateButton()
    '            'currentControllerData.buttons = SetButton(currentControllerData.buttons, 8, True)

    '            'If clientConnected Then
    '            '    PipeOperation("SendData")
    '            'End If

    '            JoystickOutline.FillColor = Color.DarkGray
    '            JoystickOutline.FillStyle = PowerPacks.FillStyle.SmallCheckerBoard
    '        End If
    '    End If
    'End Sub

    Private Sub JoystickOutline_MouseLeave(sender As System.Object, e As System.EventArgs) Handles JoystickOutline.MouseLeave
        If JoystickMoving Or (ButtonPressed And Not ButtonHold) Then
            Me.ParentForm.Capture = True
        End If
    End Sub

    Private Sub Joystick_Resize(sender As System.Object, e As System.EventArgs) Handles MyBase.Resize
        JoystickOutline.Size = Me.Size - New Size(1, 1) ' - JoystickIndicator.Size
        'JoystickOutline.Location = New Point(JoystickIndicator.Size.Width / 2, JoystickIndicator.Size.Height / 2)

        JoystickPosition = New Point(Me.JoystickOutline.Size.Width / 2, Me.JoystickOutline.Size.Height / 2)
        setJoystickIndicatorPosition(JoystickPosition)
    End Sub

    'Private Sub JoystickOutline_MouseDown_1(sender As System.Object, e As Forms.MouseEventArgs) Handles JoystickOutline.MouseDown
    '    Me.Capture = True
    '    JoystickOutline_MouseDown(Me, e)
    'End Sub
End Class
