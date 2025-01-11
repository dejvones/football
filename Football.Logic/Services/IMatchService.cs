using Football.Data.Models;

namespace Football.Logic.Services;

public interface IMatchService
{
    Task<IEnumerable<MatchModel>> GetLastMatches();
    Task<IEnumerable<MatchModel>> GetAllMatches();
    Task CreateMatchAsync(MatchModel model);
}
