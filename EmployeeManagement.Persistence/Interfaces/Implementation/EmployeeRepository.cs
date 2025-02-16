using EmployeeManagement.Domain.Entities; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Persistence.Interfaces.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the EmployeeRepository class.
        /// </summary>
        /// <param name="context">Database context instance.</param>
        /// <param name="logger">Logger instance for error tracking.</param>
        public EmployeeRepository(ApplicationDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all employees using a stored procedure.
        /// </summary>
        /// <returns>A list of employees.</returns>
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                return await _context.Employees.FromSqlRaw("CALL GetEmployees()").ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all employees.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a specific employee by ID using a stored procedure.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <returns>The employee if found, otherwise an empty Employee object.</returns>
        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                var employees = await _context.Employees
                    .FromSqlRaw("CALL GetEmployee(@p0)", id)
                    .AsNoTracking()
                    .ToListAsync();

                return employees.FirstOrDefault() ?? new Employee();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching employee with ID: {Id}", id);
                throw;
            }
        }

        /// <summary>
        /// Adds a new employee using a stored procedure.
        /// </summary>
        /// <param name="employee">The employee entity to add.</param>
        public async Task AddAsync(Employee employee)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("CALL InsertEmployee({0}, {1}, {2}, {3})",
                    employee.Name, employee.Email, employee.Department, employee.Salary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding employee: {@Employee}", employee);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing employee using a stored procedure.
        /// </summary>
        /// <param name="employee">The updated employee entity.</param>
        public async Task UpdateAsync(Employee employee)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("CALL UpdateEmployee({0}, {1}, {2}, {3}, {4})",
                    employee.Id, employee.Name, employee.Email, employee.Department, employee.Salary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating employee: {@Employee}", employee);
                throw;
            }
        }

        /// <summary>
        /// Deletes an employee by ID using a stored procedure.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        public async Task DeleteAsync(int id)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("CALL DeleteEmployee({0})", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting employee with ID: {Id}", id);
                throw;
            }
        }
    }
}
