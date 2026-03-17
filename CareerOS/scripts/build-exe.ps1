param(
    [string]$Runtime = "win-x64"
)

$ErrorActionPreference = "Stop"

Write-Host "[CareerOS] Restaurando pacotes..."
dotnet restore ..\CareerOS.csproj

Write-Host "[CareerOS] Build Release..."
dotnet build ..\CareerOS.csproj -c Release

Write-Host "[CareerOS] Publicando EXE ($Runtime)..."
dotnet publish ..\CareerOS.csproj -c Release -r $Runtime --self-contained true /p:WindowsAppSDKSelfContained=true /p:PublishSingleFile=false -o ..\bin\publish\$Runtime

Write-Host "Concluído. Pasta de saída: ..\bin\publish\$Runtime"
