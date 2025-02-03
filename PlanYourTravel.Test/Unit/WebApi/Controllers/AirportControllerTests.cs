using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PlanYourTravel.Application.Flights.Commands.CreateAirport;
using PlanYourTravel.Domain.Enums;
using PlanYourTravel.Shared.DataTypes;
using PlanYourTravel.WebApi.Controllers;
using PlanYourTravel.WebApi.Models.Request;
using Xunit;

namespace PlanYourTravel.Test.Unit.WebApi.Controllers
{
    public class AirportControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly AirportController _controller;

        public AirportControllerTests()
        {
            _controller = new AirportController(_mediatorMock.Object);
        }

        [Fact]
        public async Task AddAirport_ValidRequest_ReturnsCreatedResult()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateAirportCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(expectedId));

            var request = new AddAirportRequest
            {
                Name = "Test Airport",
                Code = "TST",
                LocationId = Guid.NewGuid(),
                FlightType = AirportFlightType.Domestic
            };

            // Act
            var result = await _controller.AddAirport(request, CancellationToken.None);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
        }
    }
}
