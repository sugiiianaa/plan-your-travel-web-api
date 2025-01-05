using MediatR;
using PlanYourTravel.Domain.Errors;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Services;
using PlanYourTravel.Domain.Shared;
using PlanYourTravel.Domain.ValueObjects;

namespace PlanYourTravel.Application.Users.Commands.LoginUser
{
    public sealed class LoginUserCommandHandler
        : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
            {
                return Result.Failure<string>(emailResult.Error);
            }

            var existingUser = await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);

            if (existingUser is null)
            {
                return Result.Failure<string>(DomainErrors.User.InvalidEmailOrPassword);
            }

            var isValidPassword = _passwordHasher.VerifyPassword(existingUser.Password, request.Password);

            if (!isValidPassword)
            {
                return Result.Failure<string>(DomainErrors.User.InvalidEmailOrPassword);
            }

            var jwtToken = _jwtTokenGenerator.GenerateToken(
                existingUser.Id,
                existingUser.Email.Value,
                existingUser.Role.ToString()  // e.g. "Admin" / "User"
            );

            return Result.Success(jwtToken);
        }
    }
}
