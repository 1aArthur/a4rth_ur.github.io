namespace CareerOS.Models;

public class Topic
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Subtopics { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
