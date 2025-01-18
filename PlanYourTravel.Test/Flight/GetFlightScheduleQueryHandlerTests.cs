//using Moq;
//using PlanYourTravel.Application.Flights.Queries.GetFlightSchedule;
//using PlanYourTravel.Domain.Dtos;
//using PlanYourTravel.Domain.Errors;
//using PlanYourTravel.Domain.Repositories;
//using Xunit;

//namespace PlanYourTravel.Test.Flight
//{
//    public class GetFlightScheduleQueryHandlerTests
//    {
//        [Fact]
//        public async Task Handle_ShouldReturnListOfSchedules_WhenScheduleExist()
//        {
//            // Arrange
//            var flightRepositoryMock = new Mock<IFlightRepository>();

//            var departureDate = new DateTime(2025, 01, 08);
//            var arrivalDate = new DateTime(2025, 01, 08);
//            var departureAirportId = Guid.NewGuid();
//            var arrivalAirportId = Guid.NewGuid();
//            var airlineId = Guid.NewGuid();

//            // Mock returned flight schedules
//            var expectedSchedules = new List<FlightScheduleDto>
//            {
//                FlightScheduleDto.Create(
//                    Guid.NewGuid(),
//                    "UNIT-TEST-123",
//                    departureDate,
//                    arrivalDate,
//                    departureAirportId,
//                    arrivalAirportId,
//                    airlineId)
//            };

//            flightRepositoryMock
//                .Setup(repo => repo.GetFlightSchedule(
//                    departureDate,
//                    departureAirportId,
//                    arrivalAirportId,
//                    It.IsAny<CancellationToken>()))
//                .ReturnsAsync(expectedSchedules);

//            var handler = new GetFlightScheduleQueryHandler(flightRepositoryMock.Object);

//            // Act
//            var query = new GetFlightScheduleQuery(departureDate, departureAirportId, arrivalAirportId);
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.True(result.IsSuccess);
//            Assert.Equal(expectedSchedules.Count, result.Value.Count);
//            Assert.Equal("UNIT-TEST-123", result.Value[0].FlightNumber);
//            Assert.Equal(departureDate, result.Value[0].DepartureDateTime);

//            // Could also check the first item, etc.
//            flightRepositoryMock.Verify(repo => repo.GetFlightSchedule(
//                departureDate,
//                departureAirportId,
//                arrivalAirportId,
//                It.IsAny<CancellationToken>()),
//                Times.Once);

//        }

//        [Fact]
//        public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
//        {
//            // Arrange
//            var flightRepositoryMock = new Mock<IFlightRepository>();

//            flightRepositoryMock
//                .Setup(repo => repo.GetFlightSchedule(
//                    It.IsAny<DateTime>(),
//                    It.IsAny<Guid>(),
//                    It.IsAny<Guid>(),
//                    It.IsAny<CancellationToken>()))
//                .ThrowsAsync(new Exception(DomainErrors.Generic.InternalServerError.Message));

//            var handler = new GetFlightScheduleQueryHandler(flightRepositoryMock.Object);

//            // Act
//            var query = new GetFlightScheduleQuery(DateTime.Now, Guid.NewGuid(), Guid.NewGuid());
//            var result = await handler.Handle(query, CancellationToken.None);

//            // Assert
//            Assert.True(result.IsFailure);
//            Assert.Equal(DomainErrors.Generic.InternalServerError.Message, result.Error.Message);
//        }
//    }
//}
