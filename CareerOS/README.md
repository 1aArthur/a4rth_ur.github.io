# Career OS (WinUI 3 + .NET 8)

Aplicativo desktop para estudantes de Ciência da Computação organizarem estudos, prática técnica e evolução profissional.

## Requisitos
- Windows 10/11
- Visual Studio 2022 17.8+
- Workload: **Desenvolvimento de aplicativos para desktop com C#**
- Windows App SDK runtime
- SDK do .NET 8

## Como executar no Visual Studio
1. Abra `CareerOS.csproj` no Visual Studio.
2. Restaure os pacotes NuGet.
3. Selecione `x64` e execute (F5).

## Gerar o `.exe` (publicação local)
No Windows PowerShell:

```powershell
cd CareerOS\scripts
.\build-exe.ps1 win-x64
```

Ou no Prompt:

```bat
cd CareerOS\scripts
build-exe.bat win-x64
```

Saída:
- `CareerOS\bin\publish\win-x64\CareerOS.exe`

## Gerar build automaticamente (GitHub Actions)
O workflow `CareerOS/.github/workflows/windows-build.yml` compila e publica o app em runner Windows e gera artifact baixável com o executável.

## Módulos
- Dashboard
- Estudos
- Sessões
- Pomodoro
- Prática
- Metas
- GitHub
- Resumo Profissional
- Configurações

## Persistência
SQLite local no caminho:
`%LocalAppData%\CareerOS\careeros.db`

## Observações
- Se a API do GitHub falhar, o app continua funcionando e mostra feedback amigável.
- O resumo profissional pode ser editado, copiado e exportado para TXT.
