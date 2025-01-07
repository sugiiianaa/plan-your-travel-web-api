using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.Primitives;

namespace PlanYourTravel.Domain.Entities
{
    public sealed class Airport : AuditableEntity
    {
        private Airport() : base()
        {
            // Ef 
        }

        private Airport(
            Guid id,
            string name,
            AirportCode code,
            Guid locationId,
            AirportFlightType flightType)
            : base(id)
        {
            Name = name;
            Code = code;
            LocationId = locationId;
            FlightType = flightType;
        }

        public string Name { get; set; } = null!;
        public AirportCode Code { get; set; }
        public AirportFlightType FlightType { get; set; }


        // Foreign Key
        public Guid LocationId { get; set; }

        // Navigation
        public Location Location { get; set; }

        public static Airport Create(
            Guid id,
            string name,
            AirportCode code,
            Guid locationId,
            AirportFlightType flightType)
        {
            return new Airport(id, name, code, locationId, flightType);
        }
    }
}
