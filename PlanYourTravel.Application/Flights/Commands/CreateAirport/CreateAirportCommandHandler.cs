using MediatR;
using PlanYourTravel.Domain.Entities.AirportAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Flights.Commands.CreateAirport
{
    public sealed class CreateAirportCommandHandler(
        ILocationRepository locationRepository,
        IAirportRepository airportRepository)
            : IRequestHandler<CreateAirportCommand, Result<Guid>>
    {
        private readonly ILocationRepository _locationRepository = locationRepository;
        private readonly IAirportRepository _airportRepository = airportRepository;

        public async Task<Result<Guid>> Handle(CreateAirportCommand command, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(command.LocationId);

            // TODO : change this error message to use DomainError
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
