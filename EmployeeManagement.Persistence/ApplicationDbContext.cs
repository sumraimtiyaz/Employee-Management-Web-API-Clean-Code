using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeManagement.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the Employees DbSet, representing the Employees table.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }


    }
}
