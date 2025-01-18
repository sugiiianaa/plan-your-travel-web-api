using PlanYourTravel.Domain.Entities.LocationAggregate;
using PlanYourTravel.Domain.Repositories.Abstraction;

namespace PlanYourTravel.Domain.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        public Task<Location?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
