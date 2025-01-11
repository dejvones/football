namespace Football.App.ViewModels;

public class MatchViewModel
{
    public string? Id { get; set; }
    public string? Team1Player1Id { get; set; }
    public string? Team1Player1Name { get; set; }
    public string? Team1Player2Id { get; set; }
    public string? Team1Player2Name { get; set; }
    public string? Team2Player1Id { get; set; }
    public string? Team2Player1Name { get; set; }
    public string? Team2Player2Id { get; set; }
    public string? Team2Player2Name { get; set; }
    public int Score1 { get; set; }
    public int Score2 { get; set; }
    public DateTime Date { get; set; }
    public List<PlayerViewModel> AllPlayers { get; set; } = [];
}
