namespace CareerOS.Models;

public enum StudyStatus { NaoIniciado, EmProgresso, Concluido }
public enum PriorityLevel { Baixa, Media, Alta }
public enum SessionType { Leitura, Exercicios, Revisao, Projeto }
public enum ExerciseStatus { Pendente, Resolvido, Revisar }

public class Subject
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Assunto { get; set; } = string.Empty;
    public string Subtopicos { get; set; } = string.Empty;
    public int NivelDominio { get; set; }
    public PriorityLevel Prioridade { get; set; } = PriorityLevel.Media;
    public DateTime Prazo { get; set; } = DateTime.Today.AddDays(30);
    public StudyStatus Status { get; set; } = StudyStatus.NaoIniciado;
}

public class StudySession
{
    public int Id { get; set; }
    public int? SubjectId { get; set; }
    public string Topico { get; set; } = string.Empty;
    public DateTime Data { get; set; } = DateTime.Now;
    public int DuracaoMinutos { get; set; }
    public SessionType TipoSessao { get; set; }
    public string Observacoes { get; set; } = string.Empty;
    public int NotaProdutividade { get; set; }
}

public class PracticeRecord
{
    public int Id { get; set; }
    public string Plataforma { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Tema { get; set; } = string.Empty;
    public string Dificuldade { get; set; } = "Médio";
    public string Linguagem { get; set; } = "C#";
    public int TempoMinutos { get; set; }
    public ExerciseStatus Status { get; set; } = ExerciseStatus.Resolvido;
    public string Observacoes { get; set; } = string.Empty;
    public string? Link { get; set; }
}

public class Goal
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Periodo { get; set; } = "Semanal";
    public int Meta { get; set; }
    public int Atual { get; set; }
}

public class AppSetting
{
    public int Id { get; set; }
    public string Chave { get; set; } = string.Empty;
    public string Valor { get; set; } = string.Empty;
}

public class GitHubRepo
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Language { get; set; } = "N/A";
    public int Stars { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Url { get; set; } = string.Empty;
}
