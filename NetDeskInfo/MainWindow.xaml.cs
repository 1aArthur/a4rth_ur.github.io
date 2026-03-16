using System.Net.Http;
using System.Windows;
using NetDeskInfo.Services;
using NetDeskInfo.ViewModels;

namespace NetDeskInfo;

public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        var sharedHttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(8)
        };

        _viewModel = new MainViewModel(
            new NetworkInfoService(sharedHttpClient),
            new IpWhoIsGeolocationService(sharedHttpClient),
            new SystemInfoService());

        DataContext = _viewModel;

        Loaded += async (_, _) => await _viewModel.RefreshAsync();
    }
}
