using PlanYourTravel.Domain.Enums;

namespace PlanYourTravel.WebApi.Models
{
    public class AddAirportRequest
    {
        public required string Name { get; set; }
        public required int Code { get; set; }
        public Guid LocationId { get; set; }
        public AirportFlightType FlightType { get; set; }
    }
}
