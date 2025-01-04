using MediatR;

namespace PlanYourTravel.Application.Users.Commands.LoginUser
{
    public sealed class LoginUserCommand(
        string Email,
        string Password) : IRequest;
}
