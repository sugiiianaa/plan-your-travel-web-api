using MediatR;
using PlanYourTravel.Domain.Entities;
using PlanYourTravel.Domain.Errors;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Shared;

namespace PlanYourTravel.Application.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (existingUser is not null)
            {
                return Result.Failure(DomainErrors.User.DuplicateEmail);
            }

            var user = User.Create(
                Guid.NewGuid(),
                request.Email,
                request.Password,
                request.FullName,
                Domain.Enums.UserRole.User);

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
