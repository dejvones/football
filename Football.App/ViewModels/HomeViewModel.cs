namespace Football.App.ViewModels;

public class HomeViewModel
{
    public RankingViewModel Ranking { get; set; } = new();
    public bool ShowAllRanking { get; set; }
    public List<IGrouping<DateTime,MatchViewModel>> LatestMatches { get; set; } = [];
    public bool ShowAllMatches { get; set; }
}
