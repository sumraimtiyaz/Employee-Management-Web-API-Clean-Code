using Entities = EmployeeManagement.Domain.Entities;
using EmployeeManagement.Persistence.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Queries.Employee
{
    public class GetAllEmployeesQuery
    {
        /// <summary>
        /// Represents the request to get all employees.
        /// </summary>
        public class Query : IRequest<IEnumerable<Entities.Employee>> { }
        /// <summary>
        /// Handles the request to retrieve all employees.
        /// </summary>
        public class Handler : IRequestHandler<Query, IEnumerable<Entities.Employee>>
        {
            private readonly IEmployeeRepository _repository;

            public Handler(IEmployeeRepository repository)
            {
                _repository = repository;
            }
            /// <summary>
            /// Handles the request to fetch all employees.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>A list of employees.</returns>
            public async Task<IEnumerable<Entities.Employee>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();
            }
        }
    }
}
