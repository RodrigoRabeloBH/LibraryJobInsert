using LibraryJobInsert.Domain.Interfaces;
using LibraryJobInsert.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryJobInsert.Infrastructure.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServicesExtensios(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            services.AddDbContext<LibraryContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));

            return services;
        }
    }
}
