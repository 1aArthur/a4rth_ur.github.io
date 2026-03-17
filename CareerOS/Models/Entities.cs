using System.ComponentModel.DataAnnotations;

namespace CareerOS.Models;

public enum StudyStatus { NaoIniciado, EmProgresso, Concluido }
public enum PriorityLevel { Baixa, Media, Alta }
public enum SessionType { Leitura, Exercicios, Revisao, Projeto }
public enum ExerciseStatus { Pendente, Resolvido, Revisar }

public class Subject
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(120)]
    public string Assunto { get; set; } = string.Empty;

    [StringLength(400)]
    public string Subtopicos { get; set; } = string.Empty;

    [Range(0, 100)]
    public int NivelDominio { get; set; }

    public PriorityLevel Prioridade { get; set; } = PriorityLevel.Media;
    public DateTime Prazo { get; set; } = DateTime.Today.AddDays(30);
    public StudyStatus Status { get; set; } = StudyStatus.NaoIniciado;
}

public class StudySession
{
    public int Id { get; set; }
    public int? SubjectId { get; set; }

    [Required, StringLength(120)]
    public string Topico { get; set; } = string.Empty;

    public DateTime Data { get; set; } = DateTime.Now;

    [Range(1, 720)]
    public int DuracaoMinutos { get; set; }

    public SessionType TipoSessao { get; set; }

    [StringLength(800)]
    public string Observacoes { get; set; } = string.Empty;

    [Range(1, 5)]
    public int NotaProdutividade { get; set; } = 4;
}

public class PracticeRecord
{
    public int Id { get; set; }

    [Required, StringLength(60)]
    public string Plataforma { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string Titulo { get; set; } = string.Empty;

    [Required, StringLength(80)]
    public string Tema { get; set; } = string.Empty;

    [StringLength(20)]
    public string Dificuldade { get; set; } = "Médio";

    [StringLength(30)]
    public string Linguagem { get; set; } = "C#";

    [Range(1, 720)]
    public int TempoMinutos { get; set; }

    public ExerciseStatus Status { get; set; } = ExerciseStatus.Resolvido;

    [StringLength(800)]
    public string Observacoes { get; set; } = string.Empty;

    [Url, StringLength(300)]
    public string? Link { get; set; }
}

public class Goal
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string Periodo { get; set; } = "Semanal";

    [Range(1, 10000)]
    public int Meta { get; set; }

    [Range(0, 10000)]
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
