using Moq;
using PlanYourTravel.Application.Users.Commands.CreateUser;
using PlanYourTravel.Domain.Errors;
using PlanYourTravel.Domain.Repositories;
using PlanYourTravel.Domain.Services;
using PlanYourTravel.Domain.ValueObjects;
using Xunit;

namespace PlanYourTravel.Test.User
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async Task HandleShouldReturnSuccess_WhernUserIsCreated()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();

            passwordHasherMock
                .Setup(ph => ph.HashPassword(It.IsAny<string>()))
                .Returns("hashed-123");

            // Simulate "no existing user found" so that creation can happen
            userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.User)null);

            userRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            userRepositoryMock
                .Setup(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var handler = new CreateUserCommandHandler(userRepositoryMock.Object, passwordHasherMock.Object);

            var command = new CreateUserCommand("test@domain.com", "plaintextpassword", "Test User");

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess, "Expected success result when user does not exist.");
            userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<CancellationToken>()), Times.Once);
            userRepositoryMock.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenDuplicateEmailIsFound()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher>();


            // Simulate "existing user found" to test duplicate email scenario
            userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Domain.Entities.User.Create(Guid.NewGuid(), Email.Create("sugiana@tester.dev").Value, "unit_test", "sugi_unit_test", Domain.Enums.UserRole.User));

            var handler = new CreateUserCommandHandler(userRepositoryMock.Object, passwordHasherMock.Object);
            var command = new CreateUserCommand("sugiana@tester.dev", "unit_test", "sugi_unit_test");

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure, "Expected failure result when user already exists.");
            Assert.Equal(DomainErrors.User.DuplicateEmail, result.Error);

            // Ensure AddAsync is never called because we found a duplicate user
            userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Domain.Entities.User>(), It.IsAny<CancellationToken>()), Times.Never);
            userRepositoryMock.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
