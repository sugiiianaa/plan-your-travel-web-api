using Microsoft.EntityFrameworkCore;
using PlanYourTravel.Domain.Entities.LocationAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Repositories.Abstraction;
using PlanYourTravel.Infrastructure.Persistence;

namespace PlanYourTravel.Infrastructure.Repositories
{
    public class LocationRepository(AppDbContext appDbContext) : ILocationRepository
    {
        public readonly AppDbContext _appDbContext = appDbContext;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task AddAsync(Location aggregate, CancellationToken cancellationToken = default)
        {
            await _appDbContext.Locations.AddAsync(aggregate, cancellationToken);
        }
        public Task UpdateAsync(Location aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.Locations.Update(aggregate);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Location aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.Locations.Remove(aggregate);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Location?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _appDbContext
                .Locations.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

        }

    }
}
