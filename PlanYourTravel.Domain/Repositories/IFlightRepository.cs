using PlanYourTravel.Domain.Dtos;
using PlanYourTravel.Domain.Entities;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IFlightRepository
    {
        Task<List<FlightScheduleDto>> GetFlightSchedule(
            DateTime departureDate,
            Guid DepartureAirportId,
            Guid ArrivalAirportId,
            CancellationToken cancellationToken);

        Task<Guid> AddAsync(
            FlightSchedule flightSchedule,
            CancellationToken cancellationToken);
    }
}
