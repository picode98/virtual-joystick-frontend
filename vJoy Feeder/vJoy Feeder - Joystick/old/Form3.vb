Public Class Form3
    Public OKPressed As Boolean = False

    Private Sub OKButton_Click(sender As System.Object, e As System.EventArgs) Handles OKButton.Click
        OKPressed = True
        Me.Close()
    End Sub

    Private Sub Form3_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        OKPressed = False
        Me.Show()
        DeviceIDInput.Focus()
    End Sub

    Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click
        Me.Close()
    End Sub
End Class