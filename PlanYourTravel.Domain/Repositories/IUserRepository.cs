using PlanYourTravel.Domain.Entities.User;
using PlanYourTravel.Domain.ValueObjects;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    }
}
