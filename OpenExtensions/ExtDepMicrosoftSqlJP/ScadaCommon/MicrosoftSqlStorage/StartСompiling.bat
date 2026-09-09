@echo off
setlocal
where pwsh >nul 2>&1
if errorlevel 1 (
    echo [ERROR] PowerShell 7 is required. Install it and run this script again.
    exit /b 1
)
pwsh -NoLogo -NoProfile -ExecutionPolicy Bypass -File "%~dp0..\..\..\..\Build-Release.ps1" -Project "%~dp0release.json" %*
set "BUILD_EXIT_CODE=%ERRORLEVEL%"
endlocal & exit /b %BUILD_EXIT_CODE%
