using PlanYourTravel.Domain.Dtos;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IFlightRepository
    {
        Task<List<FlightScheduleDto>> GetFlightSchedule(
            DateTime departureDate,
            Guid DepartureAirportId,
            Guid ArrivalAirportId,
            CancellationToken cancellationToken);

    }
}
