namespace CareerOS.Models;

public class PracticeRecord
{
    public int Id { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Theme { get; set; } = string.Empty;
    public string Difficulty { get; set; } = "Médio";
    public string Language { get; set; } = "C#";
    public int TimeSpentMinutes { get; set; }
    public ExerciseStatus Status { get; set; } = ExerciseStatus.Solved;
    public string Notes { get; set; } = string.Empty;
    public string? Link { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}
