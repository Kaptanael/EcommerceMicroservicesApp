namespace Membership.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        int Complete();
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
