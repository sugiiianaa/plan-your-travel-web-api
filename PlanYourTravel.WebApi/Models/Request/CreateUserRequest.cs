﻿namespace PlanYourTravel.WebApi.Models.Request
{
    public class CreateUserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
    }
}
