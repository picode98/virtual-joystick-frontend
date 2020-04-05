!include "x64.nsh"

!include "StrTok.nsh"
!include "StrLoc.nsh"

Function getVJoyInstallKey
    ; $0: reg command string
    ; $1: reg command output
    ; $2: reg result code
    ; $3: vJoy subkey
    ; $4: start location for registry hive truncation
    
    StrCpy $0 "reg.exe QUERY HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall /s /f $\"vJoy Device Driver$\""

    ${If} ${IsWow64}
        StrCpy $0 "$0 /reg:64"
    ${EndIf}

    DetailPrint "Executing $0"

    nsExec::ExecToStack $0
    Pop $2
    Pop $1

    ${StrTok} $3 $1 "$\r$\n" "0" "1"

    ${StrLoc} $4 $3 "\" ">"
    IntOp $4 $4 + 1

    StrCpy $3 $3 "" $4

    Push $3
    Push $2
FunctionEnd