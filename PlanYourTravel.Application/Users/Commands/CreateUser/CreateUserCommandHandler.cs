using MediatR;
using PlanYourTravel.Domain.Entities;
using PlanYourTravel.Domain.Errors;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Services;
using PlanYourTravel.Domain.Shared;
using PlanYourTravel.Domain.ValueObjects;

namespace PlanYourTravel.Application.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
            {
                return Result.Failure(emailResult.Error);
            }

            var existingUser = await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);

            if (existingUser is not null)
            {
                return Result.Failure(DomainErrors.User.DuplicateEmail);
            }

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var user = User.Create(
                Guid.NewGuid(),
                emailResult.Value,
                hashedPassword,
                request.FullName,
                Domain.Enums.UserRole.User);

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
