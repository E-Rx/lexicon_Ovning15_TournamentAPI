using Tournament.Core.Repositories;
using Tournament.Data.Data;
using System.Threading.Tasks;

namespace Tournament.Data.Repositories
{
    public class UnitOfWork(
        TournamentAPIContext context,
        ITournamentDetailsRepository tournamentDetailsRepository,
        IGameRepository gameRepository) : IUnitOfWork
    {
        public ITournamentDetailsRepository TournamentDetailsRepository => tournamentDetailsRepository;
        public IGameRepository GameRepository => gameRepository;

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
