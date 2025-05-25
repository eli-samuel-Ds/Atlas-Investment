using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class CountryRepository
    {
        private readonly ContextApp _context;

        public CountryRepository(ContextApp context)
        {
            _context = context;
        }

        public async Task<Country?> AddAsync(Country entity)
        {
            await _context.Set<Country>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Country?> UpdateAsync(int id, Country entity)
        {
            var entry = await _context.Set<Country>().FindAsync(id);
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
            var entry = await _context.Set<Country>().FindAsync(id);
            if (entry != null)
            {
                _context.Set<Country>().Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Country>> GetAllListAsync()
        {
            return await _context.Set<Country>().ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            return await _context.Set<Country>().FindAsync(id);
        }

        public IQueryable<Country> GetAllQuery()
        {
            return _context.Set<Country>().AsQueryable();
        }
    }
}