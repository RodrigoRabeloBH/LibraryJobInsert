using LibraryJobInsert.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryJobInsert.Application.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServicesExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IApplicationServices, ApplicationServices>();

            return services;
        }
    }
}
