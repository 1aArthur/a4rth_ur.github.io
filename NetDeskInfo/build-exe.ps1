$ErrorActionPreference = 'Stop'
Set-Location $PSScriptRoot

Write-Host '[1/3] Restoring packages...'
dotnet restore

Write-Host '[2/3] Building Release...'
dotnet build -c Release

Write-Host '[3/3] Publishing Win-x64 (single-file)...'
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true

Write-Host "`nDONE. EXE em: $PSScriptRoot\bin\Release\net8.0-windows\win-x64\publish\NetDeskInfo.exe"
