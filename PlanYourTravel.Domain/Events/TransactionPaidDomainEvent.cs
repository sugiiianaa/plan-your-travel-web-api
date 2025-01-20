using MediatR;

namespace PlanYourTravel.Domain.Events
{
    public class TransactionPaidDomainEvent : INotification
    {
        public Guid TransactionId { get; }
        public Guid FlightSeatClassId { get; }
        public int NumberOfSeatBooked { get; }

        public TransactionPaidDomainEvent(Guid transactionId, Guid flightSeatClassId, int numberOfSeatBooked)
        {
            TransactionId = transactionId;
            FlightSeatClassId = flightSeatClassId;
            NumberOfSeatBooked = numberOfSeatBooked;
        }
    }
}
