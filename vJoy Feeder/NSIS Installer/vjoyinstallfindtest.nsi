Unicode True

RequestExecutionLevel user

!include "StrTok.nsh"
!include "StrLoc.nsh"
!include "x64.nsh"

Function getVJoyInstallKey
    Var /GLOBAL regCmdStr
    Var /GLOBAL regCmdOutput
    Var /GLOBAL regResultCode
    
    StrCpy $regCmdStr "reg.exe QUERY HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall /s /f $\"vJoy Device Driver$\""
    DetailPrint "Start: Executing $regCmdStr"

    ${If} ${IsWow64}
        StrCpy $regCmdStr "$regCmdStr /reg:64"
    ${EndIf}

    DetailPrint "Executing $regCmdStr"

    nsExec::ExecToStack $regCmdStr

    Pop $regResultCode
    Pop $regCmdOutput

    ${StrTok} $0 $regCmdOutput "$\r$\n" "0" "1"

    ${StrLoc} $2 $0 "\" ">"
    IntOp $2 $2 + 1

    StrCpy $0 $0 "" $2

    Push $0
    Push $regResultCode
FunctionEnd

Section "Test" Test
    Call getVJoyInstallKey
    Pop $1
    Pop $0
    DetailPrint "vJoy install key: $0"
SectionEnd