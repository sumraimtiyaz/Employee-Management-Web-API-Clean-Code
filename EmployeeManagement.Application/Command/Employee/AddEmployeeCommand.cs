using Entities = EmployeeManagement.Domain.Entities;
using EmployeeManagement.Persistence.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Command.Employee
{
    public class AddEmployeeCommand
    {
        /// <summary>
        /// Represents the request to add a new employee.
        /// </summary>
        public class Command : IRequest<int>
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Department { get; set; }
            public decimal Salary { get; set; }
        }
        /// <summary>
        /// Handles the command to add an employee.
        /// </summary>
        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IEmployeeRepository _repository;
            private readonly ILogger<Handler> _logger;

            public Handler(IEmployeeRepository repository, ILogger<Handler> logger)
            {
                _repository = repository;
                _logger = logger;
            }
            /// <summary>
            /// Handles the request to add a new employee.
            /// </summary>
            /// <param name="request">The command containing employee details.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>The newly created employee ID.</returns>
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {

                try
                {
                    var employee = new Entities.Employee
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Department = request.Department,
                        Salary = request.Salary
                    };

                    await _repository.AddAsync(employee);

                    return employee.Id;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while adding employee: {Name}", request.Name);
                    throw;
                }
            }
        }
    }
}
