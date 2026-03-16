using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using NetDeskInfo.Models;

namespace NetDeskInfo.Services;

public sealed class NetworkInfoService(HttpClient httpClient) : INetworkInfoService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<NetworkInfoModel> GetNetworkInfoAsync(CancellationToken cancellationToken)
    {
        var adapter = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(n => n.OperationalStatus == OperationalStatus.Up
                        && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .OrderByDescending(n => n.Speed)
            .FirstOrDefault();

        var localIp = "Não disponível";
        var netmask = "Não disponível";
        var gateway = "Não disponível";
        var dnsServers = "Não disponível";

        if (adapter is not null)
        {
            var properties = adapter.GetIPProperties();
            var unicast = properties.UnicastAddresses
                .FirstOrDefault(a => a.Address.AddressFamily == AddressFamily.InterNetwork);

            localIp = unicast?.Address?.ToString() ?? "Não disponível";
            netmask = unicast?.IPv4Mask?.ToString() ?? "Não disponível";
            gateway = properties.GatewayAddresses
                .FirstOrDefault(g => g.Address.AddressFamily == AddressFamily.InterNetwork)?.Address.ToString()
                ?? "Não disponível";
            dnsServers = string.Join(", ", properties.DnsAddresses.Select(d => d.ToString()).DefaultIfEmpty("Não disponível"));
        }

        var publicIpTask = TryGetAsync("https://api.ipify.org", cancellationToken);
        var ipv4Task = TryGetAsync("https://api4.ipify.org", cancellationToken);
        var ipv6Task = TryGetAsync("https://api6.ipify.org", cancellationToken);

        await Task.WhenAll(publicIpTask, ipv4Task, ipv6Task);

        return new NetworkInfoModel
        {
            PublicIp = publicIpTask.Result,
            PublicIpv4 = ipv4Task.Result,
            PublicIpv6 = ipv6Task.Result,
            LocalIp = localIp,
            Netmask = netmask,
            Gateway = gateway,
            AdapterName = adapter?.Name ?? "Não disponível",
            DnsServers = dnsServers,
            IsOnline = NetworkInterface.GetIsNetworkAvailable()
        };
    }

    private async Task<string> TryGetAsync(string url, CancellationToken cancellationToken)
    {
        try
        {
            using var response = await _httpClient.GetAsync(url, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return "Não disponível";
            }

            var value = await response.Content.ReadAsStringAsync(cancellationToken);
            return string.IsNullOrWhiteSpace(value) ? "Não disponível" : value.Trim();
        }
        catch (HttpRequestException)
        {
            return "Não disponível";
        }
        catch (TaskCanceledException)
        {
            return "Não disponível";
        }
    }
}
