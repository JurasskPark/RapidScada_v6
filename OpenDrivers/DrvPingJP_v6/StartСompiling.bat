@ECHO OFF
cd /d "%~dp0"
==================
ECHO TASKKILL ScadaAdmin
taskkill /im ScadaAdmin.exe /F
==================
ECHO BUILDING WIN-X32...
===================
ECHO COMPILE LOGIC...
dotnet publish ".\DrvPingJP.Logic\DrvPingJP.Logic.csproj" -c Release -r win-x32 --framework net8.0 --self-contained false --output ./output
dotnet publish ".\DrvPingJP.Logic\DrvPingJP.Logic.csproj" -c Release -r win-x32 --framework net8.0 --self-contained false --output ".\Release\win-x32\SCADA\ScadaComm\Drv"
del ".\Release\win-x32\SCADA\ScadaComm\Drv\Scada*.dll"
del ".\Release\win-x32\SCADA\ScadaComm\Drv\*.pdb"
del ".\Release\win-x32\SCADA\ScadaComm\Drv\*.json"
===================
ECHO COMPILE VIEW...
dotnet publish ".\DrvPingJP.View\DrvPingJP.View.csproj" -c Release -r win-x32 --framework net8.0-windows --self-contained false --output ./output
dotnet publish ".\DrvPingJP.View\DrvPingJP.View.csproj" -c Release -r win-x32 --framework net8.0-windows --self-contained false --output ".\Release\win-x32\SCADA\ScadaAdmin\Lib"
del ".\Release\win-x32\SCADA\ScadaAdmin\Lib\Scada*.dll"
del ".\Release\win-x32\SCADA\ScadaAdmin\Lib\*.pdb"
del ".\Release\win-x32\SCADA\ScadaAdmin\Lib\*.json"
move ".\Release\win-x32\SCADA\ScadaAdmin\Lib\Lang" ".\Release\win-x32\SCADA\ScadaAdmin\"
==================
ECHO BUILDING WIN-X64...
===================
ECHO COMPILE LOGIC...
dotnet publish ".\DrvPingJP.Logic\DrvPingJP.Logic.csproj" -c Release -r win-x64 --framework net8.0 --self-contained false --output ./output
dotnet publish ".\DrvPingJP.Logic\DrvPingJP.Logic.csproj" -c Release -r win-x64 --framework net8.0 --self-contained false --output ".\Release\win-x64\SCADA\ScadaComm\Drv"
del ".\Release\win-x64\SCADA\ScadaComm\Drv\Scada*.dll"
del ".\Release\win-x64\SCADA\ScadaComm\Drv\*.pdb"
del ".\Release\win-x64\SCADA\ScadaComm\Drv\*.json"
===================
ECHO COMPILE VIEW...
dotnet publish ".\DrvPingJP.View\DrvPingJP.View.csproj" -c Release -r win-x64 --framework net8.0-windows --self-contained false --output ./output
dotnet publish ".\DrvPingJP.View\DrvPingJP.View.csproj" -c Release -r win-x64 --framework net8.0-windows --self-contained false --output ".\Release\win-x64\SCADA\ScadaAdmin\Lib"
del ".\Release\win-x64\SCADA\ScadaAdmin\Lib\Scada*.dll"
del ".\Release\win-x64\SCADA\ScadaAdmin\Lib\*.pdb"
del ".\Release\win-x64\SCADA\ScadaAdmin\Lib\*.json"
move ".\Release\win-x64\SCADA\ScadaAdmin\Lib\Lang" ".\Release\win-x64\SCADA\ScadaAdmin\"
==================
ECHO BUILDING ANY CPU...
===================
ECHO COMPILE LOGIC...
dotnet publish ".\DrvPingJP.Logic\DrvPingJP.Logic.csproj" -c Release --framework net8.0 --self-contained false --output ./output
dotnet publish ".\DrvPingJP.Logic\DrvPingJP.Logic.csproj" -c Release --framework net8.0 --self-contained false --output ".\Release\anycpu\SCADA\ScadaComm\Drv"
del ".\Release\anycpu\SCADA\ScadaComm\Drv\Scada*.dll"
del ".\Release\anycpu\SCADA\ScadaComm\Drv\*.pdb"
del ".\Release\anycpu\SCADA\ScadaComm\Drv\*.json"
===================
ECHO COMPILE VIEW...
dotnet publish ".\DrvPingJP.View\DrvPingJP.View.csproj" -c Release --framework net8.0-windows --self-contained false --output ./output
dotnet publish ".\DrvPingJP.View\DrvPingJP.View.csproj" -c Release --framework net8.0-windows --self-contained false --output ".\Release\anycpu\SCADA\ScadaAdmin\Lib"
del ".\Release\anycpu\SCADA\ScadaAdmin\Lib\Scada*.dll"
del ".\Release\anycpu\SCADA\ScadaAdmin\Lib\*.pdb"
del ".\Release\anycpu\SCADA\ScadaAdmin\Lib\*.json"
move ".\Release\anycpu\SCADA\ScadaAdmin\Lib\Lang" ".\Release\anycpu\SCADA\ScadaAdmin\"
==================
ECHO STOP SERVICE
NET STOP ScadaAgent6
taskkill /IM ScadaAgentWkr.exe /F
NET STOP ScadaComm6
taskkill /IM ScadaCommWkr.exe /F
NET STOP ScadaServer6
taskkill /IM ScadaServerWkr.exe /F
===================
ECHO COPY FILES
IF EXIST ".\Release\anycpu\SCADA" XCOPY /S ".\Release\anycpu\SCADA\*.*" "C:\Program Files\SCADA\*.*" /Y
===================
===================
ECHO SERVICE START
NET START ScadaAgent6
NET START ScadaComm6
NET START ScadaServer6
===================
ECHO START APP
"C:\Program Files\SCADA\ScadaAdmin\ScadaAdmin.exe"

PAUSE