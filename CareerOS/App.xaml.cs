using CareerOS.Services;
using CareerOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace CareerOS;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = default!;

    public App()
    {
        InitializeComponent();
        Services = ConfigureServices();
    }

    protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        var db = Services.GetRequiredService<DatabaseService>();
        await db.InitializeAsync();

        var window = Services.GetRequiredService<MainWindow>();
        window.Activate();
    }

    private static IServiceProvider ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<MainWindow>();
        serviceCollection.AddSingleton<DatabaseService>();
        serviceCollection.AddSingleton<StudyService>();
        serviceCollection.AddSingleton<PracticeService>();
        serviceCollection.AddSingleton<GoalService>();
        serviceCollection.AddSingleton<GitHubService>();
        serviceCollection.AddSingleton<SummaryService>();
        serviceCollection.AddSingleton<PomodoroService>();
        serviceCollection.AddSingleton<NavigationService>();

        serviceCollection.AddSingleton<MainViewModel>();
        serviceCollection.AddSingleton<DashboardViewModel>();
        serviceCollection.AddSingleton<StudiesViewModel>();
        serviceCollection.AddSingleton<SessionsViewModel>();
        serviceCollection.AddSingleton<PomodoroViewModel>();
        serviceCollection.AddSingleton<PracticeViewModel>();
        serviceCollection.AddSingleton<GoalsViewModel>();
        serviceCollection.AddSingleton<GitHubViewModel>();
        serviceCollection.AddSingleton<ProfessionalSummaryViewModel>();
        serviceCollection.AddSingleton<SettingsViewModel>();

        return serviceCollection.BuildServiceProvider();
    }
}
