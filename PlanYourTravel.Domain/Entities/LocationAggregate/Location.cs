using PlanYourTravel.Domain.Commons.Primitives;

namespace PlanYourTravel.Domain.Entities.Location
{
    public sealed class Location : AuditableEntity
    {
        // Private constructor for EF
        private Location() { }

        private Location(Guid id, string country, string state) : base(id)
        {
            Country = country;
            State = state;
        }

        public string Country { get; private set; }
        public string State { get; private set; }

        public static Location Create(Guid id, string country, string state)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Location ID cannot be empty");
            }
            return new Location(id, country, state);
        }

        public void UpdateState(string newState)
        {
            if (string.IsNullOrEmpty(newState))
            {
                throw new ArgumentException("State cannot be empty.");
            }
            State = newState;
        }

        public void UpdateCountry(string newCountry)
        {
            if (string.IsNullOrEmpty(newCountry))
            {
                throw new ArgumentException("Country cannot be empty.");
            };
            Country = newCountry;
        }
    }
}
