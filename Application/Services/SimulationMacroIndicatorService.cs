using Application.Dtos;
using Application.Dtos.AllDtos;              
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class SimulationMacroIndicatorService
    {
        private readonly SimulationMacroIndicatorRepository _repo;
        private readonly ContextApp _context;

        public SimulationMacroIndicatorService(ContextApp context)
        {
            _repo = new SimulationMacroIndicatorRepository(context);
            _context = context;
        }

        public async Task<bool> AddAsync(SimulationMacroIndicatorDtos dto)
        {
            try
            {
                var entity = new SimulationMacroIndicator
                {
                    Id = 0,             
                    MacroIndicatorId = dto.MacroIndicatorId,
                    Weight = dto.Weight,
                    MacroIndicator = null!          
                };

                var added = await _repo.AddAsync(entity);
                return added != null;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(int id, decimal newWeight)
        {
            try
            {
                var entry = await _context.Set<SimulationMacroIndicator>()
                                          .FindAsync(id);
                if (entry == null)
                    return false;

                entry.Weight = newWeight;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _repo.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<SimulationMacroIndicatorDtos?> GetByIdAsync(int id)
        {
            var e = await _context.Set<SimulationMacroIndicator>()
                                  .Include(s => s.MacroIndicator)
                                  .FirstOrDefaultAsync(s => s.Id == id);

            if (e == null)
                return null;

            return new SimulationMacroIndicatorDtos
            {
                Id = e.Id,
                MacroIndicatorId = e.MacroIndicatorId,
                Weight = e.Weight,
                MacroIndicator = new MacroIndicatorDto
                {
                    Id = e.MacroIndicator.Id,
                    Name = e.MacroIndicator.Name
                }
            };
        }

        public async Task<IEnumerable<SimulationMacroIndicatorDtos>> GetAllAsync()
        {
            return await _context.Set<SimulationMacroIndicator>()
                                 .Include(s => s.MacroIndicator)
                                 .Select(s => new SimulationMacroIndicatorDtos
                                 {
                                     Id = s.Id,
                                     MacroIndicatorId = s.MacroIndicatorId,
                                     Weight = s.Weight,
                                     MacroIndicator = new MacroIndicatorDto
                                     {
                                         Id = s.MacroIndicator.Id,
                                         Name = s.MacroIndicator.Name
                                     }
                                 })
                                 .ToListAsync();
        }

        public async Task<List<RankingResultItemDto>> GetRankingAsync(int year)
        {
            var simList = await GetAllAsync();
            var countryIndicators = await _context.Set<CountryIndicator>()
                .Where(ci => ci.Year == year)
                .Include(ci => ci.Country)   
                .ToListAsync();

            var results = countryIndicators
                .GroupBy(ci => new { ci.CountryId, ci.Country.Name, ci.Country.IsoCode })
                .Select(g =>
                {
                    var score = simList.Sum(s =>
                        g.Where(ci => ci.MacroIndicatorId == s.MacroIndicatorId)
                         .Select(ci => ci.Value)
                         .DefaultIfEmpty(0)
                         .Average()
                         * s.Weight       
                    );
                    var returnRate = score * 0.1m;
                    return new RankingResultItemDto
                    {
                        CountryName = g.Key.Name,
                        IsoCode = g.Key.IsoCode,
                        Score = score,
                        ReturnRate = returnRate
                    };
                })
                .OrderByDescending(r => r.Score)
                .ToList();

            return results;
        }
    }
}