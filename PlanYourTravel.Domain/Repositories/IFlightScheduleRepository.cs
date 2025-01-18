using PlanYourTravel.Domain.Entities.FlightScheduleAggregate;
using PlanYourTravel.Domain.Repositories.Abstraction;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IFlightScheduleRepository : IRepository<FlightSchedule>
    {
        public Task<FlightSchedule?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        public Task<(IList<FlightSchedule> items, int totalCount)> GetAllAsync(
            DateTime departureDate,
            Guid departureAirportId,
            Guid arrivalAirportId,
            Guid lastSeenId,
            int pageSize,
            CancellationToken cancellationToken);
    }
}
