using Microsoft.EntityFrameworkCore;
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
                    Country = null!, 
                    MacroIndicator = null! 
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
                    Country = null!, 
                    MacroIndicator = null! 
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
                var e = await _repo.GetAllQuery()
                                   .Include(ci => ci.Country)
                                   .Include(ci => ci.MacroIndicator)
                                   .FirstOrDefaultAsync(ci => ci.Id == id);

                if (e == null) return null;

                return new CountryIndicatorDtos
                {
                    Id = e.Id,
                    CountryId = e.CountryId,
                    Country = new CountryDtos 
                    { 
                        Id = e.Country.Id, 
                        Name = e.Country.Name, 
                        IsoCode = e.Country.IsoCode 
                    },
                    MacroIndicatorId = e.MacroIndicatorId,
                    MacroIndicator = new MacroIndicatorDtos 
                    { 
                        Id = e.MacroIndicator.Id, 
                        Name = e.MacroIndicator.Name,
                        IsHigherBetter = e.MacroIndicator.IsHigherBetter,
                        Weight = e.MacroIndicator.Weight
                    },
                    Year = e.Year,
                    Value = e.Value
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CountryIndicatorDtos>> GetAllAsync()
        {
            try
            {
                var query = _repo.GetAllQuery()
                                 .Include(ci => ci.Country)
                                 .Include(ci => ci.MacroIndicator);

                var list = await query.ToListAsync();

                return list.Select(e => new CountryIndicatorDtos
                {
                    Id = e.Id,
                    CountryId = e.CountryId,
                    Country = new CountryDtos 
                    { 
                        Id = e.Country.Id, 
                        Name = e.Country.Name, 
                        IsoCode = e.Country.IsoCode 
                    },
                    MacroIndicatorId = e.MacroIndicatorId,
                    MacroIndicator = new MacroIndicatorDtos 
                    { 
                        Id = e.MacroIndicator.Id, 
                        Name = e.MacroIndicator.Name,
                        IsHigherBetter = e.MacroIndicator.IsHigherBetter,
                        Weight = e.MacroIndicator.Weight
                    },
                    Year = e.Year,
                    Value = e.Value
                }).ToList();
            }
            catch
            {
                return new();
            }
        }
    }

}
