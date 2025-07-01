using Tournament.Data.Data;
using System.Threading.Tasks;
using Domain.Contracts.Repositories;

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
