using Authentication.Infrastructure.Persistence;
using Membership.Application.Interfaces.Repositories;

namespace Membership.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MembershipDbContext _dbContext;        

        public UnitOfWork(MembershipDbContext context)
        {
            _dbContext = context;
            Users = new UserRepository(_dbContext);
            RefreshTokens = new RefreshTokenRepository(_dbContext);
        }

        public IUserRepository Users { get; private set; }
        public IRefreshTokenRepository RefreshTokens { get; private set; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
