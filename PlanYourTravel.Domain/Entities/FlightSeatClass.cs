using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.Primitives;

namespace PlanYourTravel.Domain.Entities
{
    public class FlightSeatClass : AuditableEntity
    {
        private FlightSeatClass() : base() { }

        public FlightSeatClass(
            Guid id,
            Guid flightScheduleId,
            SeatClassType seatClassType,
            int capacity,
            decimal price)
            : base(id)
        {
            FlightScheduleId = flightScheduleId;
            SeatClassType = seatClassType;
            Capacity = capacity;
            Price = price;
            SeatsBooked = 0;
        }

        public Guid FlightScheduleId { get; private set; }
        public SeatClassType SeatClassType { get; private set; }
        public int Capacity { get; private set; }
        public int SeatsBooked { get; private set; }
        public decimal Price { get; private set; }

        public void BookSeats(int count)
        {
            if (count < 0) throw new ArgumentException("Cannot book negative seats");
            if (SeatsBooked + count > Capacity)
            {
                throw new InvalidOperationException("Not enough seats available in this class");
            }
            SeatsBooked += count;
        }

        public void CancelSeats(int count)
        {
            if (count < 0) throw new ArgumentException("Cannot unbook negative seats");
            if (SeatsBooked - count < 0)
            {
                throw new InvalidOperationException("Cannot have negative seats booked");
            }
            SeatsBooked -= count;
        }
    }
}
