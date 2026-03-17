@echo off
setlocal

set RUNTIME=win-x64
if not "%~1"=="" set RUNTIME=%~1

echo [CareerOS] Restaurando pacotes...
dotnet restore ..\CareerOS.csproj || goto :error

echo [CareerOS] Build Release...
dotnet build ..\CareerOS.csproj -c Release || goto :error

echo [CareerOS] Publicando EXE (%RUNTIME%)...
dotnet publish ..\CareerOS.csproj -c Release -r %RUNTIME% --self-contained true /p:WindowsAppSDKSelfContained=true /p:PublishSingleFile=false -o ..\bin\publish\%RUNTIME% || goto :error

echo Concluido. Saida em ..\bin\publish\%RUNTIME%
exit /b 0

:error
echo Falha durante build/publicacao.
exit /b 1
