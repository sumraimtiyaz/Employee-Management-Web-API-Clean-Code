using EmployeeManagement.Persistence.Interfaces;
using EmployeeManagement.Persistence.Interfaces.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeeManagement.Persistence
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures and registers persistence-layer dependencies, including the database context and repositories.
        /// </summary>
        public static IServiceCollection InjectPersistenceDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            // Ensure connection string is available
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }

            // Register Database Context with MySQL
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Register Employee Repository
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
