Imports System.IO

Public Class AboutDialog
    Private shownOnce As Boolean = False
    Const BUILD_DATE_TIME_FILE_PATH = "builddatetime.txt"

    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not (shownOnce) Then
            shownOnce = True

            ApplicationIconBox.Image = My.Resources.hcrclogo
            ApplicationVersionLabel.Text += Application.ProductVersion
            ApplicationNameLabel.Text = Application.ProductName

            Try
                Dim dateTimeFileStream As StreamReader = New StreamReader(BUILD_DATE_TIME_FILE_PATH)
                Dim buildDateTime As String = dateTimeFileStream.ReadLine()
                dateTimeFileStream.Close()
                BuildDateTimeLabel.Text += buildDateTime
            Catch ex As System.IO.FileNotFoundException
                MessageBox.Show("Could not read build date/time: File """ & Directory.GetCurrentDirectory() & "\" & BUILD_DATE_TIME_FILE_PATH & """ was not found.", "Build Date/Time Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BuildDateTimeLabel.Text += "[An error occurred.]"
            End Try
        End If

        Me.TopMost = Form1.TopMost
    End Sub

    Private Sub OKButton_Click(sender As System.Object, e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub
End Class