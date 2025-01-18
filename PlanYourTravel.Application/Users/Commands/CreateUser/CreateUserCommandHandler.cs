using MediatR;
using PlanYourTravel.Application.Commons;
using PlanYourTravel.Domain.Entities.UserAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.ValueObjects;
using PlanYourTravel.Infrastructure.Services.PasswordHasher;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
                : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
            {
                return Result.Failure<Guid>(ApplicationError.User.EmailInvalid());
            }

            var existingUser = await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);

            if (existingUser is not null)
            {
                return Result.Failure<Guid>(ApplicationError.User.EmailAlreadyRegistered());
            }

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var user = User.Create(
                Guid.NewGuid(),
                emailResult.Value,
                hashedPassword,
                request.FullName,
                Domain.Enums.UserRole.User);

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(user.Id);
        }
    }
}
