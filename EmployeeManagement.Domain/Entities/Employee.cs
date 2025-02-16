using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Domain.Entities
{
    /// <summary>
    /// Represents an Employee entity in the system.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email of the employee.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the department of the employee.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the salary of the employee.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the employee record was created.
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
