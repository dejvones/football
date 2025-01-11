namespace Football.App.Models;

public class MatchViewModel
{
    public string? Id { get; set; }
    public string? Player1Id { get; set; }
    public PlayerViewModel? Player1 { get; set; }
    public string? Player2Id { get; set; }
    public PlayerViewModel? Player2 { get; set; }
    public string? Player3Id { get; set; }
    public PlayerViewModel? Player3 { get; set; }
    public string? Player4Id { get; set; }
    public PlayerViewModel? Player4 { get; set; }
    public int Score1 { get; set; }
    public int Score2 { get; set; }
    public DateTime Date { get; set; }
    public List<PlayerViewModel>? AllPlayers { get; set; }
}
