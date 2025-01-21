using MediatR;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.FlightTransactions.Commands.PayFlightTransaction
{
    public class PayFlightTransactionCommandHandler(
        IFlightTransactionRepository flightTransactionRepository,
        IFlightSeatClassRepository flightSeatClassRepository) : IRequestHandler<PayFlightTransactionCommand, Result>
    {
        private readonly IFlightTransactionRepository _flightTransactionRepository = flightTransactionRepository;
        private readonly IFlightSeatClassRepository _flightSeatClassRepository = flightSeatClassRepository;

        public async Task<Result> Handle(PayFlightTransactionCommand request, CancellationToken cancellationToken)
        {
            var flightTransaction = await _flightTransactionRepository.GetByIdAsync(request.TransactionId, cancellationToken);

            if (flightTransaction == null)
            {
                return Result.Failure(new Error("FlightTransactionNotFound"));
            }

            var flightSeat = await _flightSeatClassRepository.GetByIdAsync(flightTransaction.FlightSeatClassId, cancellationToken);

            if (flightSeat == null)
            {
                return Result.Failure(new Error("FlightSeatNotFound"));
            }

            var seatBookedAfterTransaction = flightSeat.SeatsBooked + flightTransaction.NumberOfSeatBooked;

            if (seatBookedAfterTransaction > flightSeat.Capacity)
            {
                return Result.Failure(new Error("FlightSeatSoldOut"));
            }

            flightTransaction.MarkAsPaid(DateTime.UtcNow);

            await _flightTransactionRepository.UpdateAsync(flightTransaction, cancellationToken);
            await _flightTransactionRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
