using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetDeskInfo.Models;

namespace NetDeskInfo.Services;

/// <summary>
/// Implementação de geolocalização por IP usando o provedor ipwho.is.
/// Troque esta classe por outro provedor sem impactar a ViewModel.
/// </summary>
public sealed class IpWhoIsGeolocationService(HttpClient httpClient) : IGeolocationService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<LocationInfoModel> GetLocationAsync(CancellationToken cancellationToken)
    {
        try
        {
            using var response = await _httpClient.GetAsync("https://ipwho.is/", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return new LocationInfoModel();
            }

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var payload = await JsonSerializer.DeserializeAsync<IpWhoIsResponse>(stream, cancellationToken: cancellationToken);
            if (payload is null || !payload.Success)
            {
                return new LocationInfoModel();
            }

            return new LocationInfoModel
            {
                Country = payload.Country ?? "Não disponível",
                Region = payload.Region ?? "Não disponível",
                City = payload.City ?? "Não disponível",
                PostalCode = payload.Postal ?? "Não disponível",
                Latitude = payload.Latitude?.ToString("F4") ?? "Não disponível",
                Longitude = payload.Longitude?.ToString("F4") ?? "Não disponível",
                Timezone = payload.Timezone?.Id ?? "Não disponível",
                Isp = payload.Connection?.Isp ?? "Não disponível"
            };
        }
        catch (HttpRequestException)
        {
            return new LocationInfoModel();
        }
        catch (TaskCanceledException)
        {
            return new LocationInfoModel();
        }
        catch (JsonException)
        {
            return new LocationInfoModel();
        }
    }

    private sealed record IpWhoIsResponse(
        [property: JsonPropertyName("success")] bool Success,
        [property: JsonPropertyName("country")] string? Country,
        [property: JsonPropertyName("region") ] string? Region,
        [property: JsonPropertyName("city")] string? City,
        [property: JsonPropertyName("postal")] string? Postal,
        [property: JsonPropertyName("latitude")] double? Latitude,
        [property: JsonPropertyName("longitude")] double? Longitude,
        [property: JsonPropertyName("timezone")] TimezoneDto? Timezone,
        [property: JsonPropertyName("connection")] ConnectionDto? Connection);

    private sealed record TimezoneDto([property: JsonPropertyName("id")] string? Id);
    private sealed record ConnectionDto([property: JsonPropertyName("isp")] string? Isp);
}
