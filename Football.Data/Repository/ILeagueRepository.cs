using Football.Data.Models;

namespace Football.Data.Repository;

public interface ILeagueRepository
{
    Task<LeagueModel> GetActiveAsync();
    Task<IEnumerable<LeagueModel>> GetAllAsync();
    Task AddLeagueAsync(LeagueModel model);
}
