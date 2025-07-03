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

        public ITournamentDetailsService TournamentDetailsService => tournamentDetailsService.Value; 
        public IGameService GameService => gameService.Value;

        public ServiceManager(Lazy<ITournamentDetailsService> tournamentdetailsservice, Lazy<IGameService> gameservice)
        {
            tournamentDetailsService = tournamentdetailsservice;
            gameService = gameservice;
        }
    }
}
