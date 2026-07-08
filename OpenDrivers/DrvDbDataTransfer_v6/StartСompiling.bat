@ECHO OFF
SETLOCAL EnableExtensions
CD /D "%~dp0"

SET "PACKAGE_VERSION=6.0.0.0"
SET "PACKAGE_PREFIX=DrvDbDataTransfer_%PACKAGE_VERSION%"
SET "INSTALL_SCADA_DIR=C:\Program Files\SCADA"

IF /I "%~1"=="--package-only" SET "PACKAGE_ONLY=1"

ECHO ==================
ECHO Building DrvDbDataTransfer release packages
ECHO ==================

CALL :BuildPackage "%PACKAGE_PREFIX%_win-x64" "win-x64" "win-x64" "1"
IF ERRORLEVEL 1 EXIT /B 1

CALL :BuildPackage "%PACKAGE_PREFIX%_win-x32" "win-x86" "win-x86" "1"
IF ERRORLEVEL 1 EXIT /B 1

CALL :BuildPackage "%PACKAGE_PREFIX%_linux-x64" "linux-x64" "win-x64" "0"
IF ERRORLEVEL 1 EXIT /B 1

CALL :BuildPackage "%PACKAGE_PREFIX%_anycpu" "anycpu" "anycpu" "1"
IF ERRORLEVEL 1 EXIT /B 1

IF DEFINED PACKAGE_ONLY GOTO Summary

ECHO ==================
ECHO Deploying Windows x64 release package to %INSTALL_SCADA_DIR%
ECHO ==================

ECHO TASKKILL ScadaAdmin
TASKKILL /IM ScadaAdmin.exe /F 2>NUL

ECHO STOPPING SERVICES...
NET STOP ScadaAgent6 2>NUL
NET STOP ScadaComm6 2>NUL
NET STOP ScadaServer6 2>NUL
TIMEOUT /T 3 /NOBREAK >NUL

ECHO COPY FILES to %INSTALL_SCADA_DIR%...
IF EXIST ".\%PACKAGE_PREFIX%_win-x64\Release\SCADA" (
    XCOPY /S /Y /I ".\%PACKAGE_PREFIX%_win-x64\Release\SCADA\*.*" "%INSTALL_SCADA_DIR%\"
    IF ERRORLEVEL 1 (
        ECHO Failed to copy files to %INSTALL_SCADA_DIR%
        EXIT /B 1
    )
) ELSE (
    ECHO Build output folder not found: .\%PACKAGE_PREFIX%_win-x64\Release\SCADA
    EXIT /B 1
)

ECHO STARTING SERVICES...
NET START ScadaAgent6 2>NUL || ECHO Failed to start ScadaAgent6
NET START ScadaComm6 2>NUL || ECHO Failed to start ScadaComm6
NET START ScadaServer6 2>NUL || ECHO Failed to start ScadaServer6

ECHO START APP
IF EXIST "%INSTALL_SCADA_DIR%\ScadaAdmin\ScadaAdmin.exe" (
    START "" "%INSTALL_SCADA_DIR%\ScadaAdmin\ScadaAdmin.exe"
) ELSE (
    ECHO ScadaAdmin.exe not found at %INSTALL_SCADA_DIR%\ScadaAdmin\
)

:Summary
ECHO ==================
ECHO BUILD COMPLETED
ECHO ==================
ECHO Created release folders:
ECHO 1. .\%PACKAGE_PREFIX%_win-x64\Release\SCADA
ECHO 2. .\%PACKAGE_PREFIX%_win-x32\Release\SCADA
ECHO 3. .\%PACKAGE_PREFIX%_linux-x64\Release\SCADA
ECHO 4. .\%PACKAGE_PREFIX%_anycpu\Release\SCADA
ECHO.
ECHO Expected release structure:
ECHO Release
ECHO   SCADA
ECHO     ScadaAdmin
ECHO       Lang
ECHO       Lib
ECHO     ScadaComm
ECHO       Drv
ECHO ==================

IF NOT DEFINED PACKAGE_ONLY PAUSE
EXIT /B 0

