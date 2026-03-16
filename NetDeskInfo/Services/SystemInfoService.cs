using System.Runtime.InteropServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using NetDeskInfo.Models;

namespace NetDeskInfo.Services;

public sealed class SystemInfoService : ISystemInfoService
{
    public Task<SystemInfoModel> GetSystemInfoAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var version = Environment.OSVersion.Version;
        var uptime = TimeSpan.FromMilliseconds(Environment.TickCount64);

        var model = new SystemInfoModel
        {
            OperatingSystem = RuntimeInformation.OSDescription,
            OsVersion = version.ToString(),
            Build = version.Build > 0 ? version.Build.ToString() : "Não disponível",
            Architecture = RuntimeInformation.OSArchitecture.ToString(),
            MachineName = Environment.MachineName,
            UserName = Environment.UserName,
            Uptime = $"{(int)uptime.TotalDays}d {uptime.Hours}h {uptime.Minutes}m"
        };

        return Task.FromResult(model);
    }

    public Task<DeviceInfoModel> GetDeviceInfoAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var computerInfo = new ComputerInfo();
        var totalRamBytes = computerInfo.TotalPhysicalMemory;
        var availableRamBytes = computerInfo.AvailablePhysicalMemory;
        var usedRamBytes = totalRamBytes - availableRamBytes;

        var model = new DeviceInfoModel
        {
            DeviceModel = ReadDeviceModel(),
            Processor = GetProcessorName(),
            TotalRam = FormatBytes(totalRamBytes),
            UsedRam = FormatBytes(usedRamBytes),
            CoresThreads = $"Lógicos: {Environment.ProcessorCount}",
            HostName = System.Net.Dns.GetHostName()
        };

        return Task.FromResult(model);
    }

    private static string GetProcessorName()
    {
        // Busca o nome real da CPU no registro do Windows.
        const string keyPath = @"HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\0";
        return Registry.GetValue(keyPath, "ProcessorNameString", "Não disponível")?.ToString() ?? "Não disponível";
    }

    private static string ReadDeviceModel()
    {
        const string basePath = @"HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\BIOS";
        var manufacturer = Registry.GetValue(basePath, "SystemManufacturer", null)?.ToString();
        var product = Registry.GetValue(basePath, "SystemProductName", null)?.ToString();

        var result = $"{manufacturer} {product}".Trim();
        return string.IsNullOrWhiteSpace(result) ? "Não disponível" : result;
    }

    private static string FormatBytes(ulong bytes)
    {
        var value = bytes / 1024d / 1024d / 1024d;
        return $"{value:F2} GB";
    }
}
