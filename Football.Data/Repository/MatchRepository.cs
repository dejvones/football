using Football.Data.Database;
using Football.Data.Entity;
using Football.Data.Models;
using MongoDB.Driver;

namespace Football.Data.Repository;

public class MatchRepository : IMatchRepository
{
    private readonly IMongoCollection<Match> _collection;

    public MatchRepository(MongoDbContext context)
    {
        context.CheckCollectionExists("matches");
        _collection = context.GetCollection<Match>("matches");
    }

    public async Task<IEnumerable<MatchModel>> GetAllAsync(int limit)
    {
        var matches = await _collection.Find(FilterDefinition<Match>.Empty).SortByDescending(x => x.Date).Limit(limit).ToListAsync();
        return matches.Select(x => 
        new MatchModel(x.Id!, x.LeagueId,
            new MatchTeam(x.Team1.Player1Id, x.Team1.Player1Name, x.Team1.Player2Id, x.Team1.Player2Name, x.Team1.Score), 
            new MatchTeam(x.Team2.Player1Id, x.Team2.Player1Name, x.Team2.Player2Id, x.Team2.Player2Name, x.Team2.Score), 
            x.Date));
    }
    public async Task AddMatchAsync(MatchModel model)
    {
        var entity = new Match { 
            Team1 = new Team { Player1Id = model.Team1.Player1Id, Player1Name = model.Team1.Player1Name, Player2Id = model.Team1.Player2Id, Player2Name = model.Team1.Player2Name, Score = model.Team1.Score }, 
            Team2 = new Team { Player1Id = model.Team2.Player1Id, Player1Name = model.Team2.Player1Name, Player2Id = model.Team2.Player2Id, Player2Name = model.Team2.Player2Name, Score = model.Team2.Score }, 
            Date = model.Date, LeagueId = model.LeagueId };
        await _collection.InsertOneAsync(entity);
    }
}
