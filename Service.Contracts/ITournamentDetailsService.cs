using Tournament.Core.Dtos;

namespace Service.Contracts
{
    public interface ITournamentDetailsService
    {
        Task<TournamentDetailsDto> GetTournamentDetailsAsync(int tournamentId);
        Task<IEnumerable<TournamentDetailsDto>> GetAllTournamentsAsync();
        Task<bool> CreateTournamentAsync(TournamentDetailsDto tournamentDetails);
        Task<bool> UpdateTournamentAsync(TournamentDetailsDto tournamentDetails);
        Task<bool> DeleteTournamentAsync(int tournamentId);
    }
}
