using EmployeeManagement.Persistence.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;


namespace EmployeeManagement.Application.Command.Employee
{
    public class UpdateEmployeeCommand
    {
        /// <summary>
        /// Represents the request to update an employee's details.
        /// </summary>
        public class Command : IRequest<bool>
        {
            /// <summary>
            /// The ID of the employee to be updated.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// The updated name of the employee.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The updated email of the employee.
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// The updated department of the employee.
            /// </summary>
            public string Department { get; set; }

            /// <summary>
            /// The updated salary of the employee.
            /// </summary>
            public decimal Salary { get; set; }
        }
        /// <summary>
        /// Handles the request to update an employee.
        /// </summary>
        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IEmployeeRepository _repository;
            private readonly ILogger<Handler> _logger;

            public Handler(IEmployeeRepository repository, ILogger<Handler> logger)
            {
                _repository = repository;
                _logger = logger;
            }
            /// <summary>
            /// Handles the request to update an employee.
            /// </summary>
            /// <param name="request">The command containing updated employee details.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>True if update is successful, false otherwise.</returns>
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {

                try
                {
                    var employee = await _repository.GetByIdAsync(request.Id);
                    if (employee == null)
                    {
                        return false;
                    }

                    employee.Name = request.Name;
                    employee.Email = request.Email;
                    employee.Department = request.Department;
                    employee.Salary = request.Salary;

                    await _repository.UpdateAsync(employee);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating employee with ID: {Id}", request.Id);
                    throw;
                }
            }
        }
    }
}
