namespace PlanYourTravel.Domain.Repositories.Abstraction
{
    public interface IRepository<TAggregateRoot>
        where TAggregateRoot : class
    {
        IUnitOfWork UnitOfWork { get; }

        Task AddAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default);
        Task UpdateAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default);
        Task DeleteAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
