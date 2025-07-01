
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entities;


namespace Tournament.Data.Data;

    public class TournamentAPIContext : DbContext
    {
        public TournamentAPIContext (DbContextOptions<TournamentAPIContext> options)
            : base(options)
        {
        }

        public DbSet<TournamentDetails> TournamentDetails { get; set; } = default!;

        public DbSet<Game> Games { get; set; } = default!;
    }
