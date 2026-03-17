using CareerOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CareerOS.Views;

public sealed partial class ProfessionalSummaryPage : Page
{
    public ProfessionalSummaryPage()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<ProfessionalSummaryViewModel>();
    }
}
