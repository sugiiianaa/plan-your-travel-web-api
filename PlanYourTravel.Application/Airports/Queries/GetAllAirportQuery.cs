﻿using MediatR;
using PlanYourTravel.Application.Dtos;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.Airports.Queries
{
    public sealed record GetAllAirportQuery(Guid LastSeenId, int PageSize) : IRequest<Result<PaginatedResultDto<AirportDto>>>;
}
