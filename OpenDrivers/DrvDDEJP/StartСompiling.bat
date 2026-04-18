@ECHO OFF
SETLOCAL
CD /D "%~dp0"

ECHO ===================================================
ECHO DrvDDEJP Build Script (Any CPU)
ECHO ===================================================

:: Check for Administrative Privileges
NET SESSION >NUL 2>&1
IF %ERRORLEVEL% NEQ 0 (
    ECHO [ERROR] This script must be run as ADMINISTRATOR.
    ECHO Please right-click the BAT file and select "Run as administrator".
    PAUSE
    EXIT /B 1
)

:: Close ScadaAdmin
ECHO TASKKILL ScadaAdmin...
TASKKILL /IM ScadaAdmin.exe /F 2>NUL

:: Build DDE Support Library
ECHO RESTORING PROJECTS...
dotnet restore ".\DrvDDEJP.sln"

ECHO BUILDING DDE LIBRARY...
dotnet build ".\DrvDDEJP.DDE\DrvDDEJP.DDE.csproj" -c Release

:: Build Logic (Communicator Driver)
ECHO BUILDING LOGIC (ANY CPU)...
dotnet publish ".\DrvDDEJP.Logic\DrvDDEJP.Logic.csproj" -c Release --self-contained false --output ".\Release\anycpu\SCADA\ScadaComm\Drv"
DEL ".\Release\anycpu\SCADA\ScadaComm\Drv\Scada*.dll" 2>NUL
DEL ".\Release\anycpu\SCADA\ScadaComm\Drv\*.pdb" 2>NUL
DEL ".\Release\anycpu\SCADA\ScadaComm\Drv\*.json" 2>NUL

:: Build View (Admin Library)
ECHO BUILDING VIEW (ANY CPU)...
dotnet publish ".\DrvDDEJP.View\DrvDDEJP.View.csproj" -c Release --self-contained false --output ".\Release\anycpu\SCADA\ScadaAdmin\Lib"
DEL ".\Release\anycpu\SCADA\ScadaAdmin\Lib\Scada*.dll" 2>NUL
DEL ".\Release\anycpu\SCADA\ScadaAdmin\Lib\*.pdb" 2>NUL
DEL ".\Release\anycpu\SCADA\ScadaAdmin\Lib\*.json" 2>NUL

:: Move Language files if exist
IF EXIST ".\Release\anycpu\SCADA\ScadaAdmin\Lib\Lang" (
    ECHO MOVING LANG FILES...
    IF NOT EXIST ".\Release\anycpu\SCADA\ScadaAdmin\Lang" MKDIR ".\Release\anycpu\SCADA\ScadaAdmin\Lang"
    MOVE /Y ".\Release\anycpu\SCADA\ScadaAdmin\Lib\Lang\*.*" ".\Release\anycpu\SCADA\ScadaAdmin\Lang\"
)

:: Stopping Services
ECHO STOPPING SERVICES...
NET STOP ScadaAgent6 2>NUL
TASKKILL /IM ScadaAgentWkr.exe /F 2>NUL
NET STOP ScadaComm6 2>NUL
TASKKILL /IM ScadaCommWkr.exe /F 2>NUL
NET STOP ScadaServer6 2>NUL
TASKKILL /IM ScadaServerWkr.exe /F 2>NUL

:: Copying Files to System Folder
ECHO DEPLOYING TO SYSTEM FOLDER...
IF EXIST ".\Release\anycpu\SCADA" (
    XCOPY ".\Release\anycpu\SCADA\*.*" "C:\Program Files\SCADA\" /S /Y /I
)

:: Starting Services
ECHO STARTING SERVICES...
NET START ScadaAgent6 2>NUL
NET START ScadaComm6 2>NUL
NET START ScadaServer6 2>NUL

:: Reopening ScadaAdmin
ECHO STARTING APP...
START "" "C:\Program Files\SCADA\ScadaAdmin\ScadaAdmin.exe"

ECHO ===================================================
ECHO Build and Deployment Finished.
ECHO ===================================================
PAUSE