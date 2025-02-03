using MediatR;
using PlanYourTravel.Application.Dtos;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.FlightTransactions.Queries.GetFlightTransaction
{
    public sealed record GetFlightTransactionQuery(
        Guid LastSeenId,
        int PageSize) : IRequest<Result<PaginatedResultDto<FlightTransactionDto>>>;
}
