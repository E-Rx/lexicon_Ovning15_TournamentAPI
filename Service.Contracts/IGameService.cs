using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dtos;
using Tournament.Core.Dtos.Tournament.Core.Dtos;
using Tournament.Core.QueryParameters;

namespace Service.Contracts
{
    public interface IGameService
    {
        Task<GameDto?> GetGameAsync(int id);
        Task<GamePagedDto> GetAllGamesAsync(GameQuery query);        
        Task<IEnumerable<GameDto>> GetGameByTitleAsync(string title);
        Task<GameDto> CreateGameAsync(GameCreateDto gameCreateDto);
        Task<bool> UpdateGameAsync(GameUpdateDto gameUpdateDto);
        Task<bool> DeleteGameAsync(int id);
        Task<bool> PatchGameAsync(int gameId, JsonPatchDocument<GameUpdateDto> patchDocument);

    }
}
