using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dtos;
using Tournament.Core.QueryParameters;

namespace Service.Contracts
{
    public interface IGameService
    {
        Task<GameDto?> GetGameAsync(int id);
        Task<IEnumerable<GameDto>> GetAllGamesAsync(GameQuery query);
        Task<IEnumerable<GameDto>> GetGameByTitleAsync(string title);
        Task<bool> CreateGameAsync(GameDto gameDto);
        Task<bool> UpdateGameAsync(GameDto gameDto);
        Task<bool> DeleteGameAsync(int id);
        Task<bool> PatchGameAsync(int gameId, JsonPatchDocument<GameUpdateDto> patchDocument);

    }
}
