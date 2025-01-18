using PlanYourTravel.Domain.Entities.UserAggregate;
using PlanYourTravel.Domain.Repositories.Abstraction;
using PlanYourTravel.Domain.ValueObjects;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    }
}
