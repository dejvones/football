using Football.Data.Database;
using Football.Data.Entity;
using Football.Data.Models;
using MongoDB.Driver;

namespace Football.Data.Repository;

public class PlayerRepository : IPlayerRepository
{
    private readonly IMongoCollection<Player> _collection;

    public PlayerRepository(MongoDbContext context)
    {
        context.CheckCollectionExists("players");
        _collection = context.GetCollection<Player>("players");
    }

    public async Task<IEnumerable<PlayerModel>> GetAllAsync()
    {
        var players = await _collection.Find(FilterDefinition<Player>.Empty).ToListAsync();
        return players.Select(x => new PlayerModel(x.Id!, x.Name, x.Registred));
    }

    public async Task<PlayerModel?> GetByIdAsync(string id)
    {
        var filter = Builders<Player>.Filter.Eq(x => x.Id, id);
        var entity = await _collection.Find(filter).FirstOrDefaultAsync();
        return entity == null ? null : new PlayerModel(entity.Id!, entity.Name, entity.Registred);
    }

    public async Task AddPlayerAsync(PlayerModel model)
    {
        var player = new Player { Name = model.Name, Registred = model.Registred, PlayerStats = [] };
        await _collection.InsertOneAsync(player);
    }
}
