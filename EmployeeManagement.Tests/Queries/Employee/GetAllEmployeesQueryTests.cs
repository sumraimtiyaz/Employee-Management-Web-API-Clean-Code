using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Application.Queries.Employee;
using EmployeeManagement.Persistence.Interfaces;
using Entities = EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Tests.Queries
{
    [TestFixture]
    public class GetAllEmployeesQueryTests
    {
        private Mock<IEmployeeRepository> _mockRepository;
        private GetAllEmployeesQuery.Handler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _handler = new GetAllEmployeesQuery.Handler(_mockRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsAllEmployees()
        {
            // Arrange
            var expectedEmployees = new List<Entities.Employee>
            {
                new Entities.Employee { Id = 1, Name = "John Doe", Email = "john@example.com", Department = "HR", Salary = 50000 },
                new Entities.Employee { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Department = "IT", Salary = 60000 }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedEmployees);

            var query = new GetAllEmployeesQuery.Query();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(expectedEmployees.Count));
            Assert.That(result, Is.EquivalentTo(expectedEmployees));
        }

        [Test]
        public async Task Handle_WhenNoEmployees_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Entities.Employee>());

            var query = new GetAllEmployeesQuery.Query();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Empty);
        }
    }
}
