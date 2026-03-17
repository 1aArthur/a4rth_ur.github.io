namespace CareerOS.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MasteryLevel { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime Deadline { get; set; } = DateTime.Today.AddMonths(1);
    public StudyStatus Status { get; set; } = StudyStatus.NotStarted;
}
