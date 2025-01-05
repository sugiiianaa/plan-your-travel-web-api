using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.Primitives;

namespace PlanYourTravel.Domain.Entities
{
    public sealed class FlightSchedule : AuditableEntity
    {
        private FlightSchedule() : base() { /* EF constructor */ }

        private FlightSchedule(
            Guid id,
            string flightNumber,
            DateTime departureDateTime,
            DateTime arrivalDateTime,
            Guid departureAirportId,
            Guid arrivalAirportId,
            Guid airlineId)
            : base(id)
        {
            FlightNumber = flightNumber;
            DepartureDateTime = departureDateTime;
            ArrivalDateTime = arrivalDateTime;
            DepartureAirportId = departureAirportId;
            ArrivalAirportId = arrivalAirportId;
            AirlineId = airlineId;
        }

        public DateTime DepartureDateTime { get; private set; }
        public DateTime ArrivalDateTime { get; private set; }

        public string FlightNumber { get; private set; }

        // Foregin Keys
        public Guid DepartureAirportId { get; private set; }
        public Guid ArrivalAirportId { get; private set; }
        public Guid AirlineId { get; private set; }

        // Navigation Properties
        public Airport DepartureAirport { get; private set; } = null!;
        public Airport ArrivalAirport { get; private set; } = null!;
        public Airline Airline { get; private set; } = null!;

        // Relationship to FlightSeatClasses
        public ICollection<FlightSeatClass> SeatClasses { get; private set; }
            = new List<FlightSeatClass>();

        public static FlightSchedule Create(
           Guid id,
            string flightNumber,
            DateTime departureDateTime,
            DateTime arrivalDateTime,
            Guid departureAirportId,
            Guid arrivalAirportId,
            Guid airlineId)
        {
            return new FlightSchedule(
                id,
                flightNumber,
                departureDateTime,
                arrivalDateTime,
                departureAirportId,
                arrivalAirportId,
                airlineId);
        }

        // Method to create new seat class configurations:
        public FlightSeatClass AddSeatClass(
            Guid seatClassId,
            SeatClassType type,
            int capacity,
            decimal price)
        {
            var seatClass = new FlightSeatClass(
                seatClassId,
                this.Id,
                type,
                capacity,
                price
            );
            SeatClasses.Add(seatClass);
            return seatClass;
        }
    }
}
