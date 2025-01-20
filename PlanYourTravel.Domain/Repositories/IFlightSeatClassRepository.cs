using PlanYourTravel.Domain.Entities.FlightScheduleAggregate;
using PlanYourTravel.Domain.Repositories.Abstraction;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IFlightSeatClassRepository : IRepository<FlightSeatClass>
    {
        public Task<FlightSeatClass?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
