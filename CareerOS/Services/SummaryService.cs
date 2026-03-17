using CareerOS.Models;

namespace CareerOS.Services;

public class SummaryService
{
    public string BuildProfessionalSummary(IEnumerable<Subject> subjects, IEnumerable<PracticeRecord> practices, IEnumerable<GitHubRepo> repos)
    {
        var subjectList = subjects.Select(s => s.Name).Distinct().Take(4);
        var techList = practices.Select(p => p.Language).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().Take(5);
        var solved = practices.Count(p => p.Status == ExerciseStatus.Solved);
        var projectCount = repos.Count();

        return $"Estudante de Ciência da Computação com foco em {string.Join(", ", subjectList)}. " +
               $"Possui prática em {string.Join(", ", techList)} com {solved} exercícios resolvidos e {projectCount} projetos no GitHub.";
    }
}
