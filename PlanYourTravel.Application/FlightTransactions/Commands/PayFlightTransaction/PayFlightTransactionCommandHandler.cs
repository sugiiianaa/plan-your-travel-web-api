using MediatR;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.FlightTransactions.Commands.PayFlightTransaction
{
    public class PayFlightTransactionCommandHandler(
        IFlightTransactionRepository flightTransactionRepository) : IRequestHandler<PayFlightTransactionCommand, Result>
    {
        private readonly IFlightTransactionRepository _flightTransactionRepository = flightTransactionRepository;

        public async Task<Result> Handle(PayFlightTransactionCommand request, CancellationToken cancellationToken)
        {
            var flightTransaction = await _flightTransactionRepository.GetByIdAsync(request.TransactionId, cancellationToken);

            if (flightTransaction == null)
            {
                return Result.Failure(new Error("FlightTransactionNotFound", $"Flight with id {request.TransactionId} not found"));
            }

            flightTransaction.MarkAsPaid(DateTime.UtcNow);

            await _flightTransactionRepository.UpdateAsync(flightTransaction, cancellationToken);
            await _flightTransactionRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
