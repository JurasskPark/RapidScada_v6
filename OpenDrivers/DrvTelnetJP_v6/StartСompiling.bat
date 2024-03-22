@ECHO OFF
===================
ECHO TASKKILL ScadaAdmin
taskkill /im ScadaAdmin.exe /F
===================
ECHO BUILDING...

set msbuild="C:\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin\MSBuild.exe"
===================
ECHO COMPILE...
%msbuild% .\DrvTelnetJP.Logic\DrvTelnetJP.Logic.csproj /t:Build /p:Configuration=Release
%msbuild% .\DrvTelnetJP.View\DrvTelnetJP.View.csproj /t:Build /p:Configuration=Release

cd /d .\DrvTelnetJP.Logic\ 
dotnet publish -c Release

cd /d "%~dp0"

cd /d .\DrvTelnetJP.View\ 
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
IF EXIST ".\DrvTelnetJP.Logic\bin\Release\net6.0\DrvTelnetJP.Logic.dll" COPY ".\DrvTelnetJP.Logic\bin\Release\net6.0\DrvTelnetJP.Logic.dll" "C:\SCADA_6\ScadaComm\Drv\*.dll" /Y
IF EXIST ".\DrvTelnetJP.View\bin\Release\net6.0-windows\DrvTelnetJP.View.dll" COPY ".\DrvTelnetJP.View\bin\Release\net6.0-windows\DrvTelnetJP.View.dll" "C:\SCADA_6\ScadaAdmin\Lib\*.dll" /Y
IF EXIST ".\DrvTelnetJP.View\bin\Release\net6.0-windows\Lang\*.xml" COPY ".\DrvTelnetJP.View\bin\Release\net6.0-windows\Lang\*.xml" "C:\SCADA_6\ScadaAdmin\Lang\*.xml" /Y
===================
ECHO SERVICE START
NET START ScadaAgent6
NET START ScadaComm6
NET START ScadaServer6
===================
ECHO START APP
"C:\SCADA_6\ScadaAdmin\ScadaAdmin.exe"



