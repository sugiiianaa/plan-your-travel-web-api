using MediatR;
using PlanYourTravel.Application.Dtos;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.FlightTransactions.Queries.GetFlightTransaction
{
    public sealed record GetFlightTransactionQuery(
        Guid lastSeenId,
        int pageSize) : IRequest<Result<PaginatedResultDto<FlightTransactionDto>>>;
}
