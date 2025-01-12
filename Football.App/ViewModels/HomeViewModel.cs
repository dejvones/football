namespace Football.App.ViewModels;

public class HomeViewModel
{
    public RankingViewModel Ranking { get; set; } = new();
    public List<MatchViewModel> LatestMatches { get; set; } = [];
}
