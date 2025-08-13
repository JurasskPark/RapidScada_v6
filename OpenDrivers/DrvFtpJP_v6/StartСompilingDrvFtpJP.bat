@ECHO OFF
cd /d "%~dp0"
==================
ECHO TASKKILL ScadaAdmin
taskkill /im ScadaAdmin.exe /F
==================
ECHO BUILDING ANYCPU...
===================
ECHO COMPILE LOGIC...
dotnet publish ".\DrvFtpJP.Logic\DrvFtpJP.Logic.csproj" -c Release --framework net8.0 --self-contained false --output ./output
dotnet publish ".\DrvFtpJP.Logic\DrvFtpJP.Logic.csproj" -c Release --framework net8.0 --self-contained false --output ".\Release\anycpu\SCADA\ScadaComm\Drv"
del ".\Release\anycpu\SCADA\ScadaComm\Drv\Scada*.dll"
del ".\Release\anycpu\SCADA\ScadaComm\Drv\*.pdb"
del ".\Release\anycpu\SCADA\ScadaComm\Drv\*.json"
===================
ECHO COMPILE VIEW...
dotnet publish ".\DrvFtpJP.View\DrvFtpJP.View.csproj" -c Release --framework net8.0-windows --self-contained false --output ./output
dotnet publish ".\DrvFtpJP.View\DrvFtpJP.View.csproj" -c Release --framework net8.0-windows --self-contained false --output ".\Release\anycpu\SCADA\ScadaAdmin\Lib"
del ".\Release\anycpu\SCADA\ScadaAdmin\Lib\Scada*.dll"
del ".\Release\anycpu\SCADA\ScadaAdmin\Lib\*.pdb"
del ".\Release\anycpu\SCADA\ScadaAdmin\Lib\*.json"
move ".\Release\anycpu\SCADA\ScadaAdmin\Lib\Lang" ".\Release\anycpu\SCADA\ScadaAdmin\"
==================
ECHO COMPILE WINFORM...
dotnet publish ".\DrvFtpJP.Winform\DrvFtpJP.Winform.csproj" -c Release --framework net8.0-windows --self-contained false --output ./output
dotnet publish ".\DrvFtpJP.Winform\DrvFtpJP.Winform.csproj" -c Release --framework net8.0-windows --self-contained false --output ".\Release\anycpu\App\"
==================
ECHO SERVICE START
NET START ScadaAgent6
NET START ScadaComm6
NET START ScadaServer6
===================
ECHO COPY FILES
IF EXIST ".\Release\anycpu\SCADA" XCOPY /S ".\Release\anycpu\SCADA\*.*" "C:\Program Files\SCADA\*.*" /Y
===================
ECHO SERVICE START
NET START ScadaAgent6
NET START ScadaComm6
NET START ScadaServer6
===================
ECHO START APP
"C:\Program Files\SCADA\ScadaAdmin\ScadaAdmin.exe"


