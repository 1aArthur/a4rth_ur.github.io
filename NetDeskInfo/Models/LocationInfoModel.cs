namespace NetDeskInfo.Models;

public sealed class LocationInfoModel
{
    public string Country { get; init; } = "Não disponível";
    public string Region { get; init; } = "Não disponível";
    public string City { get; init; } = "Não disponível";
    public string PostalCode { get; init; } = "Não disponível";
    public string Latitude { get; init; } = "Não disponível";
    public string Longitude { get; init; } = "Não disponível";
    public string Timezone { get; init; } = "Não disponível";
    public string Isp { get; init; } = "Não disponível";
    public string ApproximationNote { get; init; } = "A localização por IP é aproximada.";
}
