using PlanYourTravel.Domain.Primitives;

namespace PlanYourTravel.Domain.Entities
{
    public sealed class Location : AuditableEntity
    {
        private Location(
            Guid id,
            string country,
            string state)
            : base(id)
        {
            Country = country;
            State = state;
        }

        public string Country { get; private set; }
        public string State { get; private set; }

        public static Location Create(
            Guid id,
            string country,
            string state)
        {
            return new Location(id, country, state);
        }
    }
}
