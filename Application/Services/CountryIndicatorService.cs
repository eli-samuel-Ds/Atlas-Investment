using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class CountryIndicatorService
    {
        private readonly CountryIndicatorRepository _repo;
        public CountryIndicatorService(ContextApp context) => _repo = new CountryIndicatorRepository(context);

        public async Task<bool> AddAsync(CountryIndicatorDtos dto)
        {
            try
            {
                var entity = new CountryIndicator { 
                    Id = 0, 
                    CountryId = dto.CountryId, 
                    MacroIndicatorId = dto.MacroIndicatorId, 
                    Year = dto.Year, 
                    Value = dto.Value, 
                    Country = null, 
                    MacroIndicator = null 
                };

                var added = await _repo.AddAsync(entity);
                return added != null;
            }
            catch { 
                return false; 
            }
        }

        public async Task<bool> UpdateAsync(CountryIndicatorDtos dto)
        {
            try
            {
                var entity = new CountryIndicator { 
                    Id = dto.Id, 
                    CountryId = dto.CountryId, 
                    MacroIndicatorId = dto.MacroIndicatorId, 
                    Year = dto.Year, 
                    Value = dto.Value, 
                    Country = null, 
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

        public async Task<CountryIndicatorDtos?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _repo.GetByIdAsync(id);
                if (e == null) return null;
                return new CountryIndicatorDtos { 
                    Id = e.Id, 
                    CountryId = e.CountryId, 
                    MacroIndicatorId = e.MacroIndicatorId, 
                    Year = e.Year, 
                    Value = e.Value, 
                    Country = null, 
                    MacroIndicator = null 
                };
            }
            catch { 
                return null; 
            }
        }

        public async Task<List<CountryIndicatorDtos>> GetAllAsync()
        {
            try
            {
                var list = await _repo.GetAllListAsync();
                return list.Select(e => new CountryIndicatorDtos { 
                    Id = e.Id, 
                    CountryId = e.CountryId, 
                    MacroIndicatorId = e.MacroIndicatorId, 
                    Year = e.Year, 
                    Value = e.Value, 
                    Country = null, 
                    MacroIndicator = null
                }).ToList();
            }
            catch { 
                return new(); 
            }
        }
    }

}
