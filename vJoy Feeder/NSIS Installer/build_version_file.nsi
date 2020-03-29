; Build Unicode installer
Unicode True

RequestExecutionLevel user

!include "x64.nsh"
!include "FileFunc.nsh"
!include "WordFunc.nsh"

!include "StrTok.nsh"
!include "StrLoc.nsh"

!getdllversion "vJoySetup.exe" VJOY_VER_
!define VJOY_VER_STRING "${VJOY_VER_1}.${VJOY_VER_2}.${VJOY_VER_3}.${VJOY_VER_4}"

Function getVJoyInstallKey
    Var /GLOBAL regCmdStr
    Var /GLOBAL regCmdOutput
    Var /GLOBAL regResultCode
    
    StrCpy $regCmdStr "reg.exe QUERY HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall /s /f $\"vJoy Device Driver$\""
    ; DetailPrint "Start: Executing $regCmdStr"

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

    DetailPrint "Returning subkey $0"

    Push $0
    Push $regResultCode
FunctionEnd

Section
    ; Var /GLOBAL installVersion
    ; !getdllversion "..\vJoy Feeder\bin\${BUILD_CONFIG}\Virtual X-Box 360 Controller.exe" INSTALL_VER_
    ; DetailPrint "Install version: ${INSTALL_VER_1}.${INSTALL_VER_2}.${INSTALL_VER_3}.${INSTALL_VER_4}"

    ; !getdllversion "vJoySetup.exe" VJOY_VER_
    ; ; ${GetFileVersion} "vJoySetup.exe" $vJoyVersion
    ; DetailPrint "vJoy version to install: ${VJOY_VER_1}.${VJOY_VER_2}.${VJOY_VER_3}.${VJOY_VER_4}"

    Call getVJoyInstallKey
        Pop $1 ; Result code
        Pop $0 ; vJoy install subkey

        IntCmp $1 0 +1 vJoyNotFound vJoyNotFound

        DetailPrint "Reading key $0"

        ${If} ${IsWow64}
            SetRegView 64
        ${EndIf}
        ReadRegStr $2 HKLM $0 "DisplayVersion"
        ${If} ${IsWow64}
            SetRegView Default
        ${EndIf}

        DetailPrint "Found vJoy install; version: $2"

        ${VersionCompare} "${VJOY_VER_STRING}" $2 $3
        IntCmp $3 1 vJoyInstall vJoyVersionEq vJoyVersionLess

    vJoyVersionEq:
        DetailPrint "Same version of vJoy already installed; skipping vJoy installation..."
        Goto mainInstall

    vJoyVersionLess:
        DetailPrint "WARNING: Newer version of vJoy already installed; skipping vJoy installation..."
        Goto mainInstall

    vJoyNotFound:
        DetailPrint "Could not find vJoy installation. Running installer..."
        Goto vJoyInstall

    vJoyInstall:
        DetailPrint "[vJoy installation]"

    mainInstall:
        DetailPrint "[main installation]"
        Goto endOfSection
        
    endOfSection:
SectionEnd