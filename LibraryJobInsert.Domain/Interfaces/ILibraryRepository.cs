using LibraryJobInsert.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryJobInsert.Domain.Interfaces
{
    public interface ILibraryRepository<T> where T : Entity
    {
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}
