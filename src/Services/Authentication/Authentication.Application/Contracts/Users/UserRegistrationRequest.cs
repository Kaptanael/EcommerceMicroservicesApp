﻿namespace Authentication.Application.Contracts.Users
{
    public class UserRegistrationRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
