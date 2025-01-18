using Microsoft.EntityFrameworkCore;
using PlanYourTravel.Domain.Entities.FlightScheduleAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Repositories.Abstraction;
using PlanYourTravel.Infrastructure.Persistence;

namespace PlanYourTravel.Infrastructure.Repositories
{
    public class FlightScheduleRepository(AppDbContext appDbContext) : IFlightScheduleRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public IUnitOfWork UnitOfWork => _appDbContext;

        // IRepository 
        public async Task AddAsync(FlightSchedule aggregate, CancellationToken cancellationToken = default)
        {
            await _appDbContext.FlightSchedules.AddAsync(aggregate, cancellationToken);
        }

        public Task UpdateAsync(FlightSchedule aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.FlightSchedules.Update(aggregate);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(FlightSchedule aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.FlightSchedules.Remove(aggregate);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        // Extentions
        public async Task<FlightSchedule?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _appDbContext.FlightSchedules
                .FirstOrDefaultAsync(fs => fs.Id == id, cancellationToken);
        }

        public async Task<(IList<FlightSchedule> items, int totalCount)> GetAllAsync(
            DateTime departureDate,
            Guid departureAirportId,
            Guid arrivalAirportId,
            Guid lastSeenId,
            int pageSize,
            CancellationToken cancellationToken)
        {
            // Base query: filter by departure date and airports.
            IQueryable<FlightSchedule> query = _appDbContext.FlightSchedules
                .Where(fs =>
                    fs.DepartureDateTime.Date == departureDate.Date &&
                    fs.DepartureAirportId == departureAirportId &&
                    fs.ArrivalAirportId == arrivalAirportId);

            int totalCount = await query.CountAsync(cancellationToken);

            if (lastSeenId != Guid.Empty)
            {
                query = query.Where(fs => fs.Id.CompareTo(lastSeenId) > 0);
            }

            // Order by Id for stable pagination, then take pageSize items.
            query = query
                .OrderBy(fs => fs.Id)
                .Take(pageSize);

            var items = await query.ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }

}
