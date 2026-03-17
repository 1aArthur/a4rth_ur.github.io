namespace CareerOS.Models;

public class StudySession
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public string Topic { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public int DurationMinutes { get; set; }
    public SessionType SessionType { get; set; }
    public string Notes { get; set; } = string.Empty;
    public int ProductivityScore { get; set; }
}
