using PlanYourTravel.Domain.Entities.AirportAggregate;
using PlanYourTravel.Domain.Repositories.Abstraction;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IAirportRepository : IRepository<Airport>
    {
        public Task<(IList<Airport> items, int totalCount)> GetAllAsync(Guid lastSeenId, int pageSize, CancellationToken cancellationToken);
    }
}
