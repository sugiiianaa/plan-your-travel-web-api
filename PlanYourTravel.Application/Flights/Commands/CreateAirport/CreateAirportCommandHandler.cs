using MediatR;
using PlanYourTravel.Application.Commons;
using PlanYourTravel.Domain.Entities.AirportAggregate;
using PlanYourTravel.Domain.Repositories;

namespace PlanYourTravel.Application.Flights.Commands.CreateAirport
{
    public sealed class CreateAirportCommandHandler
        : IRequestHandler<CreateAirportCommand, Result<Guid>>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IAirportRepository _airportRepository;

        public CreateAirportCommandHandler(ILocationRepository locationRepository, IAirportRepository airportRepository)
        {
            _locationRepository = locationRepository;
            _airportRepository = airportRepository;
        }

        public async Task<Result<Guid>> Handle(CreateAirportCommand command, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(command.LocationId);

            if (location == null)
            {
                return Result.Failure<Guid>(new Error(
                    "LocationNotFound",
                    "The specified location does not exists"));
            };

            var airport = Airport.Create(
                Guid.NewGuid(),
                command.Name,
                command.Code,
                command.LocationId,
                command.FlightType);

            await _airportRepository.AddAsync(airport, cancellationToken);

            return Result.Success(airport.Id);
        }
    }
}
