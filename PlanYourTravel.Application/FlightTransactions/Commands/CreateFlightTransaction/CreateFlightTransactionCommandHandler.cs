using MediatR;
using PlanYourTravel.Domain.Entities.Transactions;
using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Infrastructure.Services.GetCurrentUser;
using PlanYourTravel.Shared.DataTypes;

namespace PlanYourTravel.Application.FlightTransactions.Commands.CreateFlightTransaction
{
    public class CreateFlightTransactionCommandHandler(
        IGetCurrentUser getCurrentUser,
        IFlightSeatClassRepository flightSeatClassRepository,
        IFlightTransactionRepository flightTransactionRepository
        ) : IRequestHandler<CreateFlightTransactionCommand, Result<Guid>>
    {
        private readonly IGetCurrentUser _getCurrentUser = getCurrentUser;
        private readonly IFlightSeatClassRepository _flightSeatClassRepository = flightSeatClassRepository;
        private readonly IFlightTransactionRepository _flightTransactionRepository = flightTransactionRepository;

        public async Task<Result<Guid>> Handle(CreateFlightTransactionCommand request, CancellationToken cancellationToken)
        {
            var flightSeat = await _flightSeatClassRepository.GetByIdAsync(request.FlightSeatClassId, cancellationToken);

            if (flightSeat == null)
            {
                return Result.Failure<Guid>(new Error("FlightSeatNotFound", "Flight seat is not found"));
            }

            var userId = _getCurrentUser.UserId;

            if (userId == null)
            {
                return Result.Failure<Guid>(new Error("FlightSeatNotFound", "Flight seat is not found"));
            }

            var totalCost = request.NumberOfSeatBooked * flightSeat.Price;

            // TODO : add discount from promo code & seat discount
            var discount = 0;

            // TODO : add validation for paidAmount in domain level
            var paidAmount = totalCost - discount;

            var transaction = FlightTransaction.Create(
                Guid.NewGuid(),
                userId.Value,
                0,
                (PaymentMethod)request.PaymentMethod,
                DateTime.MinValue,
                totalCost,
                discount,
                paidAmount,
                request.NumberOfSeatBooked,
                request.FlightSeatClassId,
                flightSeat.Price);

            await _flightTransactionRepository.AddAsync(transaction, cancellationToken);

            await _flightTransactionRepository.SaveChangesAsync(cancellationToken);

            // TODO : add endpoint to update the transaction status
            // TODO : add logic to update seat capacity if the transaction success
            return Result.Success(transaction.Id);
        }
    }
}
