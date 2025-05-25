using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class ReturnRateConfigRepository
    {
        private readonly ContextApp _context;
        public ReturnRateConfigRepository(ContextApp context)
        {
            _context = context;
        }

        public async Task<ReturnRateConfig?> AddAsync(ReturnRateConfig entity)
        {
            await _context.Set<ReturnRateConfig>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ReturnRateConfig?> UpdateAsync(int id, ReturnRateConfig entity)
        {
            var entry = await _context.Set<ReturnRateConfig>().FindAsync(id);
            if (entry != null)
            {
                _context.Entry(entry).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entry;
            }
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await _context.Set<ReturnRateConfig>().FindAsync(id);
            if (entry != null)
            {
                _context.Set<ReturnRateConfig>().Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ReturnRateConfig>> GetAllListAsync()
        {
            return await _context.Set<ReturnRateConfig>().ToListAsync();
        }

        public async Task<ReturnRateConfig?> GetByIdAsync(int id)
        {
            return await _context.Set<ReturnRateConfig>().FindAsync(id);
        }

        public IQueryable<ReturnRateConfig> GetAllQuery()
        {
            return _context.Set<ReturnRateConfig>().AsQueryable();
        }
    }
}
