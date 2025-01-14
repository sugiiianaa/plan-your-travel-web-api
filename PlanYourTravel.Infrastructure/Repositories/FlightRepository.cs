using Microsoft.EntityFrameworkCore;
using PlanYourTravel.Domain.Dtos;
using PlanYourTravel.Domain.Entities.FlightSchedule;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Infrastructure.Persistence;

namespace PlanYourTravel.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AppDbContext _appDbContext;
        public IUnitOfWork UnitOfWork => _appDbContext;

        public FlightRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guid> CreateFlightSchedule(FlightSchedule flightSchedule, CancellationToken cancellationToken)
        {
            await _appDbContext.FlightSchedules.AddAsync(flightSchedule, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken); // Save changes here to avoid shared DbContext issues.
            return flightSchedule.Id;
        }

        public async Task<Guid> CreateSeatClass(FlightSeatClass seatClass, CancellationToken cancellationToken)
        {
            await _appDbContext.FlightSeatClasses.AddAsync(seatClass, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return seatClass.Id;
        }

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

        public async Task<FlightScheduleDto?> GetFlightShcheduleById(Guid flightScheduleId)
        {
            var flightScheduleDto = await _appDbContext.FlightSchedules
                .Where(fs => fs.Id == flightScheduleId)
                .Select(fs => FlightScheduleDto.Create(
                    fs.Id,
                    fs.FlightNumber,
                    fs.DepartureDateTime,
                    fs.ArrivalDateTime,
                    fs.DepartureAirportId,
                    fs.ArrivalAirportId,
                    fs.AirlineId)).FirstOrDefaultAsync();

            return flightScheduleDto;


        }
    }
}
