using CareerOS.Helpers;
using CareerOS.Models;
using CareerOS.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CareerOS.ViewModels;

public partial class GoalsViewModel : ViewModelBase
{
    private readonly GoalService _goalService;
    public ObservableCollection<Goal> Goals { get; } = new();

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private int targetValue = 10;

    public GoalsViewModel(GoalService goalService) => _goalService = goalService;

    [RelayCommand]
    public async Task LoadAsync()
    {
        Goals.Clear();
        foreach (var goal in await _goalService.GetGoalsAsync())
        {
            Goals.Add(goal);
        }
    }

    [RelayCommand]
    public async Task AddAsync()
    {
        await _goalService.AddGoalAsync(new Goal { Name = Name, Period = "Semanal", TargetValue = TargetValue, CurrentValue = 0 });
        await LoadAsync();
    }
}
