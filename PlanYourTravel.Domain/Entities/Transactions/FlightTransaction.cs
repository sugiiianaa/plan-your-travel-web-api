using MediatR;
using PlanYourTravel.Domain.Common.Primitives;
using PlanYourTravel.Domain.Commons.Exceptions;
using PlanYourTravel.Domain.Entities.FlightScheduleAggregate;
using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.Events;

namespace PlanYourTravel.Domain.Entities.Transactions
{
    public class FlightTransaction : Transaction, IHasDomainEvents
    {
        private readonly List<INotification> _domainEvents = new();
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        private void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        private FlightTransaction() : base() { }

        private FlightTransaction(
            Guid id,
            Guid userId,
            TransactionStatus status,
            PaymentMethod paymentMethod,
            DateTime paymentDate,
            decimal totalCost,
            decimal discount,
            decimal paidAmount,
            int numberOfSeatBooked,
            Guid flightSeatClassId,
            decimal seatClassPriceAtBooking
        )
            : base(
                id,
                userId,
                status,
                paymentMethod,
                paymentDate,
                totalCost,
                discount,
                paidAmount
            )
        {
            FlightSeatClassId = flightSeatClassId;
            NumberOfSeatBooked = numberOfSeatBooked;
            SeatClassPriceAtBooking = seatClassPriceAtBooking;
        }

        // ForeignKeys
        public Guid FlightSeatClassId { get; private set; }

        // Navigation Properties
        public FlightSeatClass FlightSeatClass { get; private set; }

        public int NumberOfSeatBooked { get; private set; }

        public decimal SeatClassPriceAtBooking { get; private set; }

        public static FlightTransaction Create(
             Guid id,
             Guid userId,
             TransactionStatus status,
             PaymentMethod paymentMethod,
             DateTime paymentDate,
             decimal totalCost,
             decimal discount,
             decimal paidAmount,
             int numberOfSeatBooked,
             Guid flightSeatClassId,
             decimal seatClassPriceAtBooking
         )
        {
            return new FlightTransaction(
                id,
                userId,
                status,
                paymentMethod,
                paymentDate,
                totalCost,
                discount,
                paidAmount,
                numberOfSeatBooked,
                flightSeatClassId,
                seatClassPriceAtBooking
            );
        }

        public void MarkAsPaid(DateTime paymentDate)
        {
            if (Status != TransactionStatus.Pending)
            {
                throw new DomainException("The transaction status is not pending.");
            }

            Status = TransactionStatus.Paid;

            PaymentDate = paymentDate;

            // Raise domain event
            var paidEvent = new TransactionPaidDomainEvent(
                this.Id,
                this.FlightSeatClassId,
                this.NumberOfSeatBooked
            );

            AddDomainEvent(paidEvent);
        }

        public void MarkAsExpired()
        {
            Status = TransactionStatus.Expired;
        }
    }
}
