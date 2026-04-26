@echo off
setlocal EnableExtensions

REM ============================================================
REM Build and deploy MimCalendarJP plugin to installed SCADA
REM ============================================================

net session >nul 2>&1
if not "%errorlevel%"=="0" (
    echo [ERROR] This script must be run as Administrator.
    echo [ERROR] Right-click the BAT file and choose "Run as administrator".
    exit /b 1
)

set "SCRIPT_DIR=%~dp0"
set "SCADA_ROOT=C:\Program Files\SCADA"
set "WEB_DST=%SCADA_ROOT%\ScadaWeb"
set "ADMIN_LIB_DST=%SCADA_ROOT%\ScadaAdmin\Lib"

set "LIB_DIR=%SCRIPT_DIR%Libraries"
set "PRJ_WEB=%SCRIPT_DIR%PlgMimCalendarJP\PlgMimCalendarJP.csproj"
set "PRJ_VIEW=%SCRIPT_DIR%PlgMimCalendarJP.View\PlgMimCalendarJP.View.csproj"
set "OUT_WEB=%SCRIPT_DIR%PlgMimCalendarJP\bin\Release\net8.0"
set "OUT_VIEW=%SCRIPT_DIR%PlgMimCalendarJP.View\bin\Release\net8.0"
set "SRC_LANG=%SCRIPT_DIR%PlgMimCalendarJP\lang"
set "SRC_PLUGIN_WWW=%SCRIPT_DIR%PlgMimCalendarJP\wwwroot\plugins\MimCalendarJP"

set "DST_LANG=%WEB_DST%\lang"
set "DST_PLUGIN_WWW=%WEB_DST%\wwwroot\plugins\MimCalendarJP"
set "DST_RU=%DST_LANG%\PlgMimCalendarJP.ru-RU.xml"

if not exist "%WEB_DST%" (
    echo [ERROR] Destination folder not found: %WEB_DST%
    exit /b 1
)
if not exist "%ADMIN_LIB_DST%" (
    echo [ERROR] Destination folder not found: %ADMIN_LIB_DST%
    exit /b 1
)
if not exist "%LIB_DIR%" (
    echo [ERROR] Libraries folder not found: %LIB_DIR%
    exit /b 1
)

set "DOTNET_CLI_HOME=%SCRIPT_DIR%.dotnet"
set "NUGET_PACKAGES=%SCRIPT_DIR%.nuget\packages"
set "DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1"
set "DOTNET_CLI_TELEMETRY_OPTOUT=1"
if not exist "%DOTNET_CLI_HOME%" mkdir "%DOTNET_CLI_HOME%"
if not exist "%NUGET_PACKAGES%" mkdir "%NUGET_PACKAGES%"

echo [1/7] Building PlgMimCalendarJP...
dotnet build "%PRJ_WEB%" -c Release -v minimal || goto :build_error

echo [2/7] Building PlgMimCalendarJP.View...
dotnet build "%PRJ_VIEW%" -c Release -v minimal || goto :build_error

echo [3/7] Stopping ScadaWeb service...
set "WEB_STOP=%WEB_DST%\svc_stop.bat"
if not exist "%WEB_STOP%" (
    echo [ERROR] Stop script not found: %WEB_STOP%
    exit /b 1
)
call "%WEB_STOP%"

echo [4/7] Deploying compiled plugin binaries...
copy /Y "%OUT_WEB%\PlgMimCalendarJP.dll" "%WEB_DST%\PlgMimCalendarJP.dll" >nul || goto :copy_error
copy /Y "%OUT_VIEW%\PlgMimCalendarJP.View.dll" "%ADMIN_LIB_DST%\PlgMimCalendarJP.View.dll" >nul || goto :copy_error

echo [5/7] Deploying language and web resources...
copy /Y "%SRC_LANG%\PlgMimCalendarJP.en-GB.xml" "%DST_LANG%\PlgMimCalendarJP.en-GB.xml" >nul || goto :copy_error
copy /Y "%SRC_LANG%\PlgMimCalendarJP.ru-RU.xml" "%DST_LANG%\PlgMimCalendarJP.ru-RU.xml" >nul || goto :copy_error
if not exist "%DST_PLUGIN_WWW%" mkdir "%DST_PLUGIN_WWW%"
robocopy "%SRC_PLUGIN_WWW%" "%DST_PLUGIN_WWW%" /E /R:1 /W:1 /NFL /NDL /NJH /NJS >nul
if errorlevel 8 goto :copy_error

echo [6/7] Verifying dictionary key in deployed RU language file...
if not exist "%DST_RU%" (
    echo [ERROR] Deployed language file not found: %DST_RU%
    exit /b 1
)
findstr /C:"Scada.Web.Plugins.PlgMimCalendarJP.Code.CalendarComponentGroup" "%DST_RU%" >nul
if errorlevel 1 (
    echo [ERROR] Dictionary key not found in %DST_RU%
    exit /b 1
)

echo [7/7] Starting ScadaWeb service...
set "WEB_START=%WEB_DST%\svc_start.bat"
if not exist "%WEB_START%" (
    echo [ERROR] Start script not found: %WEB_START%
    exit /b 1
)
call "%WEB_START%"
if errorlevel 1 (
    echo [ERROR] Failed to start ScadaWeb service.
    exit /b 1
)

set "APPCMD=%windir%\system32\inetsrv\appcmd.exe"
if exist "%APPCMD%" (
    "%APPCMD%" start SITE /site.name:Scada >nul 2>&1
    "%APPCMD%" start APPPOOL "ScadaAppPool" >nul 2>&1
)

sc query w3svc | findstr /I "RUNNING" >nul
if errorlevel 1 (
    echo [ERROR] W3SVC is not running after start command.
    echo [ERROR] Check IIS service, Scada site and ScadaAppPool.
    exit /b 1
)

echo [OK] MimCalendarJP plugin has been built, deployed, dictionary checked, and ScadaWeb was started.
exit /b 0

:build_error
echo [ERROR] Build failed.
exit /b 1

:copy_error
echo [ERROR] Copy/deploy failed.
exit /b 1
