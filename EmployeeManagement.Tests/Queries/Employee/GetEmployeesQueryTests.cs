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
    public class GetEmployeesQueryTests
    {
        private Mock<IEmployeeRepository> _mockRepository;
        private GetEmployeesQuery.Handler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _handler = new GetEmployeesQuery.Handler(_mockRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsPaginatedEmployees()
        {
            // Arrange
            int pageIndex = 1;
            int rowsPerPage = 2;

            var expectedEmployees = new List<Entities.Employee>
            {
                new Entities.Employee { Id = 1, Name = "John Doe", Email = "john@example.com", Department = "HR", Salary = 50000 },
                new Entities.Employee { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Department = "IT", Salary = 60000 }
            };

            _mockRepository
                .Setup(repo => repo.GetPaginatedAsync(pageIndex, rowsPerPage))
                .ReturnsAsync(expectedEmployees);

            var query = new GetEmployeesQuery.Query(pageIndex, rowsPerPage);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(expectedEmployees.Count));
            Assert.That(result, Is.EquivalentTo(expectedEmployees));

            _mockRepository.Verify(repo => repo.GetPaginatedAsync(pageIndex, rowsPerPage), Times.Once);
        }

        [Test]
        public async Task Handle_WhenNoEmployees_ReturnsEmptyList()
        {
            // Arrange
            int pageIndex = 1;
            int rowsPerPage = 2;

            _mockRepository
                .Setup(repo => repo.GetPaginatedAsync(pageIndex, rowsPerPage))
                .ReturnsAsync(new List<Entities.Employee>());

            var query = new GetEmployeesQuery.Query(pageIndex, rowsPerPage);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Empty);

            _mockRepository.Verify(repo => repo.GetPaginatedAsync(pageIndex, rowsPerPage), Times.Once);
        }
    }
}
