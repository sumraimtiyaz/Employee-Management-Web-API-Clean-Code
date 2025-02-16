using EmployeeManagement.Application.Queries.Employee;
using Entities = EmployeeManagement.Domain.Entities;
using EmployeeManagement.Persistence.Interfaces;
using Moq;


namespace EmployeeManagement.Tests.Queries.Employee
{
    [TestFixture]
    public class GetEmployeeByIdQueryTests
    {
        private Mock<IEmployeeRepository> _mockRepository;
        private GetEmployeeByIdQuery.Handler _handler;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _handler = new GetEmployeeByIdQuery.Handler(_mockRepository.Object);
        }

        [Test]
        public async Task Handle_ExistingEmployeeId_ReturnsEmployee()
        {
            // Arrange
            int employeeId = 1;
            var expectedEmployee = new Entities.Employee
            {
                Id = employeeId,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Department = "IT",
                Salary = 50000
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(employeeId))
                           .ReturnsAsync(expectedEmployee);

            var query = new GetEmployeeByIdQuery.Query(employeeId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(expectedEmployee.Id));
            Assert.That(result.Name, Is.EqualTo(expectedEmployee.Name));
            Assert.That(result.Email, Is.EqualTo(expectedEmployee.Email));
            Assert.That(result.Department, Is.EqualTo(expectedEmployee.Department));
            Assert.That(result.Salary, Is.EqualTo(expectedEmployee.Salary));


            _mockRepository.Verify(repo => repo.GetByIdAsync(employeeId), Times.Once);
        }

        [Test]
        public async Task Handle_NonExistingEmployeeId_ReturnsNull()
        {
            // Arrange
            int employeeId = 1;
            _mockRepository.Setup(repo => repo.GetByIdAsync(employeeId))
                           .ReturnsAsync((Entities.Employee)null); // Simulate not found

            var query = new GetEmployeeByIdQuery.Query(employeeId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _mockRepository.Verify(repo => repo.GetByIdAsync(employeeId), Times.Once);
        }
    }
}
