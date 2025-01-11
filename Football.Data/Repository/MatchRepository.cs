using Football.Data.Database;
using Football.Data.Entity;
using Football.Data.Models;
using MongoDB.Driver;

namespace Football.Data.Repository;

public class MatchRepository : IMatchRepository
{
    private readonly IPlayerStatRepository _playerStatRepository;
    private readonly ILeagueRepository _leagueRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMongoCollection<Match> _collection;

    public MatchRepository(MongoDbContext context, IPlayerStatRepository playerStatRepository, ILeagueRepository leagueRepository, IPlayerRepository playerRepository)
    {
        context.CheckCollectionExists("matches");
        _collection = context.GetCollection<Match>("matches");

        _playerStatRepository = playerStatRepository;
        _leagueRepository = leagueRepository;
        _playerRepository = playerRepository;
    }

    public async Task<IEnumerable<MatchModel>> GetAllAsync(int limit)
    {
        var matches = await _collection.Find(FilterDefinition<Match>.Empty).SortBy(x => x.Date).Limit(limit).ToListAsync();
        var matchModels = new List<MatchModel>();
        foreach(var match in matches)
        {
            var player1 = await _playerRepository.GetByIdAsync((await _playerStatRepository.GetByIdAsync(match.Team1Player1Stat)).Player);
            var player2 = await _playerRepository.GetByIdAsync((await _playerStatRepository.GetByIdAsync(match.Team1Player2Stat)).Player);
            var team1 = new MatchTeam(player1, player2, match.Team1Result);

            player1 = await _playerRepository.GetByIdAsync((await _playerStatRepository.GetByIdAsync(match.Team2Player1Stat)).Player);
            player2 = await _playerRepository.GetByIdAsync((await _playerStatRepository.GetByIdAsync(match.Team2Player2Stat)).Player);
            var team2 = new MatchTeam(player1, player2, match.Team2Result);

            var matchModel = new MatchModel(match.Id!, team1, team2, match.Date);
            matchModels.Add(matchModel);
        }
        return matchModels;
    }
    public async Task AddMatchAsync(MatchModel model)
    {
        var entity = new Match
        {
            Team1Player1Stat = (await _playerStatRepository.GetActualStatAsync(model.Team1.Player1.Id)).Id,
            Team1Player2Stat = (await _playerStatRepository.GetActualStatAsync(model.Team1.Player2.Id)).Id,
            Team2Player1Stat = (await _playerStatRepository.GetActualStatAsync(model.Team2.Player1.Id)).Id,
            Team2Player2Stat = (await _playerStatRepository.GetActualStatAsync(model.Team2.Player2.Id)).Id,
            League = (await _leagueRepository.GetActiveAsync()).Id,
            Team1Result = model.Team1.Score,
            Team2Result = model.Team2.Score,
            Date = model.Date
        };
        await _collection.InsertOneAsync(entity);
    }
}
