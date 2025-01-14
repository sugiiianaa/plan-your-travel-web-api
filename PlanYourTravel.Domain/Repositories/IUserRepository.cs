using PlanYourTravel.Domain.Entities.User;
using PlanYourTravel.Domain.ValueObjects;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
