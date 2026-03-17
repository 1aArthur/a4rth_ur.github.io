using CareerOS.Helpers;
using CareerOS.Models;
using CareerOS.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CareerOS.ViewModels;

public partial class GitHubViewModel : ViewModelBase
{
    private readonly GitHubService _gitHubService;
    public ObservableCollection<GitHubRepo> Repos { get; } = new();

    [ObservableProperty] private string username = string.Empty;

    public GitHubViewModel(GitHubService gitHubService) => _gitHubService = gitHubService;

    [RelayCommand]
    public async Task FetchAsync()
    {
        try
        {
            Repos.Clear();
            foreach (var repo in await _gitHubService.GetReposAsync(Username))
            {
                Repos.Add(repo);
            }
            StatusMessage = $"{Repos.Count} repositórios carregados.";
        }
        catch
        {
            StatusMessage = "Não foi possível carregar dados do GitHub agora.";
        }
    }
}
