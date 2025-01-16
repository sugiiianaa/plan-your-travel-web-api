﻿using Microsoft.EntityFrameworkCore;
using PlanYourTravel.Domain.Entities.User;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.ValueObjects;
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

        public async Task AddAsync(User aggregate, CancellationToken cancellationToken = default)
        {
            await _appDbContext.Users.AddAsync(aggregate, cancellationToken);
        }

        public Task UpdateAsync(User aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.Users.Update(aggregate);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(User aggregate, CancellationToken cancellationToken = default)
        {
            _appDbContext.Users.Remove(aggregate);
            return Task.CompletedTask;
        }
        public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
        {
            return await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Email.Value == email.Value, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
