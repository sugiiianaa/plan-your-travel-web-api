using MediatR;
using PlanYourTravel.Domain.Entities.FlightSchedule;
using PlanYourTravel.Domain.Repositories;

namespace PlanYourTravel.Application.Flights.Commands.CreateFlightSchedule
{
    public sealed class CreateFlightScheduleCommandHandler : IRequestHandler<CreateFlightScheduleCommand, Result<List<Guid>>>
    {
        private readonly Func<IFlightRepository> _flightRepositoryFactory;


        public CreateFlightScheduleCommandHandler(IUnitOfWork unitOfWork, Func<IFlightRepository> flightRepositoryFactory)
        {
            _flightRepositoryFactory = flightRepositoryFactory;
        }

        public async Task<Result<List<Guid>>> Handle(CreateFlightScheduleCommand request, CancellationToken cancellationToken)
        {
            // A thread-safe list to store the Ids of successfully created flight schedule
            var createdIds = new List<Guid>();

            var tasks = request.FlightSchedules.Select(async schedule =>
            {
                try
                {
                    var repositoryScope = _flightRepositoryFactory();
                    var flightSchedule = FlightSchedule.Create(
                        Guid.NewGuid(),
                        schedule.FlightNumber,
                        schedule.DepartureDateTime,
                        schedule.ArrivalDateTime,
                        schedule.DepartureAirportId,
                        schedule.ArrivalAirportId,
                        schedule.AirlineId);

                    var createFlightId = await repositoryScope.CreateFlightSchedule(flightSchedule, cancellationToken);

                    // Add the created Id to the thread-safe list
                    createdIds.Add(createFlightId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Createting flight schedule for {schedule.FlightNumber}, ${ex}");
                }
            });

            await Task.WhenAll(tasks);

            return Result.Success(createdIds.ToList());
        }
    }
}
