using MediatR;
using PlanYourTravel.Domain.Entities;

namespace PlanYourTravel.Application.Users.Commands.CreateUser
{
    internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        public Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var user = User.Create(
                Guid.NewGuid(),
                request.Email,
                request.Password,
                request.FullName,
                Domain.Enums.UserRole.User);

            throw new NotImplementedException();
        }
    }
}
