@echo off
setlocal EnableExtensions

REM ============================================================
REM Build and deploy MimShapes plugin to installed SCADA
REM ============================================================

net session >nul 2>&1
if not "%errorlevel%"=="0" (
    echo [ERROR] This script must be run as Administrator.
    echo [ERROR] Right-click the BAT file and choose "Run as administrator".
    exit /b 1
)

set "SCRIPT_DIR=%~dp0"
set "ROOT=%SCRIPT_DIR%"

set "SCADA_ROOT=C:\Program Files\SCADA"
set "WEB_DST=%SCADA_ROOT%\ScadaWeb"
set "ADMIN_LIB_DST=%SCADA_ROOT%\ScadaAdmin\Lib"

set "PRJ_WEB=%ROOT%PlgMimShapesJP\PlgMimShapesJP.csproj"
set "PRJ_VIEW=%ROOT%PlgMimShapesJP.View\PlgMimShapesJP.View.csproj"

set "OUT_WEB=%ROOT%PlgMimShapesJP\bin\Release\net8.0"
set "OUT_VIEW=%ROOT%PlgMimShapesJP.View\bin\Release\net8.0"

set "SRC_LANG=%ROOT%PlgMimShapesJP\lang"
set "SRC_PLUGIN_WWW=%ROOT%PlgMimShapesJP\wwwroot\plugins\MimShapesJP"

set "DST_LANG=%WEB_DST%\lang"
set "DST_PLUGIN_WWW=%WEB_DST%\wwwroot\plugins\MimShapesJP"
set "DST_RU=%DST_LANG%\PlgMimShapesJP.ru-RU.xml"
set "WEB_CONFIG=%WEB_DST%\config\ScadaWebConfig.xml"
set "WEB_STOP=%WEB_DST%\svc_stop.bat"
set "WEB_START=%WEB_DST%\svc_start.bat"
set "SERVICE_STOPPED=0"

if not exist "%WEB_DST%" (
    echo [ERROR] Destination folder not found: %WEB_DST%
    exit /b 1
)
if not exist "%ADMIN_LIB_DST%" (
    echo [ERROR] Destination folder not found: %ADMIN_LIB_DST%
    exit /b 1
)
if not exist "%WEB_CONFIG%" (
    echo [ERROR] Web config not found: %WEB_CONFIG%
    exit /b 1
)
if not exist "%WEB_STOP%" (
    echo [ERROR] Stop script not found: %WEB_STOP%
    exit /b 1
)
if not exist "%WEB_START%" (
    echo [ERROR] Start script not found: %WEB_START%
    exit /b 1
)

set "DOTNET_CLI_HOME=%ROOT%.dotnet"
set "NUGET_PACKAGES=%ROOT%.nuget\packages"
set "DOTNET_CLI_TELEMETRY_OPTOUT=1"
set "DOTNET_NOLOGO=1"
if not exist "%DOTNET_CLI_HOME%" mkdir "%DOTNET_CLI_HOME%"
if not exist "%NUGET_PACKAGES%" mkdir "%NUGET_PACKAGES%"

echo [1/8] Building PlgMimShapesJP...
dotnet build "%PRJ_WEB%" -c Release -v minimal || goto :build_error

echo [2/8] Building PlgMimShapesJP.View...
dotnet build "%PRJ_VIEW%" -c Release -v minimal || goto :build_error

echo [3/8] Service scripts validated.

echo [4/8] Stopping ScadaWeb service...
call "%WEB_STOP%"
if errorlevel 1 goto :service_stop_error
set "SERVICE_STOPPED=1"

echo [5/8] Deploying binaries...
copy /Y "%OUT_WEB%\PlgMimShapesJP.dll" "%WEB_DST%\PlgMimShapesJP.dll" >nul || goto :deploy_error
copy /Y "%OUT_VIEW%\PlgMimShapesJP.View.dll" "%ADMIN_LIB_DST%\PlgMimShapesJP.View.dll" >nul || goto :deploy_error

echo [6/8] Deploying language and web resources...
copy /Y "%SRC_LANG%\PlgMimShapesJP.en-GB.xml" "%DST_LANG%\PlgMimShapesJP.en-GB.xml" >nul || goto :deploy_error
copy /Y "%SRC_LANG%\PlgMimShapesJP.ru-RU.xml" "%DST_LANG%\PlgMimShapesJP.ru-RU.xml" >nul || goto :deploy_error
if not exist "%DST_PLUGIN_WWW%" mkdir "%DST_PLUGIN_WWW%"
robocopy "%SRC_PLUGIN_WWW%" "%DST_PLUGIN_WWW%" /MIR /R:1 /W:1 /NFL /NDL /NJH /NJS >nul
if errorlevel 8 goto :deploy_error

echo [7/8] Activating PlgMimShapesJP in ScadaWeb config...
powershell -NoProfile -ExecutionPolicy Bypass -Command "$path='%WEB_CONFIG%'; [xml]$xml=Get-Content -LiteralPath $path; $plugins=$xml.ScadaWebConfig.Plugins; if (-not ($plugins.Plugin | Where-Object { $_.code -eq 'PlgMimShapesJP' })) { $node=$xml.CreateElement('Plugin'); $node.SetAttribute('code','PlgMimShapesJP'); [void]$plugins.AppendChild($node); $xml.Save($path); Write-Host '[OK] PlgMimShapesJP added to ScadaWebConfig.xml'; } else { Write-Host '[OK] PlgMimShapesJP is already active in ScadaWebConfig.xml'; }" || goto :deploy_error

echo [8/8] Starting ScadaWeb service...
call "%WEB_START%"
if errorlevel 1 goto :service_start_error
set "SERVICE_STOPPED=0"

echo [OK] MimShapes plugin has been built, deployed, and ScadaWeb was started.
exit /b 0

:build_error
echo [ERROR] Build failed.
exit /b 1

:service_stop_error
echo [ERROR] ScadaWeb could not be stopped. Deployment was not started.
exit /b 1

:service_start_error
echo [ERROR] ScadaWeb could not be started. Retrying once...
goto :deploy_error

:deploy_error
echo [ERROR] Copy/deploy failed.
if "%SERVICE_STOPPED%"=="1" (
    echo [INFO] Restoring ScadaWeb service after the failure...
    call "%WEB_START%"
    if errorlevel 1 (
        echo [ERROR] ScadaWeb remains stopped. Start it manually with: %WEB_START%
    ) else (
        set "SERVICE_STOPPED=0"
        echo [OK] ScadaWeb was restarted after the deployment failure.
    )
)
exit /b 1
