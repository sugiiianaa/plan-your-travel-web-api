namespace PlanYourTravel.Domain.Repositories.Abstraction
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
