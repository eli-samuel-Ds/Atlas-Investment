using Application.Services;
using Application.ViewModels.CountryIndicador;
using Application.ViewModels.SimulationMacroIndicator;
using Microsoft.AspNetCore.Mvc;
using Persistence.Contexts;
using Persistence.Entities;

namespace Investment_Atlas.Controllers
{
    public class SimulationMacroIndicatorController : Controller
    {
        private readonly SimulationMacroIndicatorService _simService;
        private readonly MacroIndicatorService _macroService;
        private readonly ContextApp _context;

        public SimulationMacroIndicatorController(
            SimulationMacroIndicatorService simService,
            MacroIndicatorService macroService,
            ContextApp context)
        {
            _simService = simService;
            _macroService = macroService;
            _context = context;
        }

        public async Task<IActionResult> Simulation()
        {
            var simList = await _simService.GetAllAsync();
            var items = simList.Select(x => new SimulationMacroIndicatorListItemViewModel
            {
                Id = x.Id,
                MacroIndicatorName = x.MacroIndicator.Name, 
                Weight = x.Weight
            });
            var totalWeight = items.Sum(x => x.Weight);

            var years = _context.Set<CountryIndicator>()
                .Select(ci => ci.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .Select(y => new SimpleItemViewModel
                {
                    Id = y,
                    Name = y.ToString()
                })
                .ToList();
            var selectedYear = years.FirstOrDefault()?.Id ?? 0;

            var vm = new SimulationListViewModel
            {
                Items = items,
                TotalWeight = totalWeight,
                YearOptions = years,
                SelectedYear = selectedYear
            };
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var simList = await _simService.GetAllAsync();
            var existing = simList.Sum(x => x.Weight);
            var allMacros = await _macroService.GetAllAsync();
            var available = allMacros
                .Where(m => !simList.Select(s => s.MacroIndicatorId).Contains(m.Id))
                .Select(m => new SimpleItemViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                });

            var vm = new SimulationMacroIndicatorCreateViewModel
            {
                ExistingTotalWeight = existing,
                AvailableMacroIndicators = available
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SimulationMacroIndicatorCreateViewModel vm)
        {
            var simList = await _simService.GetAllAsync();
            vm.ExistingTotalWeight = simList.Sum(x => x.Weight);
            var allMacros = await _macroService.GetAllAsync();
            vm.AvailableMacroIndicators = allMacros
                .Where(m => !simList.Select(s => s.MacroIndicatorId).Contains(m.Id))
                .Select(m => new SimpleItemViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                });

            if (ModelState.IsValid)
            {
                if (vm.ExistingTotalWeight + vm.Weight > 1)
                    ModelState.AddModelError(nameof(vm.Weight), "La suma de pesos no puede exceder 1.");
            }
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new SimulationMacroIndicatorDtos
            {
                Id = 0,
                MacroIndicatorId = vm.MacroIndicatorId,
                Weight = vm.Weight,
                MacroIndicator = null!
            };
            var success = await _simService.AddAsync(dto);

            if (!success)
                ModelState.AddModelError(string.Empty, "Error al agregar macroindicador a simulación.");

            return success
                ? RedirectToAction(nameof(Simulation))
                : View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _simService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var simList = await _simService.GetAllAsync();
            var others = simList.Sum(x => x.Weight) - dto.Weight;
            var vm = new SimulationMacroIndicatorEditViewModel
            {
                Id = dto.Id,
                MacroIndicatorName = dto.MacroIndicator.Name,
                Weight = dto.Weight,
                OtherTotalWeight = others
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SimulationMacroIndicatorEditViewModel vm)
        {
            var simList = await _simService.GetAllAsync();
            var otherSum = simList.Sum(x => x.Weight) - vm.Weight;

            if (ModelState.IsValid)
            {
                if (otherSum + vm.Weight > 1)
                    ModelState.AddModelError(nameof(vm.Weight), "La suma de pesos no puede exceder 1.");
            }
            if (!ModelState.IsValid)
                return View(vm);

            var success = await _simService.UpdateAsync(vm.Id, vm.Weight);
            if (!success)
                ModelState.AddModelError(string.Empty, "Error al editar el peso en la simulación.");

            return success
                ? RedirectToAction(nameof(Simulation))
                : View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _simService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = new SimulationMacroIndicatorDeleteViewModel
            {
                Id = dto.Id,
                MacroIndicatorName = dto.MacroIndicator.Name,
                Weight = dto.Weight
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _simService.DeleteAsync(id);
            return RedirectToAction(nameof(Simulation));
        }

        [HttpPost]
        public async Task<IActionResult> Simulate(int SelectedYear)
        {
            var simList = await _simService.GetAllAsync();
            if (Math.Abs(simList.Sum(x => x.Weight) - 1m) > 0.0001m)
            {
                ModelState.AddModelError(string.Empty,
                    "La suma de los pesos debe ser exactamente 1 antes de simular.");
                var vm = await ConstruirSimulationListViewModelAsync();
                return View("Simulation", vm);
            }
            return RedirectToAction(nameof(Ranking), new { year = SelectedYear });
        }

        public async Task<IActionResult> Ranking(int year)
        {
            var rankingDto = await _simService.GetRankingAsync(year);

            var vm = new RankingViewModel
            {
                Year = year,
                Results = rankingDto.Select((r, idx) => new RankingResultItemViewModel
                {
                    CountryName = r.CountryName,
                    IsoCode = r.IsoCode,
                    Score = r.Score,
                    ReturnRate = r.ReturnRate
                }).ToList()
            };
            return View(vm);
        }

        private async Task<SimulationListViewModel> ConstruirSimulationListViewModelAsync()
        {
            var simList = await _simService.GetAllAsync();
            var items = simList.Select(x => new SimulationMacroIndicatorListItemViewModel
            {
                Id = x.Id,
                MacroIndicatorName = x.MacroIndicator.Name,
                Weight = x.Weight
            });
            var totalWeight = items.Sum(x => x.Weight);
            var years = _context.Set<CountryIndicator>()
                .Select(ci => ci.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .Select(y => new SimpleItemViewModel { Id = y, Name = y.ToString() })
                .ToList();
            var selectedYear = years.FirstOrDefault()?.Id ?? 0;

            return new SimulationListViewModel
            {
                Items = items,
                TotalWeight = totalWeight,
                YearOptions = years,
                SelectedYear = selectedYear
            };
        }
    }
}
