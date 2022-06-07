using Membership.Domain.Entities;

namespace Membership.Application.Interfaces.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken));

    }
}
