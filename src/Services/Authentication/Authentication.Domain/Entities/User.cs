using Membership.Domain.Contracts;

namespace Membership.Domain.Entities
{
    public class User : EntityBase
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public RefreshToken RefreshToken { get; set; }

    }
}
