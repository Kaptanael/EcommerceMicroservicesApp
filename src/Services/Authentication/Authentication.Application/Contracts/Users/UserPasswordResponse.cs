namespace Authentication.Application.Contracts.Users
{
    public class UserPasswordResponse
    {
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
