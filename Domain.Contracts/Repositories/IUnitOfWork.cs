namespace Domain.Contracts.Repositories
{
    public interface IUnitOfWork
    {
        ITournamentDetailsRepository TournamentDetailsRepository { get; }
        IGameRepository GameRepository { get; }
        Task CompleteAsync();
    }
}
