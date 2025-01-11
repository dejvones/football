namespace Football.App.ViewModels;

public class HomeViewModel
{
    public List<PlayerViewModel> Ranking { get; set; } = [];
    public List<MatchViewModel> LatestMatches { get; set; } = [];
}
