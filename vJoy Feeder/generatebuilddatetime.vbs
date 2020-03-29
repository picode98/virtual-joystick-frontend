Function leftPad(stringIn, finalLength, padChar)
	dim numChars
	numChars = 0
	
	If finalLength > Len(stringIn) Then
		numChars = finalLength - Len(stringIn)
	End If
	
	leftPad = String(numChars, padChar) & stringIn
End Function

set FSO = WScript.CreateObject("Scripting.FileSystemObject")

WScript.StdOut.Write "Writing build date and time to """ & WScript.Arguments.Item(0) & "builddatetime.txt"" ..."

set newFile = FSO.CreateTextFile(WScript.Arguments.Item(0) & "builddatetime.txt", True)

dim AMPMString, hourString, currentDateTime, currentDateTimeStr
currentDateTime = Now

If Hour(Time) >= 12 Then
	AMPMString = "P.M."
Else	
	AMPMString = "A.M."
End If

'WScript.Echo Hour(Time)

If Hour(Time) >= 13 Then
	hourString = CStr(Hour(currentDateTime) - 12)
Else
	hourString = CStr(Hour(currentDateTime))
End If

currentDateTimeStr = CStr(Month(currentDateTime)) & "/" & CStr(Day(currentDateTime)) & "/" & CStr(Year(currentDateTime)) & " at " & hourString & ":" & leftPad(CStr(Minute(currentDateTime)), 2, "0") & " " & AMPMString 'FormatDateTime(currentDateTime, 4)

newFile.WriteLine currentDateTimeStr

newFile.Close
set FSO = Nothing