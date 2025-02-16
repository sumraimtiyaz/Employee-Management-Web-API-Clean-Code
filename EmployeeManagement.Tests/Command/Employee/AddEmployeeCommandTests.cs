using Moq;
using EmployeeManagement.Application.Command.Employee;
using EmployeeManagement.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using Entities = EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Tests.Commands.Employee
{
    [TestFixture]
    public class AddEmployeeCommandTests
    {
        private Mock<IEmployeeRepository> _mockRepository;
        private Mock<ILogger<AddEmployeeCommand.Handler>> _mockLogger;
        private AddEmployeeCommand.Handler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _mockLogger = new Mock<ILogger<AddEmployeeCommand.Handler>>();
            _handler = new AddEmployeeCommand.Handler(_mockRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Handle_ValidEmployee_ReturnsEmployeeId()
        {
            // Arrange
            var command = new AddEmployeeCommand.Command
            {
                Name = "Test Doe",
                Email = "Test@example.com",
                Department = "HR",
                Salary = 50000
            };

            var addedEmployee = new Entities.Employee
            {
                Id = 1, // Simulating returned ID after insertion
                Name = command.Name,
                Email = command.Email,
                Department = command.Department,
                Salary = command.Salary
            };

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Entities.Employee>()))
                           .Callback<Entities.Employee>(e => e.Id = addedEmployee.Id)
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(addedEmployee.Id));
            _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Entities.Employee>()), Times.Once);
        }

        [Test]
        public void Handle_RepositoryThrowsException_ThrowsExceptionAndLogsError()
        {
            // Arrange
            var command = new AddEmployeeCommand.Command
            {
                Name = "Jane Doe",
                Email = "jane@example.com",
                Department = "IT",
                Salary = 60000
            };

            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Entities.Employee>()))
                           .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Database error"));

            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((obj, type) => obj.ToString().Contains("Error while adding employee")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
