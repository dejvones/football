using Football.Data.Database;
using Football.Data.Entity;
using MongoDB.Driver;

namespace Football.Data.Repository;

public class PlayerStatRepository : IPlayerStatRepository
{
    private readonly IMongoCollection<PlayerStat> _collection;
    private readonly ILeagueRepository _leagueRepository;

    public PlayerStatRepository(MongoDbContext context, ILeagueRepository leagueRepository)
    {
        context.CheckCollectionExists("stats");
        _collection = context.GetCollection<PlayerStat>("stats");

        _leagueRepository = leagueRepository;
    }

    public async Task<PlayerStat?> GetByIdAsync(string id)
    {
        var filter = Builders<PlayerStat>.Filter.Eq(x => x.Id, id);
        var entity = await _collection.Find(filter).FirstOrDefaultAsync();
        return entity;
    } 

    public async Task<PlayerStat?> GetActualStatAsync(string playerId)
    {
        var activeLeague = await _leagueRepository.GetActiveAsync();
        if (activeLeague is null)
        {
            return null;
        }
        var filter = Builders<PlayerStat>.Filter.And(
            Builders<PlayerStat>.Filter.Eq(x => x.Player, playerId),
            Builders<PlayerStat>.Filter.Eq(x => x.League, activeLeague.Id));
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
}
