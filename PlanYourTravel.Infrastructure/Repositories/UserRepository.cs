using Microsoft.EntityFrameworkCore;
using PlanYourTravel.Domain.Entities;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Infrastructure.Persistence;

namespace PlanYourTravel.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IUnitOfWork UnitOfWork => _appDbContext;

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _appDbContext.Users.AddAsync(user, cancellationToken);
        }
    }
}
