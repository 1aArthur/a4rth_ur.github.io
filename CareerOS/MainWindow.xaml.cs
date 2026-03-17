using CareerOS.Services;
using CareerOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CareerOS;

public sealed partial class MainWindow : Window
{
    private readonly NavigationService _navigationService;

    public MainWindow(MainViewModel viewModel, NavigationService navigationService)
    {
        InitializeComponent();
        DataContext = viewModel;
        _navigationService = navigationService;
        _navigationService.Initialize(ContentFrame);
        _navigationService.Navigate("Dashboard");
    }

    private void AppNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            _navigationService.Navigate("Settings");
            return;
        }

        if (args.SelectedItemContainer?.Tag is string tag)
        {
            _navigationService.Navigate(tag);
        }
    }
}
