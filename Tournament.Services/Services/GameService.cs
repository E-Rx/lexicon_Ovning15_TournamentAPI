using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Service.Contracts;
using Tournament.Core.Dtos;
using Tournament.Core.Dtos.Tournament.Core.Dtos;
using Tournament.Core.QueryParameters;


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
            var game = await _unitOfWork.GameRepository.GetAsync(id);
            return game == null ? null : _mapper.Map<GameDto>(game);
           
        }

        public async Task<GamePagedDto> GetAllGamesAsync(GameQuery query)
        {
            var totalItems = await _unitOfWork.GameRepository.GetCountAsync();

            var games = await _unitOfWork.GameRepository.GetAllAsync(
                query.SortBy,
                query.PageNumber,
                query.PageSize
            );

            var gameDtos = _mapper.Map<List<GameDto>>(games);
            var totalPages = (int)Math.Ceiling((double)totalItems / query.PageSize);

            return new GamePagedDto
            {
                Items = gameDtos,
                TotalCount = totalItems,
                TotalPages = totalPages,
                CurrentPage = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public async Task<IEnumerable<GameDto>> GetGameByTitleAsync(string title)
        {
           var games = await _unitOfWork.GameRepository.GetGameByTitleAsync(title);       
            return _mapper.Map<IEnumerable<GameDto>>(games);
        }


        public async Task<GameDto> CreateGameAsync(GameCreateDto gameCreateDto)
        {
            await ValidateMaxGamesPerTournament(gameCreateDto.TournamentId);

            var game = _mapper.Map<Game>(gameCreateDto);
            _unitOfWork.GameRepository.Add(game);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GameDto>(game);
        }

        public async Task<bool> UpdateGameAsync(GameUpdateDto gameUpdateDto)
        {
            var existingGame = await _unitOfWork.GameRepository.GetAsync(gameUpdateDto.Id);
            if (existingGame == null) return false;
            
            if (existingGame.TournamentId != gameUpdateDto.TournamentId)
            {
                await ValidateMaxGamesPerTournament(gameUpdateDto.TournamentId);
            }

            _mapper.Map(gameUpdateDto, existingGame);
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
            if (patchDocument == null) return false;

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

        public class BusinessRuleException : Exception
        {
            public BusinessRuleException(string message) : base(message) { }
        }
    }
}
