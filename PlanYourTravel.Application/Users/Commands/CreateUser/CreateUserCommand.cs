using MediatR;
using PlanYourTravel.Domain.Shared;

namespace PlanYourTravel.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
        string Email,
        string Password,
        string FullName) : IRequest<Result>;
};