:BuildPackage
SET "PACKAGE_DIR=%~1"
SET "LOGIC_RUNTIME=%~2"
SET "VIEW_RUNTIME=%~3"
SET "BUILD_WINFORM=%~4"
SET "RELEASE_DIR=.\%PACKAGE_DIR%\Release"
SET "SCADA_DIR=%RELEASE_DIR%\SCADA"

ECHO.
ECHO ==================
ECHO BUILDING %PACKAGE_DIR%
ECHO ==================

CALL :PrepareReleasePackage "%PACKAGE_DIR%"
IF ERRORLEVEL 1 EXIT /B 1

ECHO COMPILE LOGIC for %LOGIC_RUNTIME%...
IF /I "%LOGIC_RUNTIME%"=="anycpu" (
    DOTNET publish ".\DrvDbDataTransfer.Logic\DrvDbDataTransfer.Logic.csproj" -c Release --framework net8.0 --self-contained false --output "%SCADA_DIR%\ScadaComm\Drv"
) ELSE (
    DOTNET publish ".\DrvDbDataTransfer.Logic\DrvDbDataTransfer.Logic.csproj" -c Release --framework net8.0 --runtime %LOGIC_RUNTIME% --self-contained false --output "%SCADA_DIR%\ScadaComm\Drv"
)
IF ERRORLEVEL 1 EXIT /B 1
CALL :CleanPluginFolder "%SCADA_DIR%\ScadaComm\Drv"
IF ERRORLEVEL 1 EXIT /B 1
CALL :MoveRootRuntimeFolder "%SCADA_DIR%\ScadaComm\Drv" "DrvDbDataTransfer.Logic"
IF ERRORLEVEL 1 EXIT /B 1

ECHO COMPILE VIEW for %VIEW_RUNTIME%...
IF /I "%VIEW_RUNTIME%"=="anycpu" (
    DOTNET publish ".\DrvDbDataTransfer.View\DrvDbDataTransfer.View.csproj" -c Release --framework net8.0-windows --self-contained false --output "%SCADA_DIR%\ScadaAdmin\Lib"
) ELSE (
    DOTNET publish ".\DrvDbDataTransfer.View\DrvDbDataTransfer.View.csproj" -c Release --framework net8.0-windows --runtime %VIEW_RUNTIME% --self-contained false --output "%SCADA_DIR%\ScadaAdmin\Lib"
)
IF ERRORLEVEL 1 EXIT /B 1
CALL :MoveDriverLang "%SCADA_DIR%\ScadaAdmin\Lib" "%SCADA_DIR%\ScadaAdmin\Lang"
IF ERRORLEVEL 1 EXIT /B 1
CALL :CleanPluginFolder "%SCADA_DIR%\ScadaAdmin\Lib"
IF ERRORLEVEL 1 EXIT /B 1
CALL :MoveRootRuntimeFolder "%SCADA_DIR%\ScadaAdmin\Lib" "DrvDbDataTransfer.View"
IF ERRORLEVEL 1 EXIT /B 1

IF "%BUILD_WINFORM%"=="1" (
    ECHO COMPILE WINFORM for %VIEW_RUNTIME%...
    IF /I "%VIEW_RUNTIME%"=="anycpu" (
        DOTNET publish ".\DrvDbDataTransfer.Winform\DrvDbDataTransfer.Winform.csproj" -c Release --framework net8.0-windows --self-contained false --output "%RELEASE_DIR%\App"
    ) ELSE (
        DOTNET publish ".\DrvDbDataTransfer.Winform\DrvDbDataTransfer.Winform.csproj" -c Release --framework net8.0-windows --runtime %VIEW_RUNTIME% --self-contained false --output "%RELEASE_DIR%\App"
    )
    IF ERRORLEVEL 1 EXIT /B 1
) ELSE (
    ECHO NOTE: WinForms is not supported on Linux, skipping WinForm compilation.
)

CALL :AssertScadaStructure "%SCADA_DIR%"
IF ERRORLEVEL 1 EXIT /B 1

EXIT /B 0

:PrepareReleasePackage
SET "PACKAGE_DIR=%~1"
SET "RELEASE_DIR=.\%PACKAGE_DIR%\Release"
SET "SCADA_DIR=%RELEASE_DIR%\SCADA"

