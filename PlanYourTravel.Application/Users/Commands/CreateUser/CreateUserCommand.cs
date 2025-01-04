using MediatR;

namespace PlanYourTravel.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
        Guid UserId,
        string Email,
        string Password,
        string FullName) : IRequest;
}
