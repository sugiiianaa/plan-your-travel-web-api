using MediatR;
using PlanYourTravel.Application.Dtos;
using PlanYourTravel.Domain.Entities.AirportAggregate;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Airports.Queries
{
    public sealed class GetAllAirportQueryHandler(
        IAirportRepository airportRepository)
        : IRequestHandler<GetAllAirportQuery, Result<PaginatedResultDto<AirportDto>>>
    {
        private readonly IAirportRepository _airportRepository = airportRepository;

        public async Task<Result<PaginatedResultDto<AirportDto>>> Handle(GetAllAirportQuery query, CancellationToken cancellationToken)
        {
            (IList<Airport> airports, int totalCount) = await _airportRepository.GetAllAsync(query.lastSeenId, query.PageSize, cancellationToken);

            if (airports == null || airports.Count == 0)
            {
                return Result.Failure<PaginatedResultDto<AirportDto>>(new Error("AirportNotFound"));
            }

            var lastSeenId = airports.Any() ? airports.Last().Id : query.lastSeenId;

            var paginatedAirportDtos = new PaginatedResultDto<AirportDto>
            {
                Items = airports.Select(a => new AirportDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Code = a.Code,
                    FlightType = a.FlightType,
                }).ToList(),
                TotalCount = totalCount,
                LastSeenId = lastSeenId
            };

            return Result.Success(paginatedAirportDtos);
        }
    }
}
