using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Persistence.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Retrieves a paginated list of employees.
        /// </summary>
        /// <param name="pageIndex">The index of the page (starting from 1).</param>
        /// <param name="rowsPerPage">The number of records per page.</param>
        /// <returns>A list of employees.</returns>
        Task<IEnumerable<Employee>> GetPaginatedAsync(int pageIndex, int rowsPerPage);

        /// <summary>
        /// Retrieves an employee by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <returns>The employee if found; otherwise, null.</returns>
        Task<Employee> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new employee to the database.
        /// </summary>
        /// <param name="employee">The employee entity to add.</param>
        Task AddAsync(Employee employee);

        /// <summary>
        /// Updates an existing employee's details.
        /// </summary>
        /// <param name="employee">The updated employee entity.</param>
        Task UpdateAsync(Employee employee);

        /// <summary>
        /// Deletes an employee from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        Task DeleteAsync(int id);
    }
}
