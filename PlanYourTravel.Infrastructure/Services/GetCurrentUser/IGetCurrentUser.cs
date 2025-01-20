using PlanYourTravel.Domain.Entities.UserAggregate;

namespace PlanYourTravel.Infrastructure.Services.GetCurrentUser
{
    public interface IGetCurrentUser
    {
        Guid? UserId { get; }
        string? EmailAddress { get; }
        string? Role { get; }

        // Optionally fetch the full user entity from DB
        Task<User?> GetUserAsync(CancellationToken cancellationToken);
    }
}
