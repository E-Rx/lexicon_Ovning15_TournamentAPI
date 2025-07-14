using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync(string? sortBy = null, int pageNumber = 1, int pageSize = 10);
        Task<Game?> GetAsync(int id);
        Task<IEnumerable<Game>> GetGameByTitleAsync(string title);
        Task<bool> AnyAsync(int id);
        void Add(Game game);
        void Update(Game game);
        void Remove(Game game);
        
        IQueryable<Game> GetQueryable();
        Task<int> CountGamesByTournamentAsync(int tournamentId);
        Task<int> GetCountAsync();
    }
}
