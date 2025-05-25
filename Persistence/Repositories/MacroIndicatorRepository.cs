using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class MacroIndicatorRepository
    {
        private readonly ContextApp _context;
        public MacroIndicatorRepository(ContextApp context)
        {
            _context = context;
        }

        public async Task<MacroIndicator?> AddAsync(MacroIndicator entity)
        {
            await _context.Set<MacroIndicator>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MacroIndicator?> UpdateAsync(int id, MacroIndicator entity)
        {
            var entry = await _context.Set<MacroIndicator>().FindAsync(id);
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
            var entry = await _context.Set<MacroIndicator>().FindAsync(id);
            if (entry != null)
            {
                _context.Set<MacroIndicator>().Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<MacroIndicator>> GetAllListAsync()
        {
            return await _context.Set<MacroIndicator>().ToListAsync();
        }

        public async Task<MacroIndicator?> GetByIdAsync(int id)
        {
            return await _context.Set<MacroIndicator>().FindAsync(id);
        }

        public IQueryable<MacroIndicator> GetAllQuery()
        {
            return _context.Set<MacroIndicator>().AsQueryable();
        }
    }
}
