﻿namespace Authentication.Application.Contracts.Users
{
    public class UserLoginRequest
    {       
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
