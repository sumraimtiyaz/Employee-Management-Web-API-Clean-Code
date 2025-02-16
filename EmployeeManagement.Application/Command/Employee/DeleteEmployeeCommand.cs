using EmployeeManagement.Persistence.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;


namespace EmployeeManagement.Application.Command.Employee
{
    public class DeleteEmployeeCommand
    {
        /// <summary>
        /// Represents the request to delete an employee by ID.
        /// </summary>
        public class Command : IRequest<bool>
        {
            /// <summary>
            /// The ID of the employee to be deleted.
            /// </summary>
            public int Id { get; set; }


            public Command(int id)
            {
                Id = id;
            }
        }

        /// <summary>
        /// Handles the request to delete an employee.
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
            /// Handles the request to delete an employee.
            /// </summary>
            /// <param name="request">The command containing the employee ID.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>True if deletion is successful, false otherwise.</returns>
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var employee = await _repository.GetByIdAsync(request.Id);
                    if (employee == null)
                    {
                        return false; // Employee not found
                    }

                    await _repository.DeleteAsync(employee.Id);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while deleting employee with ID: {Id}", request.Id);
                    throw;
                }
            }
        }
    }
}
