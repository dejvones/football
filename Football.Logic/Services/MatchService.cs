using Football.Data.Models;
using Football.Data.Repository;

namespace Football.Logic.Services;

public class MatchService(IMatchRepository matchRepository, ILeagueRepository leagueRepository, IPlayerRepository playerRepository) : IMatchService
{
    private readonly IMatchRepository _matchRepository = matchRepository;
    private readonly ILeagueRepository _leagueRepository = leagueRepository;
    private readonly IPlayerRepository _playerRepository = playerRepository;

    public async Task<IEnumerable<MatchModel>> GetLastMatches()
    {
        var matches = await _matchRepository.GetAllAsync(6);
        return matches.OrderByDescending(x => x.Date);
    }

    public async Task<IEnumerable<MatchModel>> GetAllMatches()
    {
        var matches = await _matchRepository.GetAllAsync(200);
        return matches.OrderByDescending(x => x.Date);
    }

    public async Task CreateMatchAsync(MatchModel model)
    {
        var league = await _leagueRepository.GetActiveAsync();
        model = model with { LeagueId = league.Id };
        await _matchRepository.AddMatchAsync(model);
        await UpdateRanking(model);
    }

    private async Task UpdateRanking(MatchModel model)
    {
        PlayerModel? player2 = null;
        PlayerModel? player4 = null;
        if (model.Team1.Player2Id is not null && model.Team2.Player2Id is not null)
        {
            player2 = await _playerRepository.GetByIdAsync(model.Team1.Player2Id);
            player4 = await _playerRepository.GetByIdAsync(model.Team2.Player2Id);
        }
        var player1 = await _playerRepository.GetByIdAsync(model.Team1.Player1Id);
        var player3 = await _playerRepository.GetByIdAsync(model.Team2.Player1Id);

        var team1Win = model.Team1.Score > model.Team2.Score;
        var (p1, p2) = GetPoints(team1Win);

        await UpdatePlayer(player1, p1, team1Win);
        await UpdatePlayer(player2, p1, team1Win);
        await UpdatePlayer(player3, p2, !team1Win);
        await UpdatePlayer(player4, p2, !team1Win);
    }

    private async Task UpdatePlayer(PlayerModel? player, int points, bool isWin)
    {
        if (player is null)
        {
            return;
        }

        var updatedPoints = UpdatePoints(player, points);
        var updatedWins = UpdateWins(player, isWin);

        await _playerRepository.UpdatePlayerAsync(updatedWins);
    }

    private static PlayerModel UpdatePoints(PlayerModel player, int points)
    {
        var newCurrentPoints = Math.Max(player.Stats.CurrentPoints + points, 0);
        var newAllPoints = Math.Max(player.Stats.AllPoints + points, 0);
        var currentForm = player.Stats.Form.ToList();
        return player with
        {
            Stats = player.Stats with
            {
                CurrentPoints = newCurrentPoints,
                CurrentMatches = player.Stats.CurrentMatches + 1,
                AllPoints = newAllPoints,
                AllMatches = player.Stats.AllMatches + 1
            }
        };
    }

    private static PlayerModel UpdateWins(PlayerModel player, bool isWin)
    {
        var form = player.Stats.Form.ToList();
        form.Insert(0, isWin ? Result.Win : Result.Lose);
        form.RemoveAt(form.Count - 1);
        return player with
        {
            Stats = player.Stats with
            {
                Form = form.ToArray(),
                Wins = player.Stats.Wins + (isWin ? 1 : 0)
            }
        };
    }

    private static (int points1, int points2) GetPoints(bool team1Win)
    {
        const int BaseWinPoints = 100;
        const int BaseLosePoints = -50;

        return team1Win ? (BaseWinPoints, BaseLosePoints) : (BaseLosePoints, BaseWinPoints);
    }
}
