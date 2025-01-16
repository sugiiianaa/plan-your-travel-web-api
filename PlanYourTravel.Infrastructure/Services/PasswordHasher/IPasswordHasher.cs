namespace PlanYourTravel.Infrastructure.Services.PasswordHasher
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}
