using NetDeskInfo.Models;

namespace NetDeskInfo.Services;

public interface INetworkInfoService
{
    Task<NetworkInfoModel> GetNetworkInfoAsync(CancellationToken cancellationToken);
}
