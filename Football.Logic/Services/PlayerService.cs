using Football.Data.Models;
using Football.Data.Repository;

namespace Football.Logic.Services;

public class PlayerService(IPlayerRepository playerRepository) : IPlayerService
{
    private readonly IPlayerRepository _playerRepository = playerRepository;

    public async Task<IEnumerable<PlayerModel>> GetAllAsync()
    {
        return await _playerRepository.GetAllAsync();
    }

    public async Task CreateAsync(string name)
    {
        await _playerRepository.AddPlayerAsync(new PlayerModel(string.Empty, name, DateTime.Now, new Stats(100,100,0,0)));
    }

    public async Task<bool> PlayerExists(string name)
    {
        var player = await _playerRepository.GetByNameAsync(name);
        return player is not null;
    }

    public async Task<IEnumerable<PlayerModel>> GetCurrentRanking()
    {
        var players = await _playerRepository.GetAllAsync();
        return players.OrderByDescending(x => x.Stats.CurrentPoints);
    }

    public async Task<IEnumerable<PlayerModel>> GettAllTimeRanking()
    {
        var players = await _playerRepository.GetAllAsync();
        return players.OrderByDescending(x => x.Stats.AllPoints);
    }
}
