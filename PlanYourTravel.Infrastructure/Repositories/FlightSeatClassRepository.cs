using PlanYourTravel.Domain.Entities.FlightScheduleAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Repositories.Abstraction;
using PlanYourTravel.Infrastructure.Persistence;

namespace PlanYourTravel.Infrastructure.Repositories
{
    public class FlightSeatClassRepository(AppDbContext appDbContext) : IFlightSeatClassRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        // IRepository
        public async Task AddAsync(FlightSeatClass aggregate, CancellationToken cancellationToken = default)
        {
            await _appDbContext.FlightSeatClasses.AddAsync(aggregate, cancellationToken);
        }
        public Task UpdateAsync(FlightSeatClass aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.FlightSeatClasses.Update(aggregate);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(FlightSeatClass aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.FlightSeatClasses.Remove(aggregate);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
