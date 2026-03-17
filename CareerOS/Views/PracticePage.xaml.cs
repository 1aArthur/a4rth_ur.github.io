using CareerOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CareerOS.Views;

public sealed partial class PracticePage : Page
{
    public PracticePage()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<PracticeViewModel>();
        Loaded += async (_, _) => await ((PracticeViewModel)DataContext).LoadAsync();
    }
}
