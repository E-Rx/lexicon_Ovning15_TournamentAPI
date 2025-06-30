
using Microsoft.EntityFrameworkCore;


namespace Tournament.Data.Data;

    public class TournamentAPIContext : DbContext
    {
        public TournamentAPIContext (DbContextOptions<TournamentAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament.Core.Entities.TournamentDetails> TournamentDetails { get; set; } = default!;

        public DbSet<Tournament.Core.Entities.Game> Games { get; set; } = default!;
    }
