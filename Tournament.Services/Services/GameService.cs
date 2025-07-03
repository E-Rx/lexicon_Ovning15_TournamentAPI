using AutoMapper;
using Domain.Contracts.Repositories;
using Service.Contracts;
using Tournament.Core.Dtos;
using Tournament.Core.QueryParameters;
using Microsoft.AspNetCore.JsonPatch;
using Domain.Models.Entities;


namespace Tournament.Services.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int MaxGamesPerTournament = 10;


        public GameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GameDto?> GetGameAsync(int id)
        {
            var game = _unitOfWork.GameRepository.GetAsync(id);
            return game == null ? null : _mapper.Map<GameDto>(game);
           
        }

        public async Task<IEnumerable<GameDto>> GetAllGamesAsync(GameQuery query)
        { 
           var games = _unitOfWork.GameRepository.GetAllAsync(
                query.SortBy,
                query.PageNumber,
                query.PageSize
            );
           
            return _mapper.Map<IEnumerable<GameDto>>(games);
        }

        public async Task<IEnumerable<GameDto>> GetGameByTitleAsync(string title)
        {
           var games = _unitOfWork.GameRepository.GetGameByTitleAsync(title);       
            return _mapper.Map<IEnumerable<GameDto>>(games);
        }


        public async Task<bool> CreateGameAsync(GameDto gameDto)
        {
            await ValidateMaxGamesPerTournament(gameDto.TournamentId);

            var game = _mapper.Map<Game>(gameDto);
            _unitOfWork.GameRepository.Add(game);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateGameAsync(GameDto gameDto)
        {
            var existingGame = await _unitOfWork.GameRepository.GetAsync(gameDto.Id);
            if (existingGame == null) return false;
            
            if (existingGame.TournamentId != gameDto.TournamentId)
            {
                await ValidateMaxGamesPerTournament(gameDto.TournamentId);
            }

            _mapper.Map(gameDto, existingGame);
            _unitOfWork.GameRepository.Update(existingGame);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
           var game = await _unitOfWork.GameRepository.GetAsync(id);
            if (game == null) return false;

            _unitOfWork.GameRepository.Remove(game);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        
        public async Task<bool> PatchGameAsync(int gameId, JsonPatchDocument<GameUpdateDto> patchDocument)
        {
            var game = await _unitOfWork.GameRepository.GetAsync(gameId);   
            if (game == null) return false;
           
            var dtoToPatch = _mapper.Map<GameUpdateDto>(game);
            patchDocument.ApplyTo(dtoToPatch);

            if (game.TournamentId != dtoToPatch.TournamentId)
            {
                await ValidateMaxGamesPerTournament(dtoToPatch.TournamentId);
            }   

            _mapper.Map(dtoToPatch, game);  
            await _unitOfWork.CompleteAsync();
            return true;
        }

        private async Task ValidateMaxGamesPerTournament(int tournamentId)
        {
            var gameCount = await _unitOfWork.GameRepository.CountGamesByTournamentAsync(tournamentId);
            if (gameCount >= MaxGamesPerTournament)
            {
                throw new Exception($"Cannot add more than {MaxGamesPerTournament} games to a tournament.");
            }


     
        }

    }
}
