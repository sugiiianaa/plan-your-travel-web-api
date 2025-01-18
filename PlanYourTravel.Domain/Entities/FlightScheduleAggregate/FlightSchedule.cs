using PlanYourTravel.Domain.Commons.Exceptions;
using PlanYourTravel.Domain.Commons.Primitives;
using PlanYourTravel.Domain.Entities.AirlineAggregate;
using PlanYourTravel.Domain.Entities.AirportAggregate;
using PlanYourTravel.Domain.Enums;

namespace PlanYourTravel.Domain.Entities.FlightScheduleAggregate
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

        // Foreign Keys
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

            if (arrivalDateTime <= departureDateTime)
            {
                throw new DomainException("Departure time cannot be after arrival time.");
            }

            if (arrivalAirportId == departureAirportId)
            {
                throw new DomainException("Cannot use the same airport as departure airport and arrival airport.");
            }

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
            if (capacity <= 0)
            {
                throw new DomainException("Cannot add a seat class with zero or negative capacity");
            }

            if (price < 0)
            {
                throw new DomainException("Cannot add a seat class with below zero price.");
            }

            if (DepartureDateTime < DateTime.Now || ArrivalDateTime < DateTime.Now)
            {
                throw new DomainException("Cannot add a seat class after the flight has already departerd");
            }

            var seatClass = new FlightSeatClass(
                seatClassId,
                Id,
                type,
                capacity,
                price);

            SeatClasses.Add(seatClass);

            return seatClass;
        }
    }
}
