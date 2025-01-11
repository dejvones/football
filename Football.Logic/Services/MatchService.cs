using Football.Data.Models;
using Football.Data.Repository;

namespace Football.Logic.Services;

public class MatchService(IMatchRepository matchRepository) : IMatchService
{
    private readonly IMatchRepository _matchRepository = matchRepository;

    public async Task<IEnumerable<MatchModel>> GetLastMatches()
    {
        return await _matchRepository.GetAllAsync(5);
    }

    public async Task CreateMatchAsync(MatchModel model)
    {
        await _matchRepository.AddMatchAsync(model);
    }
}
