using Tournament.Core.Entities;

namespace Tournament.Core.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync(string? sortBy = null, int pageNumber = 1, int pageSize = 10);
        Task<Game?> GetAsync(int id);
        Task<IEnumerable<Game>> GetByTitleAsync(string title);
        Task<bool> AnyAsync(int id);
        void Add(Game game);
        void Update(Game game);
        void Remove(Game game);
        
        // sort and filter 
        IQueryable<Game> GetQueryable();

    }
}
