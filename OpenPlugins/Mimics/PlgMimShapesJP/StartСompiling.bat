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

set "DOTNET_CLI_HOME=%ROOT%.dotnet"
set "NUGET_PACKAGES=%ROOT%.nuget\packages"
if not exist "%DOTNET_CLI_HOME%" mkdir "%DOTNET_CLI_HOME%"
if not exist "%NUGET_PACKAGES%" mkdir "%NUGET_PACKAGES%"

echo [1/7] Building PlgMimShapesJP...
dotnet build "%PRJ_WEB%" -c Release -v minimal || goto :build_error

echo [2/7] Building PlgMimShapesJP.View...
dotnet build "%PRJ_VIEW%" -c Release -v minimal || goto :build_error

echo [3/7] Activating PlgMimShapesJP in ScadaWeb config...
powershell -NoProfile -ExecutionPolicy Bypass -Command "$path='%WEB_CONFIG%'; [xml]$xml=Get-Content -LiteralPath $path; $plugins=$xml.ScadaWebConfig.Plugins; if (-not ($plugins.Plugin | Where-Object { $_.code -eq 'PlgMimShapesJP' })) { $node=$xml.CreateElement('Plugin'); $node.SetAttribute('code','PlgMimShapesJP'); [void]$plugins.AppendChild($node); $xml.Save($path); Write-Host '[OK] PlgMimShapesJP added to ScadaWebConfig.xml'; } else { Write-Host '[OK] PlgMimShapesJP is already active in ScadaWebConfig.xml'; }" || goto :copy_error

echo [4/7] Stopping ScadaWeb service...
set "WEB_STOP=%WEB_DST%\svc_stop.bat"
if not exist "%WEB_STOP%" (
    echo [ERROR] Stop script not found: %WEB_STOP%
    exit /b 1
)
call "%WEB_STOP%"

echo [5/7] Deploying binaries...
copy /Y "%OUT_WEB%\PlgMimShapesJP.dll" "%WEB_DST%\PlgMimShapesJP.dll" >nul || goto :copy_error
copy /Y "%OUT_VIEW%\PlgMimShapesJP.View.dll" "%ADMIN_LIB_DST%\PlgMimShapesJP.View.dll" >nul || goto :copy_error

echo [6/7] Deploying language and web resources...
copy /Y "%SRC_LANG%\PlgMimShapesJP.en-GB.xml" "%DST_LANG%\PlgMimShapesJP.en-GB.xml" >nul || goto :copy_error
copy /Y "%SRC_LANG%\PlgMimShapesJP.ru-RU.xml" "%DST_LANG%\PlgMimShapesJP.ru-RU.xml" >nul || goto :copy_error
if not exist "%DST_PLUGIN_WWW%" mkdir "%DST_PLUGIN_WWW%"
robocopy "%SRC_PLUGIN_WWW%" "%DST_PLUGIN_WWW%" /E /R:1 /W:1 /NFL /NDL /NJH /NJS >nul
if errorlevel 8 goto :copy_error

echo [7/7] Starting ScadaWeb service...
set "WEB_START=%WEB_DST%\svc_start.bat"
if not exist "%WEB_START%" (
    echo [ERROR] Start script not found: %WEB_START%
    exit /b 1
)
call "%WEB_START%"

echo [OK] MimShapes plugin has been built, deployed, and ScadaWeb was started.
exit /b 0

:build_error
echo [ERROR] Build failed.
exit /b 1

:copy_error
echo [ERROR] Copy/deploy failed.
exit /b 1
