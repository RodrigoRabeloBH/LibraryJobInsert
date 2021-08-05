using LibraryJobInsert.Domain.Interfaces;
using LibraryJobInsert.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryJobInsert.Infrastructure.Data
{
    public class LibraryRepository<T> : ILibraryRepository<T> where T : Entity
    {
        protected readonly LibraryContext _context;
        protected readonly ILogger<LibraryRepository<T>> _logger;

        public LibraryRepository(ILogger<LibraryRepository<T>> logger, IServiceScopeFactory factory)
        {
            _context = factory.CreateScope().ServiceProvider.GetRequiredService<LibraryContext>(); ;
            _logger = logger;
        }

        public async Task<bool> Create(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message);

                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await GetById(id);

                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);

                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message);

                return false;
            }
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message);

                return false;
            }
        }
    }
}
