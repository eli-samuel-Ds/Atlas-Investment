using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class ReturnRateConfigService
    {
        private readonly ReturnRateConfigRepository _repo;
        public ReturnRateConfigService(ContextApp context) => _repo = new ReturnRateConfigRepository(context);

        public async Task<bool> AddAsync(ReturnRateConfigDtos dto)
        {
            try
            {
                var entity = new ReturnRateConfig { 
                    Id = 0, 
                    MinRate = dto.MinRate, 
                    MaxRate = dto.MaxRate 
                };
                
                var added = await _repo.AddAsync(entity);
                return added != null;
            }
            catch { 
                return false; 
            }
        }

        public async Task<bool> UpdateAsync(ReturnRateConfigDtos dto)
        {
            try
            {
                var entity = new ReturnRateConfig { 
                    Id = dto.Id, 
                    MinRate = dto.MinRate, 
                    MaxRate = dto.MaxRate 
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

        public async Task<ReturnRateConfigDtos?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _repo.GetByIdAsync(id);
                if (e == null) return null;
                return new ReturnRateConfigDtos { 
                    Id = e.Id, 
                    MinRate = e.MinRate, 
                    MaxRate = e.MaxRate 
                };
            }
            catch { 
                return null; 
            }
        }

        public async Task<List<ReturnRateConfigDtos>> GetAllAsync()
        {
            try
            {
                var list = await _repo.GetAllListAsync();
                return list.Select(e => new ReturnRateConfigDtos { 
                    Id = e.Id, 
                    MinRate = e.MinRate, 
                    MaxRate = e.MaxRate 
                }).ToList();
            }
            catch { 
                return new(); 
            }
        }
    }
}
