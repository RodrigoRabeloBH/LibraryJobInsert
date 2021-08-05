using LibraryJobInsert.Domain.Interfaces;
using LibraryJobInsert.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LibraryJobInsert.Infrastructure.Data
{
    public class CustomerRepository : LibraryRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ILogger<LibraryRepository<Customer>> logger, IServiceScopeFactory factory) : base(logger, factory)
        {
        }
    }
}
