using CareerOS.Helpers;
using CareerOS.Models;
using CareerOS.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CareerOS.ViewModels;

public partial class DashboardViewModel : ViewModelBase
{
    private readonly StudyService _studyService;
    private readonly PracticeService _practiceService;
    private readonly GoalService _goalService;
    private readonly GitHubService _gitHubService;

    [ObservableProperty] private int weeklyHours;
    [ObservableProperty] private int sessionsCompleted;
    [ObservableProperty] private int activeSubjects;
    [ObservableProperty] private int solvedExercises;
    [ObservableProperty] private int streakDays;
    [ObservableProperty] private int weeklyGoalPercent;
    [ObservableProperty] private string githubUsername = string.Empty;

    public List<GitHubRepo> RecentRepos { get; private set; } = new();

    public DashboardViewModel(StudyService studyService, PracticeService practiceService, GoalService goalService, GitHubService gitHubService)
    {
        _studyService = studyService;
        _practiceService = practiceService;
        _goalService = goalService;
        _gitHubService = gitHubService;
    }

    public async Task LoadAsync()
    {
        var sessions = await _studyService.GetSessionsAsync();
        var subjects = await _studyService.GetSubjectsAsync();
        var practices = await _practiceService.GetRecordsAsync();
        var goals = await _goalService.GetGoalsAsync();

        var weekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
        WeeklyHours = sessions.Where(s => s.Date >= weekStart).Sum(s => s.DurationMinutes) / 60;
        SessionsCompleted = sessions.Count;
        ActiveSubjects = subjects.Count(s => s.Status == StudyStatus.InProgress);
        SolvedExercises = practices.Count(p => p.Status == ExerciseStatus.Solved);
        StreakDays = Math.Min(30, sessions.Select(s => s.Date.Date).Distinct().Count());

        var weeklyGoal = goals.FirstOrDefault(g => g.Period == "Semanal" && g.Name.Contains("Horas", StringComparison.OrdinalIgnoreCase));
        if (weeklyGoal is not null && weeklyGoal.TargetValue > 0)
        {
            WeeklyGoalPercent = Math.Min(100, (int)(100.0 * weeklyGoal.CurrentValue / weeklyGoal.TargetValue));
        }

        if (!string.IsNullOrWhiteSpace(GithubUsername))
        {
            RecentRepos = (await _gitHubService.GetReposAsync(GithubUsername)).Take(3).ToList();
        }
    }
}
