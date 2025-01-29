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
        return players.Select(x => new PlayerModel(x.Id!, x.Name, x.Registred, new Stats(x.AllPoints, x.CurrentPoints, x.AllMatches,
            x.CurrentMatches, x.Wins, x.Form.Select(r => (Result)r).ToArray())));
    }

    public async Task<PlayerModel> GetByIdAsync(string id)
    {
        var filter = Builders<Player>.Filter.Eq(x => x.Id, id);
        var entity = await _collection.Find(filter).FirstOrDefaultAsync();
        return entity == null
            ? throw new Exception("Player not found")
            : new PlayerModel(entity.Id!, entity.Name, entity.Registred, new Stats(entity.AllPoints, entity.CurrentPoints, entity.AllMatches, 
            entity.CurrentMatches, entity.Wins, entity.Form.Select(r => (Result)r).ToArray()));
    }

    public async Task<PlayerModel?> GetByNameAsync(string name)
    {
        var filter = Builders<Player>.Filter.Eq(x => x.Name, name);
        var entity = await _collection.Find(filter).FirstOrDefaultAsync();
        return entity == null
            ? null
            : new PlayerModel(entity.Id!, entity.Name, entity.Registred, new Stats(entity.AllPoints, entity.CurrentPoints, entity.AllMatches, 
            entity.CurrentMatches, entity.Wins, entity.Form.Select(r => (Result)r).ToArray()));
    }

    public async Task AddPlayerAsync(PlayerModel model)
    {
        var player = new Player { Name = model.Name, Registred = model.Registred, AllPoints = model.Stats.AllPoints, CurrentPoints = model.Stats.CurrentPoints, AllMatches = model.Stats.AllMatches, 
            CurrentMatches = model.Stats.CurrentMatches, Wins = model.Stats.Wins, Form = model.Stats.Form.Select(r => (int)r).ToArray() };
        await _collection.InsertOneAsync(player);
    }

    public async Task UpdatePlayerAsync(PlayerModel model)
    {
        var filter = Builders<Player>.Filter.Eq(x => x.Id, model.Id);
        var update = Builders<Player>.Update
            .Set(x => x.Name, model.Name)
            .Set(x => x.AllPoints, model.Stats.AllPoints)
            .Set(x => x.CurrentPoints, model.Stats.CurrentPoints)
            .Set(x => x.AllMatches, model.Stats.AllMatches)
            .Set(x => x.CurrentMatches, model.Stats.CurrentMatches)
            .Set(x => x.Wins, model.Stats.Wins)
            .Set(x => x.Form, model.Stats.Form.Select(x => (int)x).ToArray());
        await _collection.UpdateOneAsync(filter, update);
    }
}
