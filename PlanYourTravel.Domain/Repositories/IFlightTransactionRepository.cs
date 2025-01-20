using PlanYourTravel.Domain.Entities.Transactions;
using PlanYourTravel.Domain.Repositories.Abstraction;

namespace PlanYourTravel.Domain.Repositories
{
    public interface IFlightTransactionRepository : IRepository<FlightTransaction>
    {
        public Task<FlightTransaction?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken);

        public Task<IList<FlightTransaction?>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken);
    }
}
