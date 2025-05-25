using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Persistence.Repositories
{
    public class SimulationMacroIndicatorRepository
    {
        private readonly ContextApp _context;
        public SimulationMacroIndicatorRepository(ContextApp context)
        {
            _context = context;
        }

        public async Task<SimulationMacroIndicator?> AddAsync(SimulationMacroIndicator entity)
        {
            await _context.Set<SimulationMacroIndicator>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SimulationMacroIndicator?> UpdateAsync(int id, SimulationMacroIndicator entity)
        {
            var entry = await _context.Set<SimulationMacroIndicator>().FindAsync(id);
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
            var entry = await _context.Set<SimulationMacroIndicator>().FindAsync(id);
            if (entry != null)
            {
                _context.Set<SimulationMacroIndicator>().Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<SimulationMacroIndicator>> GetAllListAsync()
        {
            return await _context.Set<SimulationMacroIndicator>().ToListAsync();
        }

        public async Task<SimulationMacroIndicator?> GetByIdAsync(int id)
        {
            return await _context.Set<SimulationMacroIndicator>().FindAsync(id);
        }

        public IQueryable<SimulationMacroIndicator> GetAllQuery()
        {
            return _context.Set<SimulationMacroIndicator>().AsQueryable();
        }
    }
}
}
