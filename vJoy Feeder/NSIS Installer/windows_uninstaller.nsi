; Build Unicode installer
Unicode True

; Request application privileges for Windows Vista
RequestExecutionLevel admin

Name "Virtual Devices Uninstaller"

!include "product_info.nsh"
!include "vJoy_utils.nsh"

Var /GLOBAL startMenuDir

Section "Virtual Devices Uninstall" VDevicesUninst
    SetShellVarContext all

    StrCpy $startMenuDir "$SMPROGRAMS\${PRODUCT_NAME}"

    Var /GLOBAL errorStr

    Var /GLOBAL installLoc
    ReadRegStr $installLoc HKLM "${INSTALL_KEY}" "InstallLocation"
    SetOutPath $installLoc

    Delete /REBOOTOK "vJoyInterface.dll"
    Delete /REBOOTOK "vJoyInterfaceWrap.dll"
    Delete /REBOOTOK "Virtual X-Box 360 Controller.exe"
    Delete /REBOOTOK "Virtual Logitech Attack 3.exe"
    Delete /REBOOTOK "Microsoft.VisualBasic.PowerPacks.dll"
    Delete /REBOOTOK "builddatetime.txt"
    Delete /REBOOTOK "One_X-Box_Controller.bat"
    Delete /REBOOTOK "Two_Logitech_Attack_3_Joysticks.bat"

    SetOutPath $startMenuDir
    Delete /REBOOTOK "Virtual X-Box 360 Controller.lnk"
    Delete /REBOOTOK "Virtual Logitech Attack 3.lnk"
    Delete /REBOOTOK "One X-Box Controller.lnk"
    Delete /REBOOTOK "Two Logitech Attack 3 Joysticks.lnk"

    SetOutPath $TEMP
    RMDir $startMenuDir


    MessageBox MB_YESNO "Remove vJoy as well?" IDYES uninstallVJoy IDNO endSuccess

    uninstallVJoy:
        SetOutPath $installLoc

        Call getVJoyInstallKey
        Pop $1 ; Result code
        Pop $0 ; vJoy install subkey

        IntCmp $1 0 +1 vJoyNotFound vJoyNotFound
        
        ${If} ${IsWow64}
            SetRegView 64
        ${EndIf}
        ReadRegStr $2 HKLM $0 "QuietUninstallString"
        ${If} ${IsWow64}
            SetRegView Default
        ${EndIf}

        Var /GLOBAL uninstallResult
        ExecWait $2 $uninstallResult

        IntCmp $uninstallResult 0 endSuccess +1 +1
        StrCpy $errorStr "Error: vJoy uninstallation script exited with code $uninstallResult"
        Goto endFailure

    vJoyNotFound:
        StrCpy $errorStr "Could not find vJoy installation."
        Goto endFailure

    endSuccess:
        SetOutPath $installLoc
        DeleteRegKey HKLM "${INSTALL_KEY}"
        Goto endOfSection

    endFailure:
        MessageBox MB_ICONSTOP $errorStr
        Abort $errorStr

    endOfSection:
SectionEnd