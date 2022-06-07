namespace Authentication.Application.Exceptions
{
    public abstract class ForbiddenAccessException : ApplicationException
    {
        protected ForbiddenAccessException(string message)
            : base("Access Denied", message) { }
    }
}
