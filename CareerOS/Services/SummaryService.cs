using CareerOS.Models;

namespace CareerOS.Services;

public interface ISummaryService
{
    string Generate(IEnumerable<Subject> subjects, IEnumerable<PracticeRecord> practices, IEnumerable<GitHubRepo> repos);
}

public class SummaryService : ISummaryService
{
    public string Generate(IEnumerable<Subject> subjects, IEnumerable<PracticeRecord> practices, IEnumerable<GitHubRepo> repos)
    {
        var materias = string.Join(", ", subjects.Select(x => x.Nome).Distinct().Take(4));
        var tecs = string.Join(", ", practices.Select(x => x.Linguagem).Distinct().Take(5));
        var exercicios = practices.Count(x => x.Status == ExerciseStatus.Resolvido);
        var projetos = repos.Count();

        return $"Estudante de Ciência da Computação com foco em {materias}, prática em {tecs}, {exercicios} exercícios resolvidos e {projetos} projetos no GitHub.";
    }
}
