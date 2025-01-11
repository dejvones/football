using Football.Data.Entity;

namespace Football.Data.Repository;

public interface IPlayerStatRepository
{
    Task<PlayerStat?> GetByIdAsync(string id);
    Task<PlayerStat?> GetActualStatAsync(string playerId);
}
