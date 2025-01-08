using Microsoft.EntityFrameworkCore;
using PlanYourTravel.Domain.Dtos;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Infrastructure.Persistence;

namespace PlanYourTravel.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AppDbContext _appDbContext;

        public FlightRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IUnitOfWork UnitOfWork => _appDbContext;

        async Task<List<FlightScheduleDto>> IFlightRepository.GetFlightSchedule(
            DateTime departureDate,
            Guid departureAirportId,
            Guid arrivalAirportId,
            CancellationToken cancellationToken)
        {
            var query = _appDbContext.FlightSchedules
                .Where(fs =>
                    fs.DepartureAirportId == departureAirportId &&
                    fs.ArrivalAirportId == arrivalAirportId &&
                    fs.DepartureDateTime == departureDate.Date);

            var flightScheduleDtos = await query
                .Select(fs => FlightScheduleDto.Create(
                    fs.Id,
                    fs.FlightNumber,
                    fs.DepartureDateTime,
                    fs.ArrivalDateTime,
                    fs.DepartureAirportId,
                    fs.ArrivalAirportId,
                    fs.AirlineId
                )).ToListAsync(cancellationToken);

            return flightScheduleDtos;
        }
    }
}
