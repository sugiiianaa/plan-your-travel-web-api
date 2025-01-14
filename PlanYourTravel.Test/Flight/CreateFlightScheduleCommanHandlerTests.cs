using Moq;
using PlanYourTravel.Application.Flights.Commands.CreateFlightSchedule;
using PlanYourTravel.Domain.Entities.FlightSchedule;
using PlanYourTravel.Domain.Repositories;
using Xunit;

namespace PlanYourTravel.Test.Flight
{
    public class CreateFlightScheduleCommanHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnCreatedIds_WhenAllFlightSchedulesAreValid()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockFlightRepository = new Mock<IFlightRepository>();
            mockFlightRepository
                .Setup(repo => repo.CreateFlightSchedule(It.IsAny<FlightSchedule>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.NewGuid);

            var handler = new CreateFlightScheduleCommandHandler(
                mockUnitOfWork.Object,
                () => mockFlightRepository.Object);

            var command = new CreateFlightScheduleCommand(new List<CreateFlightScheduleItem>
            {
                new("FL001", DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()),
                new("FL002", DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(3), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid())
            });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            mockFlightRepository.Verify(repo => repo.CreateFlightSchedule(It.IsAny<FlightSchedule>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoFlightSchedulesAreProvided()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockFlightRepository = new Mock<IFlightRepository>();

            var handler = new CreateFlightScheduleCommandHandler(
                mockUnitOfWork.Object,
                () => mockFlightRepository.Object);

            var command = new CreateFlightScheduleCommand(new List<CreateFlightScheduleItem>());

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
            mockFlightRepository.Verify(repo => repo.CreateFlightSchedule(It.IsAny<FlightSchedule>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldHandleExceptionsAndContinueProcessing()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockFlightRepository = new Mock<IFlightRepository>();
            mockFlightRepository
                .SetupSequence(repo => repo.CreateFlightSchedule(It.IsAny<FlightSchedule>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("Test Exception"))
                .ReturnsAsync(Guid.NewGuid);

            var handler = new CreateFlightScheduleCommandHandler(
                mockUnitOfWork.Object,
                () => mockFlightRepository.Object);

            var command = new CreateFlightScheduleCommand(new List<CreateFlightScheduleItem>
            {
                new("FL001", DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()),
                new("FL002", DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(3), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid())
            });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Single(result.Value); // Only one schedule succeeded
            mockFlightRepository.Verify(repo => repo.CreateFlightSchedule(It.IsAny<FlightSchedule>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }
    }
}
