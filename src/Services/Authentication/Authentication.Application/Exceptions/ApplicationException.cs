﻿
namespace Authentication.Application.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        protected ApplicationException()
        {
        }

        protected ApplicationException(string title, string message)
            : base(message) =>
            Title = title;

        public string Title { get; }
    }
}
