using NetDeskInfo.Models;

namespace NetDeskInfo.Services;

public interface ISystemInfoService
{
    Task<SystemInfoModel> GetSystemInfoAsync(CancellationToken cancellationToken);
    Task<DeviceInfoModel> GetDeviceInfoAsync(CancellationToken cancellationToken);
}
