using LibraryJobInsert.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryJobInsert.Domain.Interfaces
{
    public interface IApplicationServices
    {
        Task<IEnumerable<Message>> GET(string queueName);
        Task<IEnumerable<Message>> InsertCustomer(IEnumerable<Message> messages);
        Task POST(IEnumerable<Message> messages);
    }
}
