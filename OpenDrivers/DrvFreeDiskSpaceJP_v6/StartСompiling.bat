@ECHO OFF
cd /d "%~dp0"
==================
ECHO TASKKILL ScadaAdmin
taskkill /im ScadaAdmin.exe /F
==================
ECHO BUILDING WIN-X32...
===================
ECHO COMPILE LOGIC...
dotnet publish ".\DrvFreeDiskSpaceJP.Logic\DrvFreeDiskSpaceJP.Logic.csproj" -c Release -r win-x32 --framework net8.0 --self-contained false --output ./output
dotnet publish ".\DrvFreeDiskSpaceJP.Logic\DrvFreeDiskSpaceJP.Logic.csproj" -c Release -r win-x32 --framework net8.0 --self-contained false --output ".\Release\win-x32\SCADA\ScadaComm\Drv"
del ".\Release\win-x32\SCADA\ScadaComm\Drv\Scada*.dll"
del ".\Release\win-x32\SCADA\ScadaComm\Drv\*.pdb"
del ".\Release\win-x32\SCADA\ScadaComm\Drv\*.json"
===================
ECHO COMPILE VIEW...
dotnet publish ".\DrvFreeDiskSpaceJP.View\DrvFreeDiskSpaceJP.View.csproj" -c Release -r win-x32 --framework net8.0-windows --self-contained false --output ./output
dotnet publish ".\DrvFreeDiskSpaceJP.View\DrvFreeDiskSpaceJP.View.csproj" -c Release -r win-x32 --framework net8.0-windows --self-contained false --output ".\Release\win-x32\SCADA\ScadaAdmin\Lib"
del ".\Release\win-x32\SCADA\ScadaAdmin\Lib\Scada*.dll"
del ".\Release\win-x32\SCADA\ScadaAdmin\Lib\*.pdb"
del ".\Release\win-x32\SCADA\ScadaAdmin\Lib\*.json"
move ".\Release\win-x32\SCADA\ScadaAdmin\Lib\Lang" ".\Release\win-x32\SCADA\ScadaAdmin\"
==================
ECHO BUILDING WIN-X64...
===================
ECHO COMPILE LOGIC...
dotnet publish ".\DrvFreeDiskSpaceJP.Logic\DrvFreeDiskSpaceJP.Logic.csproj" -c Release -r win-x64 --framework net8.0 --self-contained false --output ./output
dotnet publish ".\DrvFreeDiskSpaceJP.Logic\DrvFreeDiskSpaceJP.Logic.csproj" -c Release -r win-x64 --framework net8.0 --self-contained false --output ".\Release\win-x64\SCADA\ScadaComm\Drv"
del ".\Release\win-x64\SCADA\ScadaComm\Drv\Scada*.dll"
del ".\Release\win-x64\SCADA\ScadaComm\Drv\*.pdb"
del ".\Release\win-x64\SCADA\ScadaComm\Drv\*.json"
===================
ECHO COMPILE VIEW...
dotnet publish ".\DrvFreeDiskSpaceJP.View\DrvFreeDiskSpaceJP.View.csproj" -c Release -r win-x64 --framework net8.0-windows --self-contained false --output ./output
dotnet publish ".\DrvFreeDiskSpaceJP.View\DrvFreeDiskSpaceJP.View.csproj" -c Release -r win-x64 --framework net8.0-windows --self-contained false --output ".\Release\win-x64\SCADA\ScadaAdmin\Lib"
del ".\Release\win-x64\SCADA\ScadaAdmin\Lib\Scada*.dll"
del ".\Release\win-x64\SCADA\ScadaAdmin\Lib\*.pdb"
del ".\Release\win-x64\SCADA\ScadaAdmin\Lib\*.json"
move ".\Release\win-x64\SCADA\ScadaAdmin\Lib\Lang" ".\Release\win-x64\SCADA\ScadaAdmin\"
==================
ECHO COPY
move ".\Release\win-x64\SCADA\ScadaAdmin\Lib\DrvFreeDiskSpaceJP.View.dll" ".\Libraries_v8.0\DrvFreeDiskSpaceJP.View.dll" /Y
move ".\Release\win-x64\SCADA\ScadaAdmin\Lib\LicenseJP.View.dll" ".\Libraries_v8.0\LicenseJP.View.dll" /Y
move ".\Release\win-x64\SCADA\ScadaAdmin\Lib\UtilsJP.Form.dll" ".\Libraries_v8.0\UtilsJP.Form.dll" /Y

move ".\Release\win-x64\SCADA\ScadaComm\Drv\DrvFreeDiskSpaceJP.Logic.dll" ".\Libraries_v8.0\DrvFreeDiskSpaceJP.Logic.dll" /Y
move ".\Release\win-x64\SCADA\ScadaComm\Drv\LicenseJP.Logic.dll" ".\Libraries_v8.0\LicenseJP.Logic.dll" /Y
==================
ECHO BUILDING LINUX-X64...
===================
ECHO COMPILE LOGIC...
dotnet publish ".\DrvFreeDiskSpaceJP.Logic\DrvFreeDiskSpaceJP.Logic.csproj" -c Release -r linux-x64 --framework net8.0 --self-contained false --output ./output
dotnet publish ".\DrvFreeDiskSpaceJP.Logic\DrvFreeDiskSpaceJP.Logic.csproj" -c Release -r linux-x64 --framework net8.0 --self-contained false --output ".\Release\linux-x64\SCADA\ScadaComm\Drv"
del ".\Release\linux-x64\SCADA\ScadaComm\Drv\Scada*.dll"
del ".\Release\linux-x64\SCADA\ScadaComm\Drv\*.pdb"
del ".\Release\linux-x64\SCADA\ScadaComm\Drv\*.json"
===================
ECHO COMPILE VIEW...
xcopy ".\Release\win-x64\SCADA\ScadaAdmin\" ".\Release\linux-x64\SCADA\ScadaAdmin\" /Y /E
===================
ECHO STOP SERVICE
NET STOP ScadaAgent6
taskkill /IM ScadaAgentWkr.exe /F
NET STOP ScadaComm6
taskkill /IM ScadaCommWkr.exe /F
NET STOP ScadaServer6
taskkill /IM ScadaServerWkr.exe /F
===================
ECHO COPY FILES
IF EXIST ".\Release\win-x64\SCADA" XCOPY /S ".\Release\win-x64\SCADA\*.*" "C:\Program Files\SCADA\*.*" /Y
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