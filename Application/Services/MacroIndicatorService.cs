using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class MacroIndicatorService
    {
        private readonly MacroIndicatorRepository _repo;
        public MacroIndicatorService(ContextApp context) => _repo = new MacroIndicatorRepository(context);

        public async Task<bool> AddAsync(MacroIndicatorDtos dto)
        {
            try
            {
                var entity = new MacroIndicator { 
                    Id = 0, 
                    Name = dto.Name, 
                    Weight = dto.Weight, 
                    IsHigherBetter = dto.IsHigherBetter 
                };
                
                var added = await _repo.AddAsync(entity);
                return added != null;
            }
            catch { 
                return false; 
            }
        }

        public async Task<bool> UpdateAsync(MacroIndicatorDtos dto)
        {
            try
            {
                var entity = new MacroIndicator { 
                    Id = dto.Id, 
                    Name = dto.Name, 
                    Weight = dto.Weight, 
                    IsHigherBetter = dto.IsHigherBetter 
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

        public async Task<MacroIndicatorDtos?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _repo.GetByIdAsync(id);
                if (e == null) return null;
                return new MacroIndicatorDtos { 
                    Id = e.Id, 
                    Name = e.Name, 
                    Weight = e.Weight, 
                    IsHigherBetter = e.IsHigherBetter 
                };
            }
            catch { 
                return null; 
            }
        }

        public async Task<List<MacroIndicatorDtos>> GetAllAsync()
        {
            try
            {
                var list = await _repo.GetAllListAsync();
                return list.Select(e => new MacroIndicatorDtos { 
                    Id = e.Id, 
                    Name = e.Name, 
                    Weight = e.Weight, 
                    IsHigherBetter = e.IsHigherBetter 
                }).ToList();
            }
            catch { 
                return new(); 
            }
        }
    }
}