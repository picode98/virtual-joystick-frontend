@echo off
echo Checking privileges...
net session 1>nul 2>nul

IF %errorlevel% GTR 0 goto insufficientPrivileges

echo Checking OS bitness...
reg Query "HKLM\Hardware\Description\System\CentralProcessor\0" | find /i "x86" > NUL && set OSbitness=32BIT || set OSbitness=64BIT
echo OS bitness: %OSbitness%

set previousDirectory=%cd%

IF %OSbitness%==32BIT (
cd %ProgramFiles%\vJoy\x86
) ELSE (
cd %ProgramFiles%\vJoy\x64
)

echo Configuring vJoy for two-joysticks configuration...

vJoyConfig.exe 1 -f -a x y z -b 11
vJoyConfig.exe 2 -f -a x y z -b 11

cd %previousDirectory%

echo Configuration complete.
exit /b

:insufficientPrivileges
echo ERROR: Administrative privileges are required to configure vJoy.
echo        This batch file will attempt to restart with these privileges.
pause
powershell -Command "Start-Process Two_Logitech_Attack_3_Joysticks.bat -Verb runAs"