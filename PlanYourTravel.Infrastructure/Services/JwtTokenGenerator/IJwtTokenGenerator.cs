namespace PlanYourTravel.Infrastructure.Services.JwtTokenGenerator
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string email, string role);
    }
}
