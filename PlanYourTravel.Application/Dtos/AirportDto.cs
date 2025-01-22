using PlanYourTravel.Domain.Enums;

namespace PlanYourTravel.Application.Dtos
{
    public class AirportDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public AirportFlightType FlightType { get; set; }
    }
}
