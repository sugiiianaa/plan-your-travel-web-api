namespace PlanYourTravel.Infrastructure.Services.TokenGenerator
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string email, string role);
    }
}
