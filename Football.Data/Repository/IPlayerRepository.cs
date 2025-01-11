using Football.Data.Models;

namespace Football.Data.Repository;

public interface IPlayerRepository
{
    Task<IEnumerable<PlayerModel>> GetAllAsync();
    Task<PlayerModel?> GetByIdAsync(string id);
    Task AddPlayerAsync(PlayerModel model);
}
