@ECHO OFF
cd /d "%~dp0"

REM ==================
REM Убиваем процесс ScadaAdmin
ECHO TASKKILL ScadaAdmin
taskkill /im ScadaAdmin.exe /F 2>nul

REM ==================
REM Сборка для Windows x64
ECHO BUILDING WINDOWS x64...
ECHO ===================

REM Компиляция логики для Windows x64
ECHO COMPILE LOGIC for Windows x64...
dotnet publish ".\DrvDbImportPlus.Logic\DrvDbImportPlus.Logic.csproj" -c Release --framework net8.0 --runtime win-x64 --self-contained false --output "./output_win_x64"
dotnet publish ".\DrvDbImportPlus.Logic\DrvDbImportPlus.Logic.csproj" -c Release --framework net8.0 --runtime win-x64 --self-contained false --output ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaComm\Drv"
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaComm\Drv\Scada*.dll" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaComm\Drv\*.pdb" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaComm\Drv\*.json" 2>nul

REM Компиляция View для Windows x64
ECHO COMPILE VIEW for Windows x64...
dotnet publish ".\DrvDbImportPlus.View\DrvDbImportPlus.View.csproj" -c Release --framework net8.0-windows --runtime win-x64 --self-contained false --output "./output_win_x64"
dotnet publish ".\DrvDbImportPlus.View\DrvDbImportPlus.View.csproj" -c Release --framework net8.0-windows --runtime win-x64 --self-contained false --output ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaAdmin\Lib"
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaAdmin\Lib\Scada*.dll" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaAdmin\Lib\*.pdb" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaAdmin\Lib\*.json" 2>nul
if exist ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaAdmin\Lib\Lang" move ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaAdmin\Lib\Lang" ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\ScadaAdmin\" 2>nul

REM Компиляция WinForm для Windows x64
ECHO COMPILE WINFORM for Windows x64...
dotnet publish ".\DrvDbImportPlus.Winform\DrvDbImportPlus.Winform.csproj" -c Release --framework net8.0-windows --runtime win-x64 --self-contained false --output "./output_win_x64"
dotnet publish ".\DrvDbImportPlus.Winform\DrvDbImportPlus.Winform.csproj" -c Release --framework net8.0-windows --runtime win-x64 --self-contained false --output ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\App\"

REM ==================
REM Сборка для Windows x86 (32-bit)
ECHO BUILDING WINDOWS x86 (32-bit)...
ECHO ===================

REM Компиляция логики для Windows x86
ECHO COMPILE LOGIC for Windows x86...
dotnet publish ".\DrvDbImportPlus.Logic\DrvDbImportPlus.Logic.csproj" -c Release --framework net8.0 --runtime win-x86 --self-contained false --output "./output_win_x86"
dotnet publish ".\DrvDbImportPlus.Logic\DrvDbImportPlus.Logic.csproj" -c Release --framework net8.0 --runtime win-x86 --self-contained false --output ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaComm\Drv"
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaComm\Drv\Scada*.dll" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaComm\Drv\*.pdb" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaComm\Drv\*.json" 2>nul

REM Компиляция View для Windows x86
ECHO COMPILE VIEW for Windows x86...
dotnet publish ".\DrvDbImportPlus.View\DrvDbImportPlus.View.csproj" -c Release --framework net8.0-windows --runtime win-x86 --self-contained false --output "./output_win_x86"
dotnet publish ".\DrvDbImportPlus.View\DrvDbImportPlus.View.csproj" -c Release --framework net8.0-windows --runtime win-x86 --self-contained false --output ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaAdmin\Lib"
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaAdmin\Lib\Scada*.dll" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaAdmin\Lib\*.pdb" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaAdmin\Lib\*.json" 2>nul
if exist ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaAdmin\Lib\Lang" move ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaAdmin\Lib\Lang" ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\SCADA\ScadaAdmin\" 2>nul

REM Компиляция WinForm для Windows x86 (WinForms проекты могут не поддерживать x86)
ECHO COMPILE WINFORM for Windows x86...
dotnet publish ".\DrvDbImportPlus.Winform\DrvDbImportPlus.Winform.csproj" -c Release --framework net8.0-windows --runtime win-x86 --self-contained false --output "./output_win_x86"
dotnet publish ".\DrvDbImportPlus.Winform\DrvDbImportPlus.Winform.csproj" -c Release --framework net8.0-windows --runtime win-x86 --self-contained false --output ".\DrvDbImportPlus_6.0.0.0_Release_win-x32\App\"

