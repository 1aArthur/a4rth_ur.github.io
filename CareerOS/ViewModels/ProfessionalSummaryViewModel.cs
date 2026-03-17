using CareerOS.Helpers;
using CareerOS.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Windows.ApplicationModel.DataTransfer;

namespace CareerOS.ViewModels;

public partial class ProfessionalSummaryViewModel : ViewModelBase
{
    private readonly SummaryService _summaryService;
    private readonly StudyService _studyService;
    private readonly PracticeService _practiceService;
    private readonly GitHubService _gitHubService;

    [ObservableProperty] private string text = string.Empty;
    [ObservableProperty] private string githubUsername = string.Empty;

    public ProfessionalSummaryViewModel(SummaryService summaryService, StudyService studyService, PracticeService practiceService, GitHubService gitHubService)
    {
        _summaryService = summaryService;
        _studyService = studyService;
        _practiceService = practiceService;
        _gitHubService = gitHubService;
    }

    [RelayCommand]
    public async Task GenerateAsync()
    {
        var subjects = await _studyService.GetSubjectsAsync();
        var practices = await _practiceService.GetRecordsAsync();
        var repos = await _gitHubService.GetReposAsync(GithubUsername);
        Text = _summaryService.BuildProfessionalSummary(subjects, practices, repos);
    }

    [RelayCommand]
    public void Copy()
    {
        var package = new DataPackage();
        package.SetText(Text);
        Clipboard.SetContent(package);
        StatusMessage = "Resumo copiado para a área de transferência.";
    }

    [RelayCommand]
    public async Task ExportTxtAsync()
    {
        var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "career_summary.txt");
        await File.WriteAllTextAsync(file, Text);
        StatusMessage = $"Resumo exportado para {file}";
    }
}
