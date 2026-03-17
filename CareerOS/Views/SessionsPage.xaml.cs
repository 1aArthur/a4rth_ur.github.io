using CareerOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CareerOS.Views;

public sealed partial class SessionsPage : Page
{
    public SessionsPage()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<SessionsViewModel>();
        Loaded += async (_, _) => await ((SessionsViewModel)DataContext).LoadAsync();
    }
}
