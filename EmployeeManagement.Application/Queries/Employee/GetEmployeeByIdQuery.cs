using Entities = EmployeeManagement.Domain.Entities;
using EmployeeManagement.Persistence.Interfaces;
using MediatR;


namespace EmployeeManagement.Application.Queries.Employee
{
    public class GetEmployeeByIdQuery
    {
        /// <summary>
        /// Represents the request to get an employee by ID.
        /// </summary>
        public class Query : IRequest<Entities.Employee>
        {
            /// <summary>
            /// The ID of the employee to retrieve.
            /// </summary>
            public int Id { get; set; }


            public Query(int id)
            {
                Id = id;
            }
        }
        /// <summary>
        /// Handles the request to retrieve an employee by ID.
        /// </summary>
        public class Handler : IRequestHandler<Query, Entities.Employee>
        {
            private readonly IEmployeeRepository _repository;

            public Handler(IEmployeeRepository repository)
            {
                _repository = repository;
            }
            /// <summary>
            /// Handles the request to fetch an employee by ID.
            /// </summary>
            /// <param name="request">The query request containing the employee ID.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>The employee with the specified ID, or null if not found.</returns>
            public async Task<Entities.Employee> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetByIdAsync(request.Id);
            }
        }
    }
}
