using PlanYourTravel.Domain.Entities.AirportAggregate;
using PlanYourTravel.Domain.Entities.Location;

namespace PlanYourTravel.Domain.Services
{
    public static class AirportLocationDomainService
    {
        public static void ValidateAiportIsInSameCountry(Airport airport, Location location)
        {
            if (airport.LocationId != location.Id)
            {
                throw new InvalidOperationException("Airport does not match the provided location.");
            }
        }
    }
}
