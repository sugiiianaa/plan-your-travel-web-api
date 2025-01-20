using MediatR;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.FlightTransactions.Commands.PayFlightTransaction
{
    public sealed record PayFlightTransactionCommand(Guid TransactionId)
        : IRequest<Result>;
}
