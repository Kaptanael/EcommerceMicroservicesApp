namespace Authentication.Application.Exceptions
{
    public abstract class NotFoundException :  ApplicationException
    {
        protected NotFoundException(string name, object key)
            : base("Not Fount", $"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
