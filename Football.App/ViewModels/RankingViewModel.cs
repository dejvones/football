namespace Football.App.ViewModels;

public class RankingViewModel
{
    public List<PlayerViewModel> Ranking { get; set; } = [];
    public string LeagueName { get; set; } = "";
    public DateTime LeagueStart { get; set; }
    public DateTime LeagueEnd { get; set; }
}
