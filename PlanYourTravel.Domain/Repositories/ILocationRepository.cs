using PlanYourTravel.Domain.Entities.Location;

namespace PlanYourTravel.Domain.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        public Task<Location> GetByIdAsync(Guid id);
    }
}
