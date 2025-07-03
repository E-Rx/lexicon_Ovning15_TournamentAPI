using Microsoft.AspNetCore.JsonPatch;
using System.Text.Json.Nodes;
using Tournament.Core.Dtos;
using Tournament.Core.QueryParameters;

namespace Service.Contracts
{
    public interface ITournamentDetailsService
    {
        Task<TournamentDetailsPagedDto> GetAllTournamentsAsync(TournamentDetailsQuery query);
        Task<TournamentDetailsDto?> GetTournamentDetailsAsync(TournamentDetailsQuery query);
        Task<TournamentDetailsDto> CreateTournamentAsync(TournamentDetailsCreateDto tournamentDetailsCreateDto);
        Task<bool> UpdateTournamentAsync(TournamentDetailsUpdateDto tournamentDetailsUpdateDto);
        Task<bool> DeleteTournamentAsync(int Id);
        Task<bool> PatchTournamentDetails(int tournamentDetailsId, JsonPatchDocument<TournamentDetailsUpdateDto> patchDocument);
    }
}
