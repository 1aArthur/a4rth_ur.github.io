# Career OS Web (ASP.NET Core MVC + .NET 8)

Site profissional para acompanhar estudos, sessões, pomodoro, prática técnica, metas, GitHub e gerar resumo profissional.

## Stack
- C#
- .NET 8
- ASP.NET Core MVC
- SQLite (EF Core)
- Arquitetura em camadas: Controllers, Services, Data, Models, ViewModels, Views

## Funcionalidades
- Dashboard com métricas e progresso
- Estudos (matérias/tópicos)
- Sessões de estudo + Pomodoro
- Prática de programação
- Metas semanais/mensais
- Integração GitHub
- Resumo profissional com exportação TXT
- Configurações visuais (tema escuro)

## Como rodar
```bash
cd CareerOS
dotnet restore
dotnet run
```

Acesse: `http://localhost:5000` (ou porta exibida no terminal).

## Publicar para Windows
```powershell
cd CareerOS\scripts
.\build-exe.ps1 win-x64
```

Saída em: `CareerOS\bin\publish\win-x64\`
