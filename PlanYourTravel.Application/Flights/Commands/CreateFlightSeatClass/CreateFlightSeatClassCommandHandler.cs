using MediatR;
using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Flights.Commands.CreateFlightSeatClass
{
    public class CreateFlightSeatClassCommandHandler(
        IFlightSeatClassRepository flightSeatClassRepository,
        IFlightScheduleRepository flightScheduleRepository)
        : IRequestHandler<CreateFlightSeatClassCommand, Result<List<Guid>>>
    {
        private readonly IFlightSeatClassRepository _flightSeatClassRepository = flightSeatClassRepository;
        private readonly IFlightScheduleRepository _flightScheduleRepository = flightScheduleRepository;
        public async Task<Result<List<Guid>>> Handle(CreateFlightSeatClassCommand request, CancellationToken cancellationToken)
        {
            var flightSchedule = await _flightScheduleRepository.GetByIdAsync(request.FlightScheduleId, cancellationToken);

            if (flightSchedule == null)
            {
                return Result.Failure<List<Guid>>(new Error("FlightScheduleNotFound"));
            }

            if (request.FlightSeatClassItem is null || request.FlightSeatClassItem.Count == 0)
            {
                return Result.Failure<List<Guid>>(new Error("FlightSeatClassRequestDataIsNull"));
            }

            var seatClassIds = new List<Guid>();

            foreach (var seatClass in request.FlightSeatClassItem)
            {
                var flightSeatClass = flightSchedule.AddSeatClass(
                    Guid.NewGuid(),
                    (SeatClassType)seatClass.SeatClassType,
                    seatClass.Capacity,
                    seatClass.Price);

                await _flightSeatClassRepository.AddAsync(flightSeatClass, cancellationToken);

                seatClassIds.Add(flightSeatClass.Id);
            }

            await _flightSeatClassRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(seatClassIds);
        }
    }
}
