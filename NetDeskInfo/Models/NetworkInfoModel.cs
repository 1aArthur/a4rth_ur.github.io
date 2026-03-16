namespace NetDeskInfo.Models;

public sealed class NetworkInfoModel
{
    public string PublicIp { get; init; } = "Não disponível";
    public string PublicIpv4 { get; init; } = "Não disponível";
    public string PublicIpv6 { get; init; } = "Não disponível";
    public string LocalIp { get; init; } = "Não disponível";
    public string Netmask { get; init; } = "Não disponível";
    public string Gateway { get; init; } = "Não disponível";
    public string AdapterName { get; init; } = "Não disponível";
    public string DnsServers { get; init; } = "Não disponível";
    public bool IsOnline { get; init; }
}
