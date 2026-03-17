using CareerOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CareerOS.Views;

public sealed partial class PomodoroPage : Page
{
    public PomodoroPage()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<PomodoroViewModel>();
    }
}
