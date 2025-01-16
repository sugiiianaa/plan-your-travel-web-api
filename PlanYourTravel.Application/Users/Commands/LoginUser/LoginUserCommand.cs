using MediatR;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Users.Commands.LoginUser
{
    public sealed record LoginUserCommand(
        string Email,
        string Password) : IRequest<Result<string>>;
}
