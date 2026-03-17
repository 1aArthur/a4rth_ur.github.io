param([string]$Runtime = "win-x64")
$ErrorActionPreference = "Stop"

dotnet restore ..\CareerOS.csproj
dotnet build ..\CareerOS.csproj -c Release
dotnet publish ..\CareerOS.csproj -c Release -r $Runtime --self-contained true -o ..\bin\publish\$Runtime
Write-Host "Publicado em ..\\bin\\publish\\$Runtime"
