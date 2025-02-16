using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using EmployeeManagement.Persistence;

namespace EmployeeManagement.Application
{
    /// <summary>
    /// Provides extension methods to register application-layer dependencies in the service container.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures and registers application-layer dependencies, including MediatR and persistence dependencies.
        /// </summary>
        public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            // Register Persistence Layer Dependencies
            services.InjectPersistenceDependencies(Configuration);

            // Register MediatR for CQRS pattern
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
