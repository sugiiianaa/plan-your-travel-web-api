using PlanYourTravel.Domain.Entities;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
        IUnitOfWork UnitOfWork { get; }
    }
}
