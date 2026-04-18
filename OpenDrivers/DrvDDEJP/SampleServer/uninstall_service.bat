@echo off
setlocal EnableExtensions

set "SERVICE_NAME=SampleDdeServer"

echo [INFO] Checking service "%SERVICE_NAME%"...
sc.exe query "%SERVICE_NAME%" >nul 2>&1
if not %errorlevel%==0 (
  echo [INFO] Service "%SERVICE_NAME%" not found.
  exit /b 0
)

echo [INFO] Stopping service "%SERVICE_NAME%"...
sc.exe stop "%SERVICE_NAME%" >nul 2>&1

echo [INFO] Waiting for service stop...
for /l %%i in (1,1,15) do (
  sc.exe query "%SERVICE_NAME%" | findstr /i "STOPPED" >nul 2>&1
  if %errorlevel%==0 goto :delete_service
  timeout /t 1 /nobreak >nul
)

echo [WARN] Service did not stop in time. Trying delete anyway.

:delete_service
echo [INFO] Deleting service "%SERVICE_NAME%"...
sc.exe delete "%SERVICE_NAME%"
if not %errorlevel%==0 (
  echo [ERROR] Failed to delete service.
  exit /b 1
)

echo [OK] Service "%SERVICE_NAME%" deleted.
exit /b 0
