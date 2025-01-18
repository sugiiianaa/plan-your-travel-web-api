using MediatR;
using PlanYourTravel.Application.Dtos;
using PlanYourTravel.Domain.Entities.FlightScheduleAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Flights.Queries.GetFlightSchedule
{
    public sealed class GetFlightScheduleQueryHandler(
        IFlightScheduleRepository flightScheduleRepository)
                : IRequestHandler<GetFlightScheduleQuery, Result<FlightSchedulesPageDto>>
    {
        private readonly IFlightScheduleRepository _flightScheduleRepository = flightScheduleRepository;

        public async Task<Result<FlightSchedulesPageDto>> Handle(
            GetFlightScheduleQuery request,
            CancellationToken cancellationToken)
        {
            (IList<FlightSchedule> flightSchedules, int totalCount) = await _flightScheduleRepository.GetAllAsync(
                request.DepartureDate,
                request.DepartureAirportId,
                request.ArrivalAirportId,
                request.LastSeendId,
                request.PageSize,
                cancellationToken);

            var lastSeenId = flightSchedules.Any() ? flightSchedules.LastOrDefault()!.Id : request.LastSeendId;

            var flightScheduleDtos = new FlightSchedulesPageDto
            {
                Schedules = flightSchedules,
                TotalCount = totalCount,
                NextLastSeenId = lastSeenId,
            };

            return Result.Success(flightScheduleDtos);
        }
    }
}
