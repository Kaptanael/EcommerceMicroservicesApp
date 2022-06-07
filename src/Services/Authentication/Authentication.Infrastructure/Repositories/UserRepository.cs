using Authentication.Infrastructure.Persistence;
using Membership.Application.Interfaces.Repositories;
using Membership.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Membership.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MembershipDbContext dbContext) : base(dbContext)
        {
        }

        public MembershipDbContext MembershipDbContext
        {
            get { return Context as MembershipDbContext; }
        }

        public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken)) 
        {
            return await MembershipDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u=>u.Email.ToLower() == email.ToLower(), cancellationToken);
        }
    }
}
