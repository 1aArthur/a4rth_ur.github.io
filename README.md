# NetDeskInfo (WPF / .NET 8)

Aplicativo desktop para Windows com interface moderna (tema escuro) para exibir informações de rede, localização aproximada por IP, sistema e dispositivo.

## Estrutura

- `NetDeskInfo/NetDeskInfo.csproj`: projeto WPF .NET 8.
- `NetDeskInfo/MainWindow.xaml`: layout principal com cards/painéis.
- `NetDeskInfo/ViewModels/MainViewModel.cs`: orquestra carregamento e comandos (MVVM).
- `NetDeskInfo/Services/*`: serviços separados para rede, sistema e geolocalização.
- `NetDeskInfo/Models/*`: modelos de dados por seção.
- `NetDeskInfo/Helpers/*`: infraestrutura de comandos e notificação de propriedades.

## Como gerar o .exe final no Windows

Pré-requisitos:
- Windows 10/11
- .NET SDK 8

### Opção rápida (recomendado)

No `Prompt de Comando`:

```bat
cd NetDeskInfo
build-exe.bat
```

Ou no PowerShell:

```powershell
cd NetDeskInfo
./build-exe.ps1
```

Saída final:

`NetDeskInfo/bin/Release/net8.0-windows/win-x64/publish/NetDeskInfo.exe`

## Comandos manuais (alternativa)

```bash
cd NetDeskInfo
dotnet restore
dotnet build -c Release
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true
```

## API de geolocalização

A implementação atual usa `https://ipwho.is/` (`IpWhoIsGeolocationService`).
A troca para outro provedor pode ser feita criando outra classe que implemente `IGeolocationService`.

## Observações de UX implementadas

- Carregamento automático ao abrir.
- Botão de atualização manual.
- Botões de cópia para IP público/IPv4/IPv6 e localização.
- Barra de status com mensagens de carregamento/sucesso/aviso.
- Tratamento de falhas parciais: se a API externa falhar, o app ainda mostra os dados locais normalmente.

## Melhorias futuras sugeridas

- Mostrar uso de RAM em tempo real com atualização periódica.
- Exibir endereço MAC e velocidade do adaptador ativo.
- Histórico de IP público com exportação em CSV.
- Tela de configurações para trocar provedor de geolocalização sem recompilar.
