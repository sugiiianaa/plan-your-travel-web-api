using PlanYourTravel.Domain.Entities.AirportAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Repositories.Abstraction;
using PlanYourTravel.Infrastructure.Persistence;

namespace PlanYourTravel.Infrastructure.Repositories
{
    public class AirportRepository(AppDbContext appDbContext) : IAirportRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        // IRepository
        public async Task AddAsync(Airport aggregate, CancellationToken cancellationToken = default)
        {
            await _appDbContext.Airports.AddAsync(aggregate, cancellationToken);
        }

        public Task UpdateAsync(Airport aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.Airports.Update(aggregate);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Airport aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.Airports.Remove(aggregate);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
