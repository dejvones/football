using Football.Data.Models;

namespace Football.Logic.Services;

public interface IPlayerService
{
    Task<IEnumerable<PlayerModel>> GetAllAsync();
    Task CreateAsync(string name);
}
