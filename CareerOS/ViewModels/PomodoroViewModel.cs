using CareerOS.Helpers;
using CareerOS.Models;
using CareerOS.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CareerOS.ViewModels;

public partial class PomodoroViewModel : ViewModelBase
{
    private readonly PomodoroService _pomodoroService;
    private readonly StudyService _studyService;

    [ObservableProperty] private int focusMinutes = 25;
    [ObservableProperty] private string remaining = "25:00";

    public PomodoroViewModel(PomodoroService pomodoroService, StudyService studyService)
    {
        _pomodoroService = pomodoroService;
        _studyService = studyService;

        _pomodoroService.Tick += value => Remaining = value.ToString(@"mm\:ss");
        _pomodoroService.Completed += async () =>
        {
            StatusMessage = "Ciclo concluído! Sessão registrada.";
            await _studyService.AddSessionAsync(new StudySession
            {
                Topic = "Pomodoro Focus",
                DurationMinutes = FocusMinutes,
                SessionType = SessionType.Review,
                Date = DateTime.Now,
                ProductivityScore = 5
            });
        };
    }

    [RelayCommand] public void Start() => _pomodoroService.Start(FocusMinutes);
    [RelayCommand] public void Pause() => _pomodoroService.Pause();
    [RelayCommand] public void Reset() => _pomodoroService.Reset(FocusMinutes);
}
