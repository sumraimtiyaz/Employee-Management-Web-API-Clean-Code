using EmployeeManagement.Application.Command.Employee;
using EmployeeManagement.Application.Queries.Employee;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all employees.
        /// </summary>
        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _mediator.Send(new GetAllEmployeesQuery.Query());
            return Ok(employees);
        }

        /// <summary>
        /// Get an employee by ID.
        /// </summary>
        [HttpGet("GetEmployeeById{employeeId:int}")]
        public async Task<ActionResult<Employee>> GetEmployeeById([FromRoute] int employeeId)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery.Query(employeeId));
            if (employee == null)
            {
                return NotFound(new { Message = "Employee not found." });
            }
            return Ok(employee);
        }

        /// <summary>
        /// Create a new employee.
        /// </summary>
        [HttpPost("CreateEmployee")]
        public async Task<ActionResult<int>> CreateEmployee([FromBody] Employee newEmployee)
        {
            AddEmployeeCommand.Command command = new AddEmployeeCommand.Command 
            {
                Name = newEmployee.Name, 
                Email = newEmployee.Email, 
                Salary = newEmployee.Salary, 
                Department = newEmployee.Department 
            };

            var employeeId = await _mediator.Send(command);

            var generatedUrl = Url.Action(nameof(GetEmployeeById), new { id = employeeId });

            if (string.IsNullOrEmpty(generatedUrl))
            {
                return Ok();
            }
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, new { EmployeeId = employeeId });
        }

        /// <summary>
        /// Update an employee by ID.
        /// </summary>
        [HttpPut("UpdateEmployee{employeeId:int}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int employeeId, [FromBody] UpdateEmployeeCommand.Command employee)
        {
            if (employeeId != employee.Id)
            {
                return BadRequest(new { Message = "Employee ID mismatch." });
            }

            var result = await _mediator.Send(employee);
            if (!result)
            {
                return NotFound(new { Message = "Employee not found." });
            }

            return NoContent();
        }

        /// <summary>
        /// Delete an employee by ID.
        /// </summary>
        [HttpDelete("DeleteEmployee{employeeId:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int employeeId)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand.Command(employeeId));
            if (!result)
            {
                return NotFound(new { Message = "Employee not found." });
            }

            return NoContent();
        }
    }
}
