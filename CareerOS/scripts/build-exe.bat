@echo off
setlocal
set RUNTIME=win-x64
if not "%~1"=="" set RUNTIME=%~1

dotnet restore ..\CareerOS.csproj || goto :error
dotnet build ..\CareerOS.csproj -c Release || goto :error
dotnet publish ..\CareerOS.csproj -c Release -r %RUNTIME% --self-contained true -o ..\bin\publish\%RUNTIME% || goto :error

echo Publicado em ..\bin\publish\%RUNTIME%
exit /b 0
:error
exit /b 1
