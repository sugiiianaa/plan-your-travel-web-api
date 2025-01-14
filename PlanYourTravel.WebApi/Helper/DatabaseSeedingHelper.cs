using PlanYourTravel.Domain.Entities.Airline;
using PlanYourTravel.Domain.Entities.Location;
using PlanYourTravel.Infrastructure.Persistence;

namespace PlanYourTravel.WebApi.Helper
{
    public class DatabaseSeedingHelper
    {
        public static async Task SeedData(AppDbContext dbContext)
        {
            // Guid for location
            // so another entity that reference will be able to use it
            var jakartaGuid = Guid.NewGuid();
            var singaporeGuid = Guid.NewGuid();
            var auklandGuid = Guid.NewGuid();
            var albertaGuid = Guid.NewGuid();

            Console.WriteLine($"data from dbcontext : {!dbContext.Airlines.Any()} ");

            if (!dbContext.Airlines.Any())
            {
                Console.WriteLine("Seeding data : Airline");
                var garudaIndonesia = Airline.Create(Guid.NewGuid(), "Garuda Indonesia", Domain.Enums.AirlineCode.GA);
                var singaporeAirline = Airline.Create(Guid.NewGuid(), "Singapore Airline", Domain.Enums.AirlineCode.SQ);
                var airNewZealand = Airline.Create(Guid.NewGuid(), "Air New Zealand", Domain.Enums.AirlineCode.NZ);
                var airCanada = Airline.Create(Guid.NewGuid(), "Air Canada", Domain.Enums.AirlineCode.AC);

                dbContext.Airlines.AddRange(garudaIndonesia, singaporeAirline, airNewZealand, airCanada);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Locations.Any())
            {
                Console.WriteLine("Seeding data : Location");

                var jakarta = Location.Create(jakartaGuid, "Indonesia", "Jakarta");
                var airportBlvd = Location.Create(singaporeGuid, "Singapore", "Airport Blvd");
                var aukland = Location.Create(auklandGuid, "New Zealand", "Auckland");
                var alberta = Location.Create(albertaGuid, "England", "London");

                dbContext.Locations.AddRange(jakarta, airportBlvd, aukland, alberta);
                await dbContext.SaveChangesAsync();

            }

            if (!dbContext.Airports.Any())
            {
                Console.WriteLine("Seeding data : Airport");
                if (!dbContext.Locations.Any())
                {
                    Console.WriteLine("Can't seed airport data due to missing location data.");
                }
                else
                {
                    var soekarnoehatta = Airport.Create(Guid.NewGuid(), "Soekarno-Hatta International Airport", Domain.Enums.AirportCode.CGK, jakartaGuid, Domain.Enums.AirportFlightType.International);
                    var changi = Airport.Create(Guid.NewGuid(), "Singapore Changi Airport", Domain.Enums.AirportCode.SIN, singaporeGuid, Domain.Enums.AirportFlightType.International);
                    var aukland = Airport.Create(Guid.NewGuid(), "Auckland Airport", Domain.Enums.AirportCode.AKL, auklandGuid, Domain.Enums.AirportFlightType.International);
                    var edmonton = Airport.Create(Guid.NewGuid(), "Edmonton International Airport", Domain.Enums.AirportCode.YEG, albertaGuid, Domain.Enums.AirportFlightType.International);
                    dbContext.Airports.AddRange(soekarnoehatta, changi, aukland, edmonton);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
