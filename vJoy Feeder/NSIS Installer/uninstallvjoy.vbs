Set objShell = CreateObject("WScript.Shell")

Set fso = CreateObject("Scripting.FileSystemObject")

dim filePath
If fso.FolderExists(objShell.ExpandEnvironmentStrings("%windir%") & "\sysnative") Then
	'filePath = """" & objShell.ExpandEnvironmentStrings("%ProgramW6432%") & "\vJoy\unins000.exe"" /SILENT"
	filePath = """%ProgramW6432%\vJoy\unins000.exe"" /SILENT"
	'MsgBox "WOW64 " & filePath
Else
	filePath = """%ProgramFiles%\vJoy\unins000.exe"" /SILENT"
End If

exitCode = objShell.Run(filePath, 1, TRUE)

Set objShell = Nothing
WScript.Quit(exitCode)