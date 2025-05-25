using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class SimulationMacroIndicatorService
    {
        private readonly SimulationMacroIndicatorRepository _repo;
        public SimulationMacroIndicatorService(ContextApp context) => _repo = new SimulationMacroIndicatorRepository(context);

        public async Task<bool> AddAsync(SimulationMacroIndicatorDtos dto)
        {
            try
            {
                var entity = new SimulationMacroIndicator { 
                    Id = 0, 
                    MacroIndicatorId = dto.MacroIndicatorId, 
                    Weight = dto.Weight, 
                    MacroIndicator = null 
                };

                var added = await _repo.AddAsync(entity);
                return added != null;
            }
            catch { 
                return false; 
            }
        }

        public async Task<bool> UpdateAsync(SimulationMacroIndicatorDtos dto)
        {
            try
            {
                var entity = new SimulationMacroIndicator { 
                    Id = dto.Id, 
                    MacroIndicatorId = dto.MacroIndicatorId, 
                    Weight = dto.Weight, 
                    MacroIndicator = null
                };

                var updated = await _repo.UpdateAsync(entity.Id, entity);
                return updated != null;
            }
            catch { 
                return false; 
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try { 
                await _repo.DeleteAsync(id); return true; 
            }
            catch { 
                return false; 
            }
        }

        public async Task<SimulationMacroIndicatorDtos?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _repo.GetByIdAsync(id);
                if (e == null) return null;
                return new SimulationMacroIndicatorDtos { 
                    Id = e.Id, 
                    MacroIndicatorId = e.MacroIndicatorId, 
                    Weight = e.Weight, 
                    MacroIndicator = null
                };
            }
            catch { 
                return null; 
            }
        }

        public async Task<List<SimulationMacroIndicatorDtos>> GetAllAsync()
        {
            try
            {
                var list = await _repo.GetAllListAsync();
                return list.Select(e => new SimulationMacroIndicatorDtos { 
                    Id = e.Id, 
                    MacroIndicatorId = e.MacroIndicatorId, 
                    Weight = e.Weight, 
                    MacroIndicator = null
                }).ToList();
            }
            catch { 
                return new(); 
            }
        }
    }
}