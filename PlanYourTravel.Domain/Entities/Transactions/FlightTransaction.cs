using PlanYourTravel.Domain.Entities.FlightSchedule;
using PlanYourTravel.Domain.Enums;

namespace PlanYourTravel.Domain.Entities.Transactions
{
    public class FlightTransaction : Transaction
    {
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
    }
}
