using CareerOS.Helpers;
using CareerOS.Models;
using CareerOS.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CareerOS.ViewModels;

public partial class PracticeViewModel : ViewModelBase
{
    private readonly PracticeService _practiceService;
    public ObservableCollection<PracticeRecord> Records { get; } = new();

    [ObservableProperty] private string title = string.Empty;
    [ObservableProperty] private string theme = string.Empty;

    public PracticeViewModel(PracticeService practiceService) => _practiceService = practiceService;

    [RelayCommand]
    public async Task LoadAsync()
    {
        Records.Clear();
        foreach (var record in await _practiceService.GetRecordsAsync())
        {
            Records.Add(record);
        }
    }

    [RelayCommand]
    public async Task AddAsync()
    {
        await _practiceService.AddRecordAsync(new PracticeRecord
        {
            Title = Title,
            Theme = Theme,
            Platform = "LeetCode",
            Difficulty = "Médio",
            Language = "C#",
            TimeSpentMinutes = 30,
            Status = ExerciseStatus.Solved
        });
        await LoadAsync();
    }
}
