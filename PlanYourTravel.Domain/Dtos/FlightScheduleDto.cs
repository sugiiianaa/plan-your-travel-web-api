namespace PlanYourTravel.Domain.Dtos
{
    public class FlightScheduleDto
    {

        private FlightScheduleDto(
            Guid id,
            string flightNumber,
            DateTime departureDateTime,
            DateTime arrivalDateTime,
            Guid departureAirportId,
            Guid arrivalAirportId,
            Guid airlineId)
        {
            Id = id;
            FlightNumber = flightNumber;
            DepartureDateTime = departureDateTime;
            ArrivalDateTime = arrivalDateTime;
            DepartureAirportId = departureAirportId;
            ArrivalAirportId = arrivalAirportId;
            AirlineId = airlineId;
        }

        public Guid Id { get; set; }
        public DateTime DepartureDateTime { get; private set; }
        public DateTime ArrivalDateTime { get; private set; }
        public string FlightNumber { get; private set; }
        public Guid DepartureAirportId { get; private set; }
        public Guid ArrivalAirportId { get; private set; }
        public Guid AirlineId { get; private set; }

        public static FlightScheduleDto Create(
            Guid id,
            string flightNumber,
            DateTime departureDateTime,
            DateTime arrivalDateTime,
            Guid departureAirportId,
            Guid arrivalAirportId,
            Guid airlineId)
        {
            return new FlightScheduleDto(
                id,
                flightNumber,
                departureDateTime,
                arrivalDateTime,
                departureAirportId,
                arrivalAirportId,
                airlineId);
        }

    }
}
