using MediatR;
using PlanYourTravel.Domain.Entities.FlightScheduleAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Flights.Commands.CreateFlightSchedule
{
    public sealed class CreateFlightScheduleCommandHandler(IFlightScheduleRepository flightScheduleRepository)
        : IRequestHandler<CreateFlightScheduleCommand, Result<List<Guid>>>
    {
        private readonly IFlightScheduleRepository _flightScheduleRepository = flightScheduleRepository;

        public async Task<Result<List<Guid>>> Handle(CreateFlightScheduleCommand request, CancellationToken cancellationToken)
        {
            if (request.FlightSchedules is null || request.FlightSchedules.Count == 0)
            {
                return Result.Failure<List<Guid>>(new Error("FlightScheduleNotFound"));
            }

            var createdIds = new List<Guid>();

            foreach (var item in request.FlightSchedules)
            {
                var flightSchedule = FlightSchedule.Create(
                    Guid.NewGuid(),
                    item.FlightNumber,
                    item.DepartureDateTime,
                    item.ArrivalDateTime,
                    item.DepartureAirportId,
                    item.ArrivalAirportId,
                    item.AirlineId);

                await _flightScheduleRepository.AddAsync(flightSchedule, cancellationToken);

                createdIds.Add(flightSchedule.Id);
            }

            await _flightScheduleRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(createdIds);
        }
    }
}
