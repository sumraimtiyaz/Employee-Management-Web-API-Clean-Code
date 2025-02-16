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
    public class UpdateEmployeeCommandTests
    {
        private Mock<IEmployeeRepository> _mockRepository;
        private Mock<ILogger<UpdateEmployeeCommand.Handler>> _mockLogger;
        private UpdateEmployeeCommand.Handler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _mockLogger = new Mock<ILogger<UpdateEmployeeCommand.Handler>>();
            _handler = new UpdateEmployeeCommand.Handler(_mockRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Handle_ExistingEmployee_ReturnsTrue()
        {
            // Arrange
            int employeeId = 1;
            var existingEmployee = new Entities.Employee
            {
                Id = employeeId,
                Name = "Old Name",
                Email = "old@example.com",
                Department = "IT",
                Salary = 50000
            };

            var command = new UpdateEmployeeCommand.Command
            {
                Id = employeeId,
                Name = "New Name",
                Email = "new@example.com",
                Department = "HR",
                Salary = 60000
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(employeeId))
                           .ReturnsAsync(existingEmployee);

            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Entities.Employee>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(existingEmployee.Name, Is.EqualTo(command.Name));
            Assert.That(existingEmployee.Email, Is.EqualTo(command.Email));
            Assert.That(existingEmployee.Department, Is.EqualTo(command.Department));
            Assert.That(existingEmployee.Salary, Is.EqualTo(command.Salary));

            _mockRepository.Verify(repo => repo.GetByIdAsync(employeeId), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Entities.Employee>()), Times.Once);
        }

        [Test]
        public async Task Handle_NonExistingEmployee_ReturnsFalse()
        {
            // Arrange
            int employeeId = 2;
            _mockRepository.Setup(repo => repo.GetByIdAsync(employeeId))
                           .ReturnsAsync((Entities.Employee)null); // Simulate employee not found

            var command = new UpdateEmployeeCommand.Command
            {
                Id = employeeId,
                Name = "New Name",
                Email = "new@example.com",
                Department = "HR",
                Salary = 60000
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False);
            _mockRepository.Verify(repo => repo.GetByIdAsync(employeeId), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Entities.Employee>()), Times.Never);
        }

        [Test]
        public void Handle_RepositoryThrowsException_ThrowsExceptionAndLogsError()
        {
            // Arrange
            int employeeId = 3;
            _mockRepository.Setup(repo => repo.GetByIdAsync(employeeId))
                           .ThrowsAsync(new Exception("Database error"));

            var command = new UpdateEmployeeCommand.Command
            {
                Id = employeeId,
                Name = "New Name",
                Email = "new@example.com",
                Department = "HR",
                Salary = 60000
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Database error"));

            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((obj, type) => obj.ToString().Contains("Error occurred while updating employee")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
