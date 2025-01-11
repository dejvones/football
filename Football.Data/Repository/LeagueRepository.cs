using Football.Data.Database;
using Football.Data.Entity;
using Football.Data.Models;
using MongoDB.Driver;

namespace Football.Data.Repository;

public class LeagueRepository : ILeagueRepository
{
    private readonly IMongoCollection<League> _collection;

    public LeagueRepository(MongoDbContext context)
    {
        context.CheckCollectionExists("leagues");
        _collection = context.GetCollection<League>("leagues");
    }

    public async Task<LeagueModel?> GetActiveAsync()
    {
        var filter = Builders<League>.Filter.Eq(x => x.Active, true);
        var entity = await _collection.Find(filter).FirstOrDefaultAsync();
        return entity == null ? null : new LeagueModel(entity.Id!, entity.Name, entity.StartDate, entity.EndDate);
    }

    public async Task<IEnumerable<LeagueModel>> GetAllAsync()
    {
        var leagues = await _collection.Find(FilterDefinition<League>.Empty).ToListAsync();
        return leagues.Select(x => new LeagueModel(x.Id!, x.Name, x.StartDate, x.EndDate));
    }

    public async Task AddLeagueAsync()
    {
        var league = new League
        {
            Name = "Premier League",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1)
        };
        await _collection.InsertOneAsync(league);
    }
}
