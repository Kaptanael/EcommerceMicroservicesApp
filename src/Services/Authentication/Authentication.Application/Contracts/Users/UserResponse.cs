namespace Authentication.Application.Contracts.Users
{
    public class UserResponse:BaseResponse
    {
        public string UserName { get; set; }

        public string Email { get; set; }        

    }
}
