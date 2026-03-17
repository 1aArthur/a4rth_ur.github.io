using CareerOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CareerOS.Views;

public sealed partial class GitHubPage : Page
{
    public GitHubPage()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<GitHubViewModel>();
    }
}
