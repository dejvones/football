namespace Football.App.ViewModels;

public class PlayerViewModel
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public DateTime Registred { get; set; }
    public int AllPoints { get; set; }
    public int CurrentPoints { get; set; }
    public int AllMatches { get; set; }
    public int CurrentMatches { get; set; }
    public double CurrentPointsPerMatch { get; set; }
}
