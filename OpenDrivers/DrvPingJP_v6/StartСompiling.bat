@ECHO OFF
cd /d "%~dp0"
===================
ECHO TASKKILL ScadaAdmin
taskkill /im ScadaAdmin.exe /F
===================
ECHO BUILDING...
set msbuild="C:\Program Files\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin\MSBuild.exe"
===================
ECHO COMPILE...
%msbuild% ".\DrvPingJP_v6\DrvPingJP.Logic\DrvPingJP.Logic.csproj" /t:Build /p:Configuration=Release
%msbuild% ".\DrvPingJP_v6\DrvPingJP.View\DrvPingJP.View.csproj" /t:Build /p:Configuration=Release

cd /d .\DrvPingJP.Logic\ 
dotnet publish -c Release

cd /d "%~dp0"

cd /d .\DrvPingJP.View\ 
dotnet publish -c Release

cd /d "%~dp0"
===================
ECHO SERVICE STOP
NET STOP ScadaAgent6
taskkill /IM ScadaAgentWkr.exe /F
NET STOP ScadaComm6
taskkill /IM ScadaCommWkr.exe /F
NET STOP ScadaServer6
taskkill /IM ScadaServerWkr.exe /F
===================
ECHO COPYING FILES...
IF EXIST ".\DrvPingJP.Logic\bin\Release\net8.0\*.dll" COPY ".\DrvPingJP.Logic\bin\Release\net8.0\*.dll" "C:\Program Files\SCADA\ScadaComm\Drv\*.dll" /Y
IF EXIST ".\DrvPingJP.View\bin\Release\net8.0-windows\*.dll" COPY ".\DrvPingJP.View\bin\Release\net8.0-windows\*.dll" "C:\Program Files\SCADA\ScadaAdmin\Lib\*.dll" /Y
IF EXIST ".\DrvPingJP.View\bin\Release\net8.0-windows\Lang\*.xml" COPY ".\DrvPingJP.View\bin\Release\net8.0-windows\Lang\*.xml" "C:\Program Files\SCADA\ScadaAdmin\Lang\*.xml" /Y
===================
ECHO SERVICE START
NET START ScadaAgent6
NET START ScadaComm6
NET START ScadaServer6
===================
ECHO START APP
"C:\Program Files\SCADA\ScadaAdmin\ScadaAdmin.exe"



