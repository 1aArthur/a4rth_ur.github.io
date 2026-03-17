using CareerOS.Models;

namespace CareerOS.ViewModels;

public class DashboardViewModel
{
    public int HorasSemana { get; set; }
    public int SessoesConcluidas { get; set; }
    public int MateriasAtivas { get; set; }
    public int Pendencias { get; set; }
    public int ExerciciosResolvidos { get; set; }
    public int StreakDias { get; set; }
    public int MetaPercentual { get; set; }
    public List<GitHubRepo> UltimosRepos { get; set; } = [];
    public Dictionary<string, int> ProgressoPorMateria { get; set; } = [];
}

public class GitHubPageViewModel
{
    public string Username { get; set; } = string.Empty;
    public List<GitHubRepo> Repos { get; set; } = [];
    public string Mensagem { get; set; } = string.Empty;
}

public class SummaryPageViewModel
{
    public string Username { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
}
