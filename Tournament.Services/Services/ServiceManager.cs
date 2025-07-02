using AutoMapper;
using Domain.Contracts.Repositories;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Services.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITournamentDetailsService> tournamentDetailsService;
        private readonly Lazy<IGameService> gameService;

        public ITournamentDetailsService TournamentService => tournamentDetailsService.Value;
        public IGameService GameService => gameService.Value;

       
        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)

        {
            ArgumentNullException.ThrowIfNull(nameof(unitOfWork));

            tournamentDetailsService = new Lazy<ITournamentDetailsService>(() => new TournamentDetailsService(unitOfWork, mapper));
            gameService = new Lazy<IGameService>(() => new GameService(unitOfWork, mapper));
        }

       
    }
}
