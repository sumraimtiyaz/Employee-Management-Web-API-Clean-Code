using NUnit.Framework;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Application.Command.Employee;
using EmployeeManagement.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using Entities = EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Tests.Commands.Employee
{
    [TestFixture]
    public class DeleteEmployeeCommandTests
    {
        private Mock<IEmployeeRepository> _mockRepository;
        private Mock<ILogger<DeleteEmployeeCommand.Handler>> _mockLogger;
        private DeleteEmployeeCommand.Handler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _mockLogger = new Mock<ILogger<DeleteEmployeeCommand.Handler>>();
            _handler = new DeleteEmployeeCommand.Handler(_mockRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Handle_ExistingEmployee_ReturnsTrue()
        {
            // Arrange
            int employeeId = 1;
            var employee = new Entities.Employee { Id = employeeId, Name = "Test Doe" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(employeeId))
                           .ReturnsAsync(employee);

            _mockRepository.Setup(repo => repo.DeleteAsync(employeeId))
                           .Returns(Task.CompletedTask);

            var command = new DeleteEmployeeCommand.Command(employeeId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
            _mockRepository.Verify(repo => repo.GetByIdAsync(employeeId), Times.Once);
            _mockRepository.Verify(repo => repo.DeleteAsync(employeeId), Times.Once);
        }

        [Test]
        public async Task Handle_NonExistingEmployee_ReturnsFalse()
        {
            // Arrange
            int employeeId = 2;
            _mockRepository.Setup(repo => repo.GetByIdAsync(employeeId))
                           .ReturnsAsync((Entities.Employee)null); // Simulate employee not found

            var command = new DeleteEmployeeCommand.Command(employeeId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False);
            _mockRepository.Verify(repo => repo.GetByIdAsync(employeeId), Times.Once);
            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void Handle_RepositoryThrowsException_ThrowsExceptionAndLogsError()
        {
            // Arrange
            int employeeId = 3;
            _mockRepository.Setup(repo => repo.GetByIdAsync(employeeId))
                           .ThrowsAsync(new Exception("Database error"));

            var command = new DeleteEmployeeCommand.Command(employeeId);

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Database error"));

            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((obj, type) => obj.ToString().Contains("Error occurred while deleting employee")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
