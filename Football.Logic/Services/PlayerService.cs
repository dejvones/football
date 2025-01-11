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
        await _playerRepository.AddPlayerAsync(new PlayerModel(string.Empty, name, DateTime.Now));
    }
}
