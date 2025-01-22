using MediatR;
using PlanYourTravel.Application.Dtos;
using PlanYourTravel.Domain.Entities.FlightScheduleAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Flights.Queries.GetFlightSchedule
{
    public sealed class GetFlightScheduleQueryHandler(
        IFlightScheduleRepository flightScheduleRepository)
        : IRequestHandler<GetFlightScheduleQuery, Result<PaginatedResultDto<FlightScheduleDto>>>
    {
        private readonly IFlightScheduleRepository _flightScheduleRepository = flightScheduleRepository;

        public async Task<Result<PaginatedResultDto<FlightScheduleDto>>> Handle(
            GetFlightScheduleQuery request,
            CancellationToken cancellationToken)
        {
            (IList<FlightSchedule> flightSchedules, int totalCount) = await _flightScheduleRepository.GetAllAsync(
                request.DepartureDate,
                request.DepartureAirportId,
                request.ArrivalAirportId,
                request.LastSeenId,
                request.PageSize,
                cancellationToken);

            if (flightSchedules == null || flightSchedules.Count == 0)
            {
                return Result.Failure<PaginatedResultDto<FlightScheduleDto>>(new Error("FlightScheduleNotFound"));
            }

            var lastSeenId = flightSchedules.Any() ? flightSchedules.LastOrDefault()!.Id : request.LastSeenId;

            var paginatedFlightScheduleDtos = new PaginatedResultDto<FlightScheduleDto>
            {
                Items = flightSchedules.Select(fs => new FlightScheduleDto
                {
                    Id = fs.Id,
                    FlightNumber = fs.FlightNumber,
                    DepartureDateTime = fs.DepartureDateTime,
                    ArrivalAiportId = fs.ArrivalAirportId,
                    DepartureAirportId = fs.ArrivalAirportId,
                    ArrivalDateTime = fs.ArrivalDateTime,
                    AirlineId = fs.AirlineId,
                }).ToList(),
                TotalCount = totalCount,
                LastSeenId = lastSeenId,
            };

            return Result.Success(paginatedFlightScheduleDtos);
        }
    }
}
