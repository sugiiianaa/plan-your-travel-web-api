using MediatR;

namespace PlanYourTravel.Application.Users.Commands.LoginUser
{
    internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand>
    {
        public Task Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }
    }
}
