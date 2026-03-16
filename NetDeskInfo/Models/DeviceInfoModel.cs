namespace NetDeskInfo.Models;

public sealed class DeviceInfoModel
{
    public string DeviceModel { get; init; } = "Não disponível";
    public string Processor { get; init; } = "Não disponível";
    public string TotalRam { get; init; } = "Não disponível";
    public string UsedRam { get; init; } = "Não disponível";
    public string CoresThreads { get; init; } = "Não disponível";
    public string HostName { get; init; } = "Não disponível";
}
