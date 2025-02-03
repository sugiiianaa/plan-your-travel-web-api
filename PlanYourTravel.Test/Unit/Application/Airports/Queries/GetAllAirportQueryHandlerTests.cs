using Moq;
using PlanYourTravel.Application.Airports.Queries;
using PlanYourTravel.Domain.Entities.AirportAggregate;
using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Domain.Repositories;
using Xunit;

namespace PlanYourTravel.Test.Unit.Application.Airports.Queries
{
    public class GetAllAirportQueryHandlerTests
    {
        private readonly Mock<IAirportRepository> _repositoryMock = new();
        private readonly GetAllAirportQueryHandler _handler;

        public GetAllAirportQueryHandlerTests()
        {
            _handler = new GetAllAirportQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_NoAirportsFound_ReturnsFailure()
        {
            // Arrange
            _repositoryMock.Setup(x => x.GetAllAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((new List<Airport>(), 0));

            var query = new GetAllAirportQuery(Guid.Empty, 10);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("AirportNotFound", result.Error.ErrorCode);
        }

        [Fact]
        public async Task Handle_AirportsExist_ReturnsPaginatedResult()
        {
            // Arrange
            var airportId = Guid.NewGuid();
            var locationId = Guid.NewGuid();
            var airports = new List<Airport> {
            Airport.Create(airportId, "Test", "TST", locationId, AirportFlightType.Domestic)
        };

            _repositoryMock.Setup(x => x.GetAllAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((airports, 1));

            var query = new GetAllAirportQuery(Guid.Empty, 10);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Single(result.Value.Items);
            Assert.Equal(1, result.Value.TotalCount);
            Assert.Equal(airportId, result.Value.Items[0].Id);

        }
    }
}
