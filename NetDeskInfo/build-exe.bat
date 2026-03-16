@echo off
setlocal

REM Build and publish NetDeskInfo as Windows executable
cd /d %~dp0

echo [1/3] Restoring packages...
dotnet restore
if errorlevel 1 goto :error

echo [2/3] Building Release...
dotnet build -c Release
if errorlevel 1 goto :error

echo [3/3] Publishing Win-x64 (single-file)...
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true
if errorlevel 1 goto :error

echo.
echo DONE. EXE em:
echo %~dp0bin\Release\net8.0-windows\win-x64\publish\NetDeskInfo.exe
exit /b 0

:error
echo Falha no processo de build/publicacao.
exit /b 1
