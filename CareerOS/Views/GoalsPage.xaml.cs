using CareerOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CareerOS.Views;

public sealed partial class GoalsPage : Page
{
    public GoalsPage()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<GoalsViewModel>();
        Loaded += async (_, _) => await ((GoalsViewModel)DataContext).LoadAsync();
    }
}
