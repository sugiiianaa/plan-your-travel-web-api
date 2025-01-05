using MediatR;
using PlanYourTravel.Domain.Shared;

namespace PlanYourTravel.Application.Users.Commands.LoginUser
{
    public sealed record LoginUserCommand(
        string Email,
        string Password) : IRequest<Result<string>>;
}
