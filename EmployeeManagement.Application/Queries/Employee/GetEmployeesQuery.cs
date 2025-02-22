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
    public class GetEmployeesQuery
    {
        /// <summary>
        /// Represents the request to get employees with pagination.
        /// </summary>
        public class Query : IRequest<IEnumerable<Entities.Employee>> {
            /// <summary>
            /// Gets the index of the page (starting from 1).
            /// </summary>
            public int PageIndex { get; }

            /// <summary>
            /// Gets the number of records to retrieve per page.
            /// </summary>
            public int RowsPerPage { get; }

            public Query(int pageIndex, int rowsPerPage)
            {
                PageIndex = pageIndex;
                RowsPerPage = rowsPerPage;
            }

        }
        /// <summary>
        /// Handles the request to retrieve a paginated list of employees.
        /// </summary>
        public class Handler : IRequestHandler<Query, IEnumerable<Entities.Employee>>
        {
            private readonly IEmployeeRepository _repository;

            public Handler(IEmployeeRepository repository)
            {
                _repository = repository;
            }
            /// <summary>
            /// Handles the query to retrieve employees with pagination.
            /// </summary>
            /// <param name="request">The query request.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>A paginated list of employees.</returns>
            public async Task<IEnumerable<Entities.Employee>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetPaginatedAsync(request.PageIndex, request.RowsPerPage);
            }
        }
    }
}
