using MediatR;
using PlanYourTravel.Domain.Events;
using PlanYourTravel.Domain.Repositories;

namespace PlanYourTravel.Application.FlightTransactions.Events
{
    public class TransacrtionPaidDomainEventHandler(IFlightSeatClassRepository flightSeatClassRepository) : INotificationHandler<TransactionPaidDomainEvent>
    {
        private readonly IFlightSeatClassRepository _flightSeatClassRepository = flightSeatClassRepository;

        public async Task Handle(TransactionPaidDomainEvent notification, CancellationToken cancellationToken)
        {
            var flightSeat = await _flightSeatClassRepository
                .GetByIdAsync(notification.FlightSeatClassId, cancellationToken);

            if (flightSeat == null)
            {
                return;
            }

            flightSeat.BookSeats(notification.NumberOfSeatBooked);

            await _flightSeatClassRepository.UpdateAsync(flightSeat, cancellationToken);
            await _flightSeatClassRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
