using Football.Data.Models;

namespace Football.Logic.Services;

public interface IMatchService
{
    Task<IEnumerable<MatchModel>> GetLastMatches();
    Task CreateMatchAsync(MatchModel model);
}
