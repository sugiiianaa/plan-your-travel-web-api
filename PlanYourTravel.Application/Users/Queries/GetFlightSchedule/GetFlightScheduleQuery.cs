using MediatR;
using PlanYourTravel.Domain.Dtos;
using PlanYourTravel.Domain.Shared;

namespace PlanYourTravel.Application.Users.Queries.GetFlightSchedule
{
    public sealed record GetFlightScheduleQuery(
        DateTime DepartureDate,
        string DepartureCity,
        string ArrivalCity) : IRequest<Result<List<FlightScheduleDto>>>;
}
