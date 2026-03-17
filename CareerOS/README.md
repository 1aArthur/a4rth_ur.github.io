# Career OS Web (ASP.NET Core MVC + .NET 8)

Site profissional para acompanhar estudos, sessões, pomodoro, prática técnica, metas, GitHub e gerar resumo profissional.

## Stack
- C#
- .NET 8
- ASP.NET Core MVC
- SQLite (EF Core)
- Arquitetura em camadas: Controllers, Services, Data, Models, ViewModels, Views

## Segurança implementada
- Anti-CSRF global (`AutoValidateAntiforgeryToken`) em POST/PUT/DELETE
- Token CSRF para chamadas JavaScript (Pomodoro)
- Sessão com cookie `HttpOnly`, `Secure`, `SameSite=Strict`
- Rate limiting global por IP
- Headers de segurança: CSP, `X-Frame-Options`, `X-Content-Type-Options`, Referrer Policy
- Validação de input com DataAnnotations + sanitização server-side
- Validação e normalização de username do GitHub

## Configurar com **seu GitHub**
No `appsettings.json`, altere:
```json
"GitHub": {
  "DefaultUsername": "SEU_USUARIO"
}
```

Você também pode salvar seu username na tela **Configurações**, com validação.

## Como rodar
```bash
cd CareerOS
dotnet restore
dotnet run
```

Acesse: `https://localhost:5001` (ou URL exibida no terminal).

## Publicar para Windows
```powershell
cd CareerOS\scripts
.\build-exe.ps1 win-x64
```

Saída em: `CareerOS\bin\publish\win-x64\`
