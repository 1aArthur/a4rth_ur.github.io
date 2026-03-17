using CareerOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CareerOS.Views;

public sealed partial class StudiesPage : Page
{
    public StudiesPage()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<StudiesViewModel>();
        Loaded += async (_, _) => await ((StudiesViewModel)DataContext).LoadAsync();
    }
}
