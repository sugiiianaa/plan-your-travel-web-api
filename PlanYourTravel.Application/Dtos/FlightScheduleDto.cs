namespace PlanYourTravel.Application.Dtos
{
    public class FlightScheduleDto
    {
        public Guid Id { get; set; }

        public required string FlightNumber { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public DateTime ArrivalDateTime { get; set; }

        public Guid DepartureAirportId { get; set; }

        public Guid ArrivalAiportId { get; set; }

        public Guid AirlineId { get; set; }
    }
}
