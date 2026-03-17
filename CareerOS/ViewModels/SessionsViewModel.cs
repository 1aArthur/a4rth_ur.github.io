using CareerOS.Helpers;
using CareerOS.Models;
using CareerOS.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CareerOS.ViewModels;

public partial class SessionsViewModel : ViewModelBase
{
    private readonly StudyService _studyService;
    public ObservableCollection<StudySession> Sessions { get; } = new();

    [ObservableProperty] private string topic = string.Empty;
    [ObservableProperty] private int durationMinutes = 25;

    public SessionsViewModel(StudyService studyService) => _studyService = studyService;

    [RelayCommand]
    public async Task LoadAsync()
    {
        Sessions.Clear();
        foreach (var session in await _studyService.GetSessionsAsync())
        {
            Sessions.Add(session);
        }
    }

    [RelayCommand]
    public async Task AddSessionAsync()
    {
        await _studyService.AddSessionAsync(new StudySession
        {
            Topic = Topic,
            DurationMinutes = DurationMinutes,
            SessionType = SessionType.Reading,
            ProductivityScore = 4,
            Date = DateTime.Now
        });

        Topic = string.Empty;
        DurationMinutes = 25;
        await LoadAsync();
    }
}