REM ==================
REM Сборка для Linux x64
ECHO BUILDING LINUX x64...
ECHO ===================

REM Компиляция логики для Linux x64
ECHO COMPILE LOGIC for Linux x64...
dotnet publish ".\DrvDbImportPlus.Logic\DrvDbImportPlus.Logic.csproj" -c Release --framework net8.0 --runtime linux-x64 --self-contained false --output "./output_linux_x64"
dotnet publish ".\DrvDbImportPlus.Logic\DrvDbImportPlus.Logic.csproj" -c Release --framework net8.0 --runtime linux-x64 --self-contained false --output ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaComm\Drv"
del ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaComm\Drv\Scada*.dll" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaComm\Drv\*.pdb" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaComm\Drv\*.json" 2>nul

REM Компиляция View для Linux x64 (View может быть Windows-specific)
ECHO COMPILE VIEW for Linux x64...
dotnet publish ".\DrvDbImportPlus.View\DrvDbImportPlus.View.csproj" -c Release --framework net8.0-windows --runtime win-x64 --self-contained false --output "./output_linux_x64"
dotnet publish ".\DrvDbImportPlus.View\DrvDbImportPlus.View.csproj" -c Release --framework net8.0-windows --runtime win-x64 --self-contained false --output ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaAdmin\Lib"
del ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaAdmin\Lib\Scada*.dll" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaAdmin\Lib\*.pdb" 2>nul
del ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaAdmin\Lib\*.json" 2>nul
if exist ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaAdmin\Lib\Lang" move ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaAdmin\Lib\Lang" ".\DrvDbImportPlus_6.0.0.0_Release_linux-x64\SCADA\ScadaAdmin\" 2>nul

REM Компиляция WinForm для Linux x64 (WinForms не поддерживается на Linux, поэтому может потребоваться условие)
ECHO NOTE: WinForms is not supported on Linux, skipping WinForm compilation for Linux

REM ==================
REM Очистка временных папок
ECHO CLEANING TEMP FOLDERS...
if exist ".\output_win_x64" rmdir /s /q ".\output_win_x64"
if exist ".\output_win_x86" rmdir /s /q ".\output_win_x86"
if exist ".\output_linux_x64" rmdir /s /q ".\output_linux_x64"

REM ==================
REM Запуск служб и копирование файлов (только для Windows)
ECHO SERVICE OPERATIONS (Windows only)...
ECHO ===================

REM Запуск служб Windows
NET START ScadaAgent6 2>nul || ECHO Failed to start ScadaAgent6 or service not found
NET START ScadaComm6 2>nul || ECHO Failed to start ScadaComm6 or service not found
NET START ScadaServer6 2>nul || ECHO Failed to start ScadaServer6 or service not found

ECHO COPY FILES to Program Files...
IF EXIST ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA" (
    XCOPY /S ".\DrvDbImportPlus_6.0.0.0_Release_win-x64\SCADA\*.*" "C:\Program Files\SCADA\*.*" /Y 2>nul || ECHO Failed to copy files to C:\Program Files\SCADA
)

REM Перезапуск служб после копирования
ECHO RESTARTING SERVICES...
NET STOP ScadaAgent6 2>nul
NET STOP ScadaComm6 2>nul
NET STOP ScadaServer6 2>nul
timeout /t 3 /nobreak >nul
NET START ScadaAgent6 2>nul || ECHO Failed to start ScadaAgent6
NET START ScadaComm6 2>nul || ECHO Failed to start ScadaComm6
NET START ScadaServer6 2>nul || ECHO Failed to start ScadaServer6

REM ==================
REM Запуск приложения
ECHO START APP
if exist "C:\Program Files\SCADA\ScadaAdmin\ScadaAdmin.exe" (
    start "" "C:\Program Files\SCADA\ScadaAdmin\ScadaAdmin.exe"
) else (
    ECHO ScadaAdmin.exe not found at C:\Program Files\SCADA\ScadaAdmin\
)

ECHO ==================
ECHO BUILD COMPLETED!
ECHO ==================
ECHO Created 3 versions:
ECHO 1. .\DrvDbImportPlus_6.0.0.0_Release_win-x64\ - Windows 64-bit
ECHO 2. .\DrvDbImportPlus_6.0.0.0_Release_win-x32\ - Windows 32-bit
ECHO 3. .\DrvDbImportPlus_6.0.0.0_Release_linux-x64\ - Linux 64-bit
ECHO ==================

pause