using Football.Data.Models;

namespace Football.Data.Repository;

public interface IPlayerRepository
{
    Task<IEnumerable<PlayerModel>> GetAllAsync();
    Task<PlayerModel> GetByIdAsync(string id);
    Task<PlayerModel?> GetByNameAsync(string name);
    Task AddPlayerAsync(PlayerModel model);
    Task UpdatePlayerAsync(PlayerModel model);
}
