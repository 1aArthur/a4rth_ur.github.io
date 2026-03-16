using System.Windows;
using System.Windows.Input;
using NetDeskInfo.Helpers;
using NetDeskInfo.Models;
using NetDeskInfo.Services;

namespace NetDeskInfo.ViewModels;

public sealed class MainViewModel : ObservableObject
{
    private readonly INetworkInfoService _networkInfoService;
    private readonly IGeolocationService _geolocationService;
    private readonly ISystemInfoService _systemInfoService;

    private string _statusMessage = "Pronto.";
    private string _statusColorHex = "#21C785";
    private bool _isLoading;

    public MainViewModel(INetworkInfoService networkInfoService, IGeolocationService geolocationService, ISystemInfoService systemInfoService)
    {
        _networkInfoService = networkInfoService;
        _geolocationService = geolocationService;
        _systemInfoService = systemInfoService;

        RefreshDataCommand = new AsyncRelayCommand(RefreshAsync, () => !IsLoading);
        CopyCommand = new RelayCommand(CopyValue);
    }

    public ICommand RefreshDataCommand { get; }
    public ICommand CopyCommand { get; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (!SetProperty(ref _isLoading, value))
            {
                return;
            }

            OnPropertyChanged(nameof(IsNotLoading));
        }
    }

    public bool IsNotLoading => !IsLoading;

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public string StatusColorHex
    {
        get => _statusColorHex;
        set => SetProperty(ref _statusColorHex, value);
    }

    // Rede
    public string PublicIp { get; private set; } = "Não disponível";
    public string PublicIpv4 { get; private set; } = "Não disponível";
    public string PublicIpv6 { get; private set; } = "Não disponível";
    public string LocalIp { get; private set; } = "Não disponível";
    public string Netmask { get; private set; } = "Não disponível";
    public string Gateway { get; private set; } = "Não disponível";
    public string AdapterName { get; private set; } = "Não disponível";
    public string DnsServers { get; private set; } = "Não disponível";
    public string ConnectionStatus { get; private set; } = "Desconhecido";

    // Localização
    public string Country { get; private set; } = "Não disponível";
    public string Region { get; private set; } = "Não disponível";
    public string City { get; private set; } = "Não disponível";
    public string PostalCode { get; private set; } = "Não disponível";
    public string Latitude { get; private set; } = "Não disponível";
    public string Longitude { get; private set; } = "Não disponível";
    public string Timezone { get; private set; } = "Não disponível";
    public string Isp { get; private set; } = "Não disponível";
    public string ApproximationNote { get; private set; } = "A localização por IP é aproximada.";
    public string LocationSummary => $"{City}, {Region}, {Country} ({Latitude}, {Longitude})";

    // Sistema
    public string OperatingSystem { get; private set; } = "Não disponível";
    public string OsVersion { get; private set; } = "Não disponível";
    public string Build { get; private set; } = "Não disponível";
    public string Architecture { get; private set; } = "Não disponível";
    public string MachineName { get; private set; } = "Não disponível";
    public string UserName { get; private set; } = "Não disponível";
    public string Uptime { get; private set; } = "Não disponível";

    // Dispositivo
    public string DeviceModel { get; private set; } = "Não disponível";
    public string Processor { get; private set; } = "Não disponível";
    public string TotalRam { get; private set; } = "Não disponível";
    public string UsedRam { get; private set; } = "Não disponível";
    public string CoresThreads { get; private set; } = "Não disponível";
    public string HostName { get; private set; } = "Não disponível";

    public async Task RefreshAsync()
    {
        IsLoading = true;
        StatusMessage = "Carregando dados do sistema e da rede...";
        StatusColorHex = "#36C4FF";

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(12));

        var networkTask = SafeLoadAsync(() => _networkInfoService.GetNetworkInfoAsync(cts.Token));
        var locationTask = SafeLoadAsync(() => _geolocationService.GetLocationAsync(cts.Token));
        var systemTask = SafeLoadAsync(() => _systemInfoService.GetSystemInfoAsync(cts.Token));
        var deviceTask = SafeLoadAsync(() => _systemInfoService.GetDeviceInfoAsync(cts.Token));

        await Task.WhenAll(networkTask, locationTask, systemTask, deviceTask);

        var warnings = 0;

        if (networkTask.Result is { } network)
        {
            PublicIp = network.PublicIp;
            PublicIpv4 = network.PublicIpv4;
            PublicIpv6 = network.PublicIpv6;
            LocalIp = network.LocalIp;
            Netmask = network.Netmask;
            Gateway = network.Gateway;
            AdapterName = network.AdapterName;
            DnsServers = network.DnsServers;
            ConnectionStatus = network.IsOnline ? "Online" : "Offline";
        }
        else
        {
            warnings++;
        }

        if (locationTask.Result is { } location)
        {
            Country = location.Country;
            Region = location.Region;
            City = location.City;
            PostalCode = location.PostalCode;
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            Timezone = location.Timezone;
            Isp = location.Isp;
            ApproximationNote = location.ApproximationNote;
        }
        else
        {
            warnings++;
        }

        if (systemTask.Result is { } system)
        {
            OperatingSystem = system.OperatingSystem;
            OsVersion = system.OsVersion;
            Build = system.Build;
            Architecture = system.Architecture;
            MachineName = system.MachineName;
            UserName = system.UserName;
            Uptime = system.Uptime;
        }
        else
        {
            warnings++;
        }

        if (deviceTask.Result is { } device)
        {
            DeviceModel = device.DeviceModel;
            Processor = device.Processor;
            TotalRam = device.TotalRam;
            UsedRam = device.UsedRam;
            CoresThreads = device.CoresThreads;
            HostName = device.HostName;
        }
        else
        {
            warnings++;
        }

        NotifyAllDataProperties();

        if (warnings == 0)
        {
            StatusMessage = "Dados atualizados com sucesso.";
            StatusColorHex = "#21C785";
        }
        else if (warnings < 4)
        {
            StatusMessage = "Dados atualizados parcialmente. Alguns itens não estavam disponíveis no momento.";
            StatusColorHex = "#FFB020";
        }
        else
        {
            StatusMessage = "Não foi possível carregar os dados agora. Tente novamente em instantes.";
            StatusColorHex = "#FF4D6D";
        }

        IsLoading = false;
    }

    private static async Task<T?> SafeLoadAsync<T>(Func<Task<T>> action) where T : class
    {
        try
        {
            return await action();
        }
        catch
        {
            return null;
        }
    }

    private void CopyValue(object? parameter)
    {
        var content = parameter?.ToString();
        if (string.IsNullOrWhiteSpace(content) || content == "Não disponível")
        {
            StatusMessage = "Nada para copiar.";
            StatusColorHex = "#FFB020";
            return;
        }

        try
        {
            Clipboard.SetText(content);
            StatusMessage = "Conteúdo copiado para a área de transferência.";
            StatusColorHex = "#21C785";
        }
        catch
        {
            StatusMessage = "Não foi possível copiar no momento.";
            StatusColorHex = "#FF4D6D";
        }
    }

    private void NotifyAllDataProperties()
    {
        OnPropertyChanged(nameof(PublicIp));
        OnPropertyChanged(nameof(PublicIpv4));
        OnPropertyChanged(nameof(PublicIpv6));
        OnPropertyChanged(nameof(LocalIp));
        OnPropertyChanged(nameof(Netmask));
        OnPropertyChanged(nameof(Gateway));
        OnPropertyChanged(nameof(AdapterName));
        OnPropertyChanged(nameof(DnsServers));
        OnPropertyChanged(nameof(ConnectionStatus));
        OnPropertyChanged(nameof(Country));
        OnPropertyChanged(nameof(Region));
        OnPropertyChanged(nameof(City));
        OnPropertyChanged(nameof(PostalCode));
        OnPropertyChanged(nameof(Latitude));
        OnPropertyChanged(nameof(Longitude));
        OnPropertyChanged(nameof(Timezone));
        OnPropertyChanged(nameof(Isp));
        OnPropertyChanged(nameof(ApproximationNote));
        OnPropertyChanged(nameof(LocationSummary));
        OnPropertyChanged(nameof(OperatingSystem));
        OnPropertyChanged(nameof(OsVersion));
        OnPropertyChanged(nameof(Build));
        OnPropertyChanged(nameof(Architecture));
        OnPropertyChanged(nameof(MachineName));
        OnPropertyChanged(nameof(UserName));
        OnPropertyChanged(nameof(Uptime));
        OnPropertyChanged(nameof(DeviceModel));
        OnPropertyChanged(nameof(Processor));
        OnPropertyChanged(nameof(TotalRam));
        OnPropertyChanged(nameof(UsedRam));
        OnPropertyChanged(nameof(CoresThreads));
        OnPropertyChanged(nameof(HostName));
    }
}
