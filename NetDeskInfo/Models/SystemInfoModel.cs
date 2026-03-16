namespace NetDeskInfo.Models;

public sealed class SystemInfoModel
{
    public string OperatingSystem { get; init; } = "Não disponível";
    public string OsVersion { get; init; } = "Não disponível";
    public string Build { get; init; } = "Não disponível";
    public string Architecture { get; init; } = "Não disponível";
    public string MachineName { get; init; } = "Não disponível";
    public string UserName { get; init; } = "Não disponível";
    public string Uptime { get; init; } = "Não disponível";
}
