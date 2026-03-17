namespace CareerOS.Models;

public class Goal
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Period { get; set; } = "Semanal";
    public int TargetValue { get; set; }
    public int CurrentValue { get; set; }
}
