using PlanYourTravel.Domain.Entities.AirportAggregate;
using PlanYourTravel.Domain.Enums;
using Xunit;

namespace PlanYourTravel.Test.Unit.Domain.Entities
{
    public class AirportTests
    {
        [Fact]
        public void Create_ValidParameters_CreatesAirport()
        {
            // Arrange
            var id = Guid.NewGuid();
            var locationId = Guid.NewGuid();

            // Act
            var airport = Airport.Create(id, "Test Airport", "TST", locationId, AirportFlightType.International);

            // Assert
            Assert.Equal(id, airport.Id);
            Assert.Equal("Test Airport", airport.Name);
            Assert.Equal("TST", airport.Code);
        }

        [Fact]
        public void Create_EmptyName_ThrowsException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var locationId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                Airport.Create(id, "", "TST", locationId, AirportFlightType.Domestic)
            );
        }
    }
}
