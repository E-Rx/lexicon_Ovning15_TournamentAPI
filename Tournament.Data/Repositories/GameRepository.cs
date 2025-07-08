using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Domain.Models.Entities;
using Domain.Contracts.Repositories; 

namespace Tournament.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly TournamentAPIContext _context;

        public GameRepository(TournamentAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync(string? sortBy = null, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<Game> query = _context.Games;

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "title" => query.OrderBy(g => g.Title),
                    "title_desc" => query.OrderByDescending(g => g.Title),
                    "time" => query.OrderBy(g => g.Time),
                    "time_desc" => query.OrderByDescending(g => g.Time),
                    _ => query
                };
            }

            // Pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }


        public async Task<Game?> GetAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public async Task<IEnumerable<Game>> GetGameByTitleAsync(string title)
        {

            return await _context.Games
               .Where(g => g.Title.ToLower().Contains(title.ToLower()))
               .ToListAsync();
        }


        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Games.AnyAsync(g => g.Id == id);
        }

        public void Add(Game game)
        {
            _context.Games.Add(game);
        }

        public void Update(Game game)
        {
            _context.Games.Update(game);
        }

        public void Remove(Game game)
        {
            _context.Games.Remove(game);
        }


        public IQueryable<Game> GetQueryable()
        {
            return _context.Games.AsQueryable();
        }

        public async Task<int> CountGamesByTournamentAsync(int tournamentId)
        {
            return await _context.Games.CountAsync(g => g.TournamentId == tournamentId);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Games.CountAsync();
        }

    }
}
