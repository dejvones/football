using Football.Data.Models;

namespace Football.Data.Repository;

public interface IMatchRepository
{
    Task<IEnumerable<MatchModel>> GetAllAsync(int limit);
    Task AddMatchAsync(MatchModel model);
}
