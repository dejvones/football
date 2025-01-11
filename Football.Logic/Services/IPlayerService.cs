using Football.Data.Models;

namespace Football.Logic.Services;

public interface IPlayerService
{
    Task<IEnumerable<PlayerModel>> GetAllAsync();
    Task<IEnumerable<PlayerModel>> GetCurrentRanking();
    Task<IEnumerable<PlayerModel>> GettAllTimeRanking();
    Task<bool> PlayerExists(string name);
    Task CreateAsync(string name);
}
