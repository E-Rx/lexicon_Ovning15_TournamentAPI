using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface ITournamentDetailsRepository
    {
        Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames = false, string? sortBy = null, int pageNumber = 1, int pageSize = 10);
        Task<TournamentDetails?> GetAsync(int id, bool includeGames = false);
        Task<bool> AnyAsync(int id);
        void Add(TournamentDetails tournament);
        void Update(TournamentDetails tournament);
        void Remove(TournamentDetails tournament);


        // sort and filter
        IQueryable<TournamentDetails> GetQueryable();


    }
}
