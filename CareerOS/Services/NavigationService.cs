using CareerOS.Views;
using Microsoft.UI.Xaml.Controls;

namespace CareerOS.Services;

public class NavigationService
{
    private Frame? _frame;

    public void Initialize(Frame frame) => _frame = frame;

    public void Navigate(string key)
    {
        if (_frame is null) return;

        var page = key switch
        {
            "Dashboard" => typeof(DashboardPage),
            "Studies" => typeof(StudiesPage),
            "Sessions" => typeof(SessionsPage),
            "Pomodoro" => typeof(PomodoroPage),
            "Practice" => typeof(PracticePage),
            "Goals" => typeof(GoalsPage),
            "GitHub" => typeof(GitHubPage),
            "ProfessionalSummary" => typeof(ProfessionalSummaryPage),
            "Settings" => typeof(SettingsPage),
            _ => typeof(DashboardPage)
        };

        _frame.Navigate(page);
    }
}
