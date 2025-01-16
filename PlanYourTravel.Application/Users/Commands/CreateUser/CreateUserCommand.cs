using MediatR;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
        string Email,
        string Password,
        string FullName) : IRequest<Result<Guid>>;
};
