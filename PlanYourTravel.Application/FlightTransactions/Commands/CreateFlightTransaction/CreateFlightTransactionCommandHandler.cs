using Hangfire;
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
                return Result.Failure<Guid>(new Error("FlightSeatNotFound"));
            }

            var seatsBookedAfterTransaction = flightSeat.SeatsBooked + request.NumberOfSeatBooked;

            if (seatsBookedAfterTransaction > flightSeat.Capacity)
            {
                return Result.Failure<Guid>(new Error("BookingExceedCapacity"));
            }

            var userId = _getCurrentUser.UserId;

            if (userId == null)
            {
                return Result.Failure<Guid>(new Error("FlightSeatNotFound"));
            }

            var totalCost = request.NumberOfSeatBooked * flightSeat.Price;

            // TODO : add discount from promo code & seat discount
            var discount = 0;

            // TODO : add validation for paidAmount in domain level
            var paidAmount = totalCost - discount;

            // TODO : apply background process to expire the transaction after 24 h
            // you could use hangfire library
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

            var client = new BackgroundJobClient();

            client.Schedule(() => MarkTransactionAsExpired(transaction.Id, default), TimeSpan.FromSeconds(10));

            return Result.Success(transaction.Id);
        }

        public async Task<bool> MarkTransactionAsExpired(Guid transactionId, CancellationToken cancellationToken)
        {
            var transaction = await _flightTransactionRepository.GetByIdAsync(transactionId, cancellationToken);

            if (transaction == null)
            {
                return false;
            }

            transaction?.MarkAsExpired();

            await _flightTransactionRepository.UpdateAsync(transaction!, cancellationToken);

            await _flightTransactionRepository.SaveChangesAsync(cancellationToken);

            Console.WriteLine("--- Hangfire task for MarkTransactionAsExpired fired!");

            return true;
        }
    }
}
