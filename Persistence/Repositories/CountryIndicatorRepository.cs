using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class CountryIndicatorRepository
    {
        private readonly ContextApp _context;
        public CountryIndicatorRepository(ContextApp context)
        {
            _context = context;
        }

        public async Task<CountryIndicator?> AddAsync(CountryIndicator entity)
        {
            await _context.Set<CountryIndicator>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CountryIndicator?> UpdateAsync(int id, CountryIndicator entity)
        {
            var entry = await _context.Set<CountryIndicator>().FindAsync(id);
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
            var entry = await _context.Set<CountryIndicator>().FindAsync(id);
            if (entry != null)
            {
                _context.Set<CountryIndicator>().Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CountryIndicator>> GetAllListAsync()
        {
            return await _context.Set<CountryIndicator>().ToListAsync();
        }

        public async Task<CountryIndicator?> GetByIdAsync(int id)
        {
            return await _context.Set<CountryIndicator>().FindAsync(id);
        }

        public IQueryable<CountryIndicator> GetAllQuery()
        {
            return _context.Set<CountryIndicator>().AsQueryable();
        }
    }
}
