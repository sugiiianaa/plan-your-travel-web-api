using Microsoft.EntityFrameworkCore;
using PlanYourTravel.Domain.Entities.Transactions;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Repositories.Abstraction;
using PlanYourTravel.Infrastructure.Persistence;

namespace PlanYourTravel.Infrastructure.Repositories
{
    public class FlightTransactionRepository(AppDbContext appDbContext) : IFlightTransactionRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        // IRepository 
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task AddAsync(FlightTransaction aggregate, CancellationToken cancellationToken = default)
        {
            await _appDbContext.FlightTransactions.AddAsync(aggregate, cancellationToken);
        }

        public Task UpdateAsync(FlightTransaction aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.FlightTransactions.Update(aggregate);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(FlightTransaction aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.FlightTransactions.Remove(aggregate);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        // Extentions
        public async Task<FlightTransaction?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _appDbContext.FlightTransactions
                .FirstOrDefaultAsync(ft => ft.Id == id, cancellationToken);
        }

        public async Task<(IList<FlightTransaction> items, int totalCount)> GetAllByUserIdAsync(
            Guid userId,
            Guid lastSeenId,
            int pageSize,
            CancellationToken cancellationToken)
        {
            IQueryable<FlightTransaction> query = _appDbContext.FlightTransactions
                .Where(ft => ft.UserId == userId);

            int totalCount = -1;

            if (lastSeenId != Guid.Empty)
            {
                query = query.Where(ft => ft.Id.CompareTo(lastSeenId) > 0);
            }
            else
            {
                totalCount = await query.CountAsync(cancellationToken);
            }

            query = query
                .OrderBy(ft => ft.Id)
                .Take(pageSize);

            var items = await query.ToListAsync(cancellationToken);

            return (items, totalCount);
        }

    }
}
