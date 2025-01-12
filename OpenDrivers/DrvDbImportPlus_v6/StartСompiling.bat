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
ECHO %msbuild% ".\DrvDbImportPlus.Logic\DrvDbImportPlus.Logic.csproj" /t:Build /p:Configuration=Release
%msbuild% ".\DrvDbImportPlus.Logic\DrvDbImportPlus.Logic.csproj" /t:Build /p:Configuration=Release
ECHO %msbuild% ".\DrvDbImportPlus.View\DrvDbImportPlus.View.csproj" /t:Build /p:Configuration=Release
%msbuild% ".\DrvDbImportPlus.View\DrvDbImportPlus.View.csproj" /t:Build /p:Configuration=Release

cd /d .\DrvDbImportPlus.Logic\ 
dotnet publish -c Release

cd /d "%~dp0"

cd /d .\DrvDbImportPlus.View\ 
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
REM IF EXIST ".\DrvDbImportPlus.Logic\bin\Release\net8.0\*.dll" COPY ".\DrvDbImportPlus.Logic\bin\Release\net8.0\*.dll" "C:\Program Files\SCADA\ScadaComm\Drv\*.dll" /Y
REM IF EXIST ".\DrvDbImportPlus.View\bin\Release\net8.0-windows\*.dll" COPY ".\DrvDbImportPlus.View\bin\Release\net8.0-windows\*.dll" "C:\Program Files\SCADA\ScadaAdmin\Lib\*.dll" /Y

IF EXIST ".\DrvDbImportPlus.Logic\bin\Release\net8.0\DrvDbImportPlus.Logic.dll" COPY ".\DrvDbImportPlus.Logic\bin\Release\net8.0\DrvDbImportPlus.Logic.dll" "C:\Program Files\SCADA\ScadaComm\Drv\DrvDbImportPlus.Logic.dll" /Y
IF EXIST ".\DrvDbImportPlus.View\bin\Release\net8.0-windows\DrvDbImportPlus.View.dll" COPY ".\DrvDbImportPlus.View\bin\Release\net8.0-windows\DrvDbImportPlus.View.dll" "C:\Program Files\SCADA\ScadaAdmin\Lib\DrvDbImportPlus.View.dll" /Y

IF EXIST ".\DrvDbImportPlus.View\bin\Release\net8.0-windows\Lang\*.xml" COPY ".\DrvDbImportPlus.View\bin\Release\net8.0-windows\Lang\*.xml" "C:\Program Files\SCADA\ScadaAdmin\Lang\*.xml" /Y
===================
ECHO SERVICE START
NET START ScadaAgent6
NET START ScadaComm6
NET START ScadaServer6
===================
ECHO START APP
"C:\Program Files\SCADA\ScadaAdmin\ScadaAdmin.exe"

PAUSE



