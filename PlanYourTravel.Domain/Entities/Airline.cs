using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.Primitives;

namespace PlanYourTravel.Domain.Entities
{
    public sealed class Airline : AuditableEntity
    {
        private Airline() : base()
        {
            // Ef
        }

        private Airline(
            Guid id,
            string name,
            AirlineCode airlineCode)
                : base(id)
        {
            Name = name;
            AirlineCode = airlineCode;
        }

        public string Name { get; private set; } = null!;
        public AirlineCode AirlineCode { get; private set; }


        public static Airline Create(
            Guid id,
            string name,
            AirlineCode airlineCode)
        {
            return new Airline(id, name, airlineCode);
        }
    }
}
