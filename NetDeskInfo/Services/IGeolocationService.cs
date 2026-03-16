using NetDeskInfo.Models;

namespace NetDeskInfo.Services;

public interface IGeolocationService
{
    Task<LocationInfoModel> GetLocationAsync(CancellationToken cancellationToken);
}