IF EXIST "%RELEASE_DIR%" RMDIR /S /Q "%RELEASE_DIR%"
MKDIR "%SCADA_DIR%\ScadaAdmin\Lang"
IF ERRORLEVEL 1 EXIT /B 1
MKDIR "%SCADA_DIR%\ScadaAdmin\Lib"
IF ERRORLEVEL 1 EXIT /B 1
MKDIR "%SCADA_DIR%\ScadaComm\Drv"
IF ERRORLEVEL 1 EXIT /B 1
MKDIR "%RELEASE_DIR%\App"
IF ERRORLEVEL 1 EXIT /B 1
EXIT /B 0

:MoveDriverLang
SET "LIB_DIR=%~1"
SET "LANG_DIR=%~2"

IF EXIST "%LIB_DIR%\Lang\DrvDbDataTransfer.*.xml" (
    XCOPY /Y /I "%LIB_DIR%\Lang\DrvDbDataTransfer.*.xml" "%LANG_DIR%\"
    IF ERRORLEVEL 1 EXIT /B 1
)

IF EXIST "%LIB_DIR%\Lang" RMDIR /S /Q "%LIB_DIR%\Lang"
EXIT /B 0

:CleanPluginFolder
SET "TARGET_DIR=%~1"

DEL /Q "%TARGET_DIR%\Scada*.dll" 2>NUL
DEL /Q "%TARGET_DIR%\*.pdb" 2>NUL
DEL /Q "%TARGET_DIR%\*.json" 2>NUL
FOR %%D IN (de es fr it ja ko pt-BR ru zh-Hans zh-Hant) DO (
    IF EXIST "%TARGET_DIR%\%%D" RMDIR "%TARGET_DIR%\%%D" 2>NUL
)
EXIT /B 0

:MoveRootRuntimeFolder
SET "TARGET_DIR=%~1"
SET "DRIVER_SUBDIR=%~2"

IF EXIST "%TARGET_DIR%\runtimes" (
    XCOPY /E /Y /I "%TARGET_DIR%\runtimes\*.*" "%TARGET_DIR%\%DRIVER_SUBDIR%\runtimes\"
    IF ERRORLEVEL 1 EXIT /B 1
    RMDIR /S /Q "%TARGET_DIR%\runtimes"
)
EXIT /B 0

:AssertScadaStructure
SET "SCADA_DIR=%~1"

IF NOT EXIST "%SCADA_DIR%\ScadaAdmin\Lang" (
    ECHO Missing folder: %SCADA_DIR%\ScadaAdmin\Lang
    EXIT /B 1
)
IF NOT EXIST "%SCADA_DIR%\ScadaAdmin\Lib" (
    ECHO Missing folder: %SCADA_DIR%\ScadaAdmin\Lib
    EXIT /B 1
)
IF NOT EXIST "%SCADA_DIR%\ScadaComm\Drv" (
    ECHO Missing folder: %SCADA_DIR%\ScadaComm\Drv
    EXIT /B 1
)
IF NOT EXIST "%SCADA_DIR%\ScadaAdmin\Lang\DrvDbDataTransfer.*.xml" (
    ECHO Missing language files in %SCADA_DIR%\ScadaAdmin\Lang
    EXIT /B 1
)
IF NOT EXIST "%SCADA_DIR%\ScadaAdmin\Lib\DrvDbDataTransfer.View.dll" (
    ECHO Missing View DLL in %SCADA_DIR%\ScadaAdmin\Lib
    EXIT /B 1
)
IF NOT EXIST "%SCADA_DIR%\ScadaAdmin\Lib\DrvDbDataTransfer.View" (
    ECHO Missing View dependency folder in %SCADA_DIR%\ScadaAdmin\Lib
    EXIT /B 1
)
IF NOT EXIST "%SCADA_DIR%\ScadaComm\Drv\DrvDbDataTransfer.Logic.dll" (
    ECHO Missing Logic DLL in %SCADA_DIR%\ScadaComm\Drv
    EXIT /B 1
)
IF NOT EXIST "%SCADA_DIR%\ScadaComm\Drv\DrvDbDataTransfer.Logic" (
    ECHO Missing Logic dependency folder in %SCADA_DIR%\ScadaComm\Drv
    EXIT /B 1
)

EXIT /B 0
