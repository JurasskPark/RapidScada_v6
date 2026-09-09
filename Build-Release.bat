@echo off
setlocal
if not exist "%~dp0Build-Release.ps1" (
    echo [ERROR] Build-Release.ps1 is missing from "%~dp0".
    echo Check antivirus quarantine or restore the script from a trusted source.
    exit /b 1
)
where pwsh >nul 2>&1
if errorlevel 1 (
    echo [ERROR] PowerShell 7 is required. Install it and run this script again.
    exit /b 1
)
if "%~1"=="" (
    pwsh -NoLogo -NoProfile -ExecutionPolicy Bypass -File "%~dp0Build-Release.ps1" -All
) else (
    pwsh -NoLogo -NoProfile -ExecutionPolicy Bypass -File "%~dp0Build-Release.ps1" %*
)
set "BUILD_EXIT_CODE=%ERRORLEVEL%"
endlocal & exit /b %BUILD_EXIT_CODE%
