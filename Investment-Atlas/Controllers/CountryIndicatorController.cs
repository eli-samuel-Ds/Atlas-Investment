using Application.Services;
using Application.ViewModels.CountryIndicador;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace Investment_Atlas.Controllers
{
    public class CountryIndicatorController : Controller
    {
        private readonly CountryIndicatorService _ciService;
        private readonly CountryService _countryService;
        private readonly MacroIndicatorService _macroService;

        public CountryIndicatorController( CountryIndicatorService ciService, 
            CountryService countryService, MacroIndicatorService macroService )
        {
            _ciService = ciService;
            _countryService = countryService;
            _macroService = macroService;
        }

        public async Task<IActionResult> IndicadorPaises(int? filterCountryId, int? filterYear)
        {
            var all = await _ciService.GetAllAsync();
            var items = all
                .Where(d => !filterCountryId.HasValue || d.CountryId == filterCountryId.Value)
                .Where(d => !filterYear.HasValue || d.Year == filterYear.Value)
                .Select(d => new CountryIndicatorListItemViewModel
                {
                    Id = d.Id,
                    CountryName = d.Country.Name,
                    MacroIndicatorName = d.MacroIndicator.Name,
                    Value = d.Value,
                    Year = d.Year
                });

            var countries = (await _countryService.GetAllAsync())
                .Select(c => new SimpleItemViewModel 
                { 
                    Id = c.Id, 
                    Name = c.Name 
                });

            var vm = new CountryIndicatorListViewModel
            {
                Items = items,
                FilterCountryId = filterCountryId,
                FilterYear = filterYear,
                Countries = countries
            };
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new CountryIndicatorCreateViewModel
            {
                Countries = (await _countryService.GetAllAsync())
                    .Select(c => new SimpleItemViewModel 
                    { 
                        Id = c.Id, 
                        Name = c.Name 
                    }),
                MacroIndicators = (await _macroService.GetAllAsync())
                    .Select(m => new SimpleItemViewModel 
                    { 
                        Id = m.Id, 
                        Name = m.Name 
                    })
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryIndicatorCreateViewModel vm)
        {
            vm.Countries = (await _countryService.GetAllAsync())
                .Select(c => new SimpleItemViewModel 
                { 
                    Id = c.Id, 
                    Name = c.Name 
                });
            vm.MacroIndicators = (await _macroService.GetAllAsync())
                .Select(m => new SimpleItemViewModel 
                { 
                    Id = m.Id, 
                    Name = m.Name 
                });

            if (ModelState.IsValid)
            {
                var existing = (await _ciService.GetAllAsync())
                    .Any(d => d.CountryId == vm.CountryId
                           && d.MacroIndicatorId == vm.MacroIndicatorId
                           && d.Year == vm.Year);
                if (existing)
                {
                    ModelState.AddModelError("", "Ya existe un indicador para ese macroindicador y año.");
                    return View(vm);
                }

                var dto = new CountryIndicatorDtos
                {
                    CountryId = vm.CountryId,
                    MacroIndicatorId = vm.MacroIndicatorId,
                    Year = vm.Year,
                    Value = vm.Value,
                    Country = null!,
                    Id = 0, 
                    MacroIndicator = null!
                };
                if (await _ciService.AddAsync(dto))
                    return RedirectToAction(nameof(IndicadorPaises));

                ModelState.AddModelError("", "Error al crear el indicador.");
            }
            return View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _ciService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            var vm = new CountryIndicatorEditViewModel
            {
                Id = dto.Id,
                CountryName = dto.Country.Name,
                MacroIndicatorName = dto.MacroIndicator.Name,
                Year = dto.Year,
                Value = dto.Value
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryIndicatorEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var existing = await _ciService.GetByIdAsync(vm.Id);
                if (existing == null) return NotFound();

                var dto = new CountryIndicatorDtos
                {
                    Id = vm.Id,
                    CountryId = existing.CountryId,
                    MacroIndicatorId = existing.MacroIndicatorId,
                    Year = existing.Year,
                    Value = vm.Value,
                    Country = null!,
                    MacroIndicator = null!
                };

                if (await _ciService.UpdateAsync(dto))
                    return RedirectToAction(nameof(IndicadorPaises));

                ModelState.AddModelError("", "Error al editar el indicador.");
            }

            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _ciService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            var vm = new CountryIndicatorDeleteViewModel
            {
                Id = dto.Id,
                CountryName = dto.Country.Name,
                MacroIndicatorName = dto.MacroIndicator.Name,
                Year = dto.Year,
                Value = dto.Value
            };
            return View(vm);
        }
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _ciService.DeleteAsync(id);
            if (!success)
                ModelState.AddModelError(string.Empty, "Error al eliminar macroindicador.");

            return RedirectToAction(nameof(IndicadorPaises));
        }
    }
}