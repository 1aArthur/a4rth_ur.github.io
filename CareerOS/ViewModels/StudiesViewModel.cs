using CareerOS.Helpers;
using CareerOS.Models;
using CareerOS.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CareerOS.ViewModels;

public partial class StudiesViewModel : ViewModelBase
{
    private readonly StudyService _studyService;
    public ObservableCollection<Subject> Subjects { get; } = new();

    [ObservableProperty] private string name = string.Empty;
    [ObservableProperty] private string description = string.Empty;

    public StudiesViewModel(StudyService studyService) => _studyService = studyService;

    [RelayCommand]
    public async Task LoadAsync()
    {
        Subjects.Clear();
        foreach (var subject in await _studyService.GetSubjectsAsync())
        {
            Subjects.Add(subject);
        }
    }

    [RelayCommand]
    public async Task AddSubjectAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            StatusMessage = "Nome da matéria é obrigatório.";
            return;
        }

        await _studyService.AddSubjectAsync(new Subject { Name = Name, Description = Description, MasteryLevel = 0, Priority = PriorityLevel.Medium, Status = StudyStatus.NotStarted });
        Name = string.Empty;
        Description = string.Empty;
        await LoadAsync();
        StatusMessage = "Matéria adicionada.";
    }
}
