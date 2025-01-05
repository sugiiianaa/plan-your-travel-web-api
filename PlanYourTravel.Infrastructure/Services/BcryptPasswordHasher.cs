using PlanYourTravel.Domain.Services;

namespace PlanYourTravel.Infrastructure.Services
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            var pepper = Environment.GetEnvironmentVariable("PLAN_YOUR_TRAVEL_PEPPER_WORD");

            var passwordToHash = pepper is not null
                ? password + pepper
                : password;

            return BCrypt.Net.BCrypt.HashPassword(passwordToHash);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var pepper = Environment.GetEnvironmentVariable("PLAN_YOUR_TRAVEL_PEPPER_WORD");

            var passwordToVerify = pepper is not null
                ? providedPassword + pepper
                : providedPassword;

            return BCrypt.Net.BCrypt.Verify(passwordToVerify, hashedPassword);
        }
    }
}
