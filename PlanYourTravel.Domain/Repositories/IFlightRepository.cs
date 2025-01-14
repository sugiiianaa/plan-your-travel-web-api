using PlanYourTravel.Domain.Dtos;
using PlanYourTravel.Domain.Entities.FlightSchedule;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IFlightRepository
    {
        Task<List<FlightScheduleDto>> GetFlightSchedule(
            DateTime departureDate,
            Guid DepartureAirportId,
            Guid ArrivalAirportId,
            CancellationToken cancellationToken);

        Task<FlightScheduleDto?> GetFlightShcheduleById(
            Guid flightScheduleId);

        Task<Guid> CreateFlightSchedule(
            FlightSchedule flightSchedule,
            CancellationToken cancellationToken);

        Task<Guid> CreateSeatClass(
            FlightSeatClass seatClass,
            CancellationToken cancellationToken);
    }
}
