@ECHO OFF
===================
ECHO TASKKILL ScadaAdmin
taskkill /im ScadaAdmin.exe /F
===================
ECHO BUILDING...

set msbuild="C:\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin\MSBuild.exe"
===================
ECHO COMPILE...
%msbuild% .\DrvDbImportPlus.Logic\DrvDbImportPlus.Logic.csproj /t:Build /p:Configuration=Release
%msbuild% .\DrvDbImportPlus.View\DrvDbImportPlus.View.csproj /t:Build /p:Configuration=Release

cd /d .\DrvDbImportPlus.Logic\ 
dotnet publish -c Release

cd /d "%~dp0"

cd /d .\DrvDbImportPlus.View\ 
dotnet publish -c Release

cd /d "%~dp0"
===================
ECHO SERVICE STOP
NET STOP ScadaAgent6
NET STOP ScadaComm6
NET STOP ScadaServer6
===================
ECHO COPYING FILES...
IF EXIST ".\DrvDbImportPlus.Logic\bin\Release\net6.0-windows\*.dll" COPY ".\DrvDbImportPlus.Logic\bin\Release\net6.0-windows\*.dll" "C:\SCADA_6\ScadaComm\Drv\*.dll" /Y
IF EXIST ".\DrvDbImportPlus.View\bin\Release\net6.0-windows\*.dll" COPY ".\DrvDbImportPlus.View\bin\Release\net6.0-windows\*.dll" "C:\SCADA_6\ScadaAdmin\Lib\*.dll" /Y
IF EXIST ".\DrvDbImportPlus.View\bin\Release\net6.0-windows\Lang\*.xml" COPY ".\DrvDbImportPlus.View\bin\Release\net6.0-windows\Lang\*.xml" "C:\SCADA_6\ScadaAdmin\Lang\*.xml" /Y
===================
ECHO SERVICE START
NET START ScadaAgent6
NET START ScadaComm6
NET START ScadaServer6
===================
ECHO START APP
"C:\SCADA_6\ScadaAdmin\ScadaAdmin.exe"



