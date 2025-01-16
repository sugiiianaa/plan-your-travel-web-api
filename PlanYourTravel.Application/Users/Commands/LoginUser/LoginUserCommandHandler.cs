using MediatR;
using PlanYourTravel.Application.Commons;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.ValueObjects;
using PlanYourTravel.Infrastructure.Services.PasswordHasher;
using PlanYourTravel.Infrastructure.Services.TokenGenerator;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Users.Commands.LoginUser
{
    public sealed class LoginUserCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher)
                : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);

            if (emailResult.IsFailure)
            {
                return Result.Failure<string>(ApplicationError.User.EmailInvalid());
            }

            var existingUser = await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);

            if (existingUser is null)
            {
                return Result.Failure<string>(ApplicationError.User.EmailNotFound());
            }

            var isValidPassword = _passwordHasher.VerifyPassword(existingUser.Password, request.Password);

            if (!isValidPassword)
            {
                return Result.Failure<string>(ApplicationError.User.EmailOrPasswordInvalid());
            }

            var jwtToken = _jwtTokenGenerator.GenerateToken(
                existingUser.Id,
                existingUser.Email.Value,
                existingUser.Role.ToString()
            );

            return Result.Success(jwtToken);
        }
    }
}
