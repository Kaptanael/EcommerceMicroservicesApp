namespace Authentication.Application.Contracts.Auth
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }

    }
}
