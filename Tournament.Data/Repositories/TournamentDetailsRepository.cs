using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using Tournament.Core.QueryParameters;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;

namespace Tournament.Data.Repositories
{
    public class TournamentDetailsRepository : ITournamentDetailsRepository
    {
        private readonly TournamentAPIContext _context;
        


        public TournamentDetailsRepository(TournamentAPIContext context)
        {
            _context = context;
        }

        // All tournaments with or without games
        public async Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames = false, string? sortBy = null, int pageNumber = 1, int pageSize = 10)
        {

            IQueryable<TournamentDetails> query = _context.TournamentDetails;

            if (includeGames)
            {
                query = query.Include(t => t.Games);
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "title" => query.OrderBy(t => t.Title),
                    "title_desc" => query.OrderByDescending(t => t.Title),
                    "startDate" => query.OrderBy(t => t.StartDate),
                    "startDate_desc" => query.OrderByDescending(t => t.StartDate),
                    _ => query
                };
            }

            // Pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
           
            return await query.ToListAsync();

          
        }

        public async Task<TournamentDetails?> GetAsync(int id, bool includeGames = false)
        {
            if (includeGames)
            {
                return await _context.TournamentDetails
                                     .Include(t => t.Games)
                                     .FirstOrDefaultAsync(t => t.Id == id);
            }

            return await _context.TournamentDetails
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _context.TournamentDetails.AnyAsync(t => t.Id == id);
        }

        public void Add(TournamentDetails tournament)
        {
            _context.TournamentDetails.Add(tournament);
        }

        public void Update(TournamentDetails tournament)
        {
            _context.TournamentDetails.Update(tournament);
        }

        public void Remove(TournamentDetails tournament)
        {
            _context.TournamentDetails.Remove(tournament);
        }

        public IQueryable<TournamentDetails> GetQueryable()
        {
            return _context.TournamentDetails.AsQueryable();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.TournamentDetails.CountAsync();
        }



    }
}
