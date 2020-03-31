; Build Unicode installer
Unicode True

!include "WordFunc.nsh"
!include "FileFunc.nsh"
!include "x64.nsh"

!include "product_info.nsh"
!include "vJoy_utils.nsh"

; If build parameters must be passed to the uninstaller in the future, add
; /DBUILD_PARAM_NAME=${BUILD_PARAM_NAME} *at the beginning*
!makensis "windows_uninstaller.nsi"

; The name of the installer
Name "Virtual Devices Installer"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

InstallDir "$PROGRAMFILES32\Virtual Devices"

Var /GLOBAL restartRequired
!macro setRestartReq
    StrCpy $restartRequired "true"
!macroend

!define DOT_NET_VERSION_4_5 378389

!define INNO_SETUP_INSTALL_SUCCESS 0
!define INNO_SETUP_INSTALL_INIT_FAILED 1
!define INNO_SETUP_INSTALL_START_CANCELED 2
!define INNO_SETUP_INSTALL_TR_FATAL 3
!define INNO_SETUP_INSTALL_FATAL 4
!define INNO_SETUP_INSTALL_EXEC_CANCELED 5
!define INNO_SETUP_INSTALL_TERMINATED 6
!define INNO_SETUP_INSTALL_PREP_FAILED 7
!define INNO_SETUP_INSTALL_PREP_FAILED_RESTART 8

!getdllversion "..\vJoy Feeder\bin\${BUILD_CONFIG}\Virtual X-Box 360 Controller.exe" INSTALL_VER_
!define INSTALL_VER_STRING "${INSTALL_VER_1}.${INSTALL_VER_2}.${INSTALL_VER_3}.${INSTALL_VER_4}"

!getdllversion "vJoySetup.exe" VJOY_VER_
!define VJOY_VER_STRING "${VJOY_VER_1}.${VJOY_VER_2}.${VJOY_VER_3}.${VJOY_VER_4}"

Var /GLOBAL startMenuDir

; Function "isVJoyInstalled"
;     Var /GLOBAL thisKey
;     Var /GLOBAL index
;     StrCpy $index 0

;     ${StrLoc} R0 R1 "vJoy Device Driver" ">"

;     EnumRegKey $thisKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall" $index
;     ReadRegStr $thisName HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$thisKey" "DisplayName"
; FunctionEnd

Page directory
Page instfiles

Section "Virtual Devices" VDevices
    SetShellVarContext all

    StrCpy $startMenuDir "$SMPROGRAMS\${PRODUCT_NAME}"

    SetOutPath $TEMP

    Var /GLOBAL installAbortString

    Var /GLOBAL dotNet4Version
    ReadRegDWORD $dotNet4Version HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" Release

    IfErrors DotNetInstallReq +1
    IntCmp $dotNet4Version ${DOT_NET_VERSION_4_5} vJoyInstallCheck DotNetInstallReq vJoyInstallCheck

    DotNetInstallReq:
         StrCpy $installAbortString "Microsoft .NET Framework v4.5 or newer is required to install this program."
         Goto InstallAbort

    vJoyInstallCheck:
        Call getVJoyInstallKey
        Pop $1 ; Result code
        Pop $0 ; vJoy install subkey

        IntCmp $1 0 +1 vJoyNotFound vJoyNotFound

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
        Var /GLOBAL vJoyInstallResult
        File "vJoySetup.exe"
        ExecWait "vJoySetup.exe /SP- /SILENT /NORESTART /RESTARTEXITCODE=200" $vJoyInstallResult
		
		IntCmp $vJoyInstallResult 200 vJoyRestartReq +1 +1
		IntCmp $vJoyInstallResult ${INNO_SETUP_INSTALL_SUCCESS} mainInstall vJoyInstallErr vJoyInstallErr
		
	vJoyRestartReq:
		!insertmacro setRestartReq
		Goto mainInstall
		
	vJoyInstallErr:
		Abort "vJoy install failed with exit code $vJoyInstallResult. See http://jrsoftware.org/is6help/topic_setupexitcodes.htm for more information."
		
	mainInstall:
        SetOverwrite ifdiff
        SetOutPath $INSTDIR

        File /oname=uninstall.exe "windows_uninstaller.exe"
        File /oname=vJoyInterface.dll "..\Common Files\vJoyInterface\${BUILD_PLATFORM}\vJoyInterface.dll"
        File /oname=vJoyInterfaceWrap.dll "..\Common Files\vJoyInterface\${BUILD_PLATFORM}\vJoyInterfaceWrap.dll"
        File "/oname=Virtual X-Box 360 Controller.exe" "..\vJoy Feeder\bin\${BUILD_CONFIG}\Virtual X-Box 360 Controller.exe"
        
        File "/oname=Virtual Logitech Attack 3.exe" "..\vJoy Feeder - Joystick\bin\${BUILD_CONFIG}\Virtual Logitech Attack 3.exe"
        File "/oname=Microsoft.VisualBasic.PowerPacks.dll" "..\vJoy Feeder\bin\${BUILD_CONFIG}\Microsoft.VisualBasic.PowerPacks.dll"
        File /oname=builddatetime.txt "..\vJoy Feeder\bin\${BUILD_CONFIG}\builddatetime.txt"
        File /oname=One_X-Box_Controller.bat "..\vJoy Feeder\One_X-Box_Controller.bat"
        File /oname=Two_Logitech_Attack_3_Joysticks.bat "..\vJoy Feeder - Joystick\Two_Logitech_Attack_3_Joysticks.bat"

        CreateDirectory "$startMenuDir"
        CreateShortCut "$startMenuDir\Virtual X-Box 360 Controller.lnk" "$INSTDIR\Virtual X-Box 360 Controller.exe"
        CreateShortCut "$startMenuDir\Virtual Logitech Attack 3.lnk" "$INSTDIR\Virtual Logitech Attack 3.exe"
        CreateShortCut "$startMenuDir\One X-Box Controller.lnk" "$INSTDIR\One_X-Box_Controller.bat"
        CreateShortCut "$startMenuDir\Two Logitech Attack 3 Joysticks.lnk" "$INSTDIR\Two_Logitech_Attack_3_Joysticks.bat"

        Goto postInstall

    postInstall:
        WriteRegStr HKLM "${INSTALL_KEY}" "DisplayName" "${PRODUCT_NAME}"
        WriteRegStr HKLM "${INSTALL_KEY}" "UninstallString" "$\"$INSTDIR\uninstall.exe$\""
        WriteRegStr HKLM "${INSTALL_KEY}" "InstallLocation" "$INSTDIR"
        WriteRegStr HKLM "${INSTALL_KEY}" "Publisher" "Saaman Khalilollahi"
        WriteRegStr HKLM "${INSTALL_KEY}" "DisplayVersion" "${INSTALL_VER_STRING}"
        WriteRegDWORD HKLM "${INSTALL_KEY}" "VersionMajor" "${INSTALL_VER_1}"
        WriteRegDWORD HKLM "${INSTALL_KEY}" "VersionMinor" "${INSTALL_VER_2}"
        WriteRegDWORD HKLM "${INSTALL_KEY}" "NoModify" 1
        WriteRegDWORD HKLM "${INSTALL_KEY}" "NoRepair" 1

        StrCmp $restartRequired "true" +1 endOfSection
        MessageBox MB_YESNO "A restart is required to complete this installation. Would you like to restart now?" IDYES rebootNow IDNO endOfSection

    InstallAbort:
        Abort $installAbortString

    rebootNow:
        Reboot

    endOfSection:
SectionEnd