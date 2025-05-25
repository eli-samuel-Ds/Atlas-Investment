using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services
{
    public class CountryService
    {
        private readonly CountryRepository _repo;
        public CountryService(ContextApp context) => _repo = new CountryRepository(context);

        public async Task<bool> AddAsync(CountryDtos dto)
        {
            try
            {
                var entity = new Country { 
                    Id = 0, 
                    Name = dto.Name, 
                    IsoCode = dto.IsoCode 
                };

                var added = await _repo.AddAsync(entity);
                return added != null;
            }
            catch { 
                return false; 
            }
        }

        public async Task<bool> UpdateAsync(CountryDtos dto)
        {
            try
            {
                var entity = new Country { 
                    Id = dto.Id, 
                    Name = dto.Name, 
                    IsoCode = dto.IsoCode 
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

        public async Task<CountryDtos?> GetByIdAsync(int id)
        {
            try
            {
                var e = await _repo.GetByIdAsync(id);
                if (e == null) return null;
                return new CountryDtos { 
                    Id = e.Id, 
                    Name = e.Name, 
                    IsoCode = e.IsoCode 
                };
            }
            catch { 
                return null; 
            }
        }

        public async Task<List<CountryDtos>> GetAllAsync()
        {
            try
            {
                var list = await _repo.GetAllListAsync();
                return list.Select(e => new CountryDtos { 
                    Id = e.Id, 
                    Name = e.Name, 
                    IsoCode = e.IsoCode 
                }).ToList();
            }
            catch { 
                return new(); 
            }
        }

        public async Task<List<CountryDtos>> GetAllWithIncludeAsync()
        {
            try
            {
                var query = _repo.GetAllQuery();
                var list = await query.Include(c => c.Indicators).ToListAsync();
                return list.Select(e => new CountryDtos { 
                    Id = e.Id, 
                    Name = e.Name, 
                    IsoCode = e.IsoCode 
                }).ToList();
            }
            catch { 
                return new(); 
            }
        }
    }
}
