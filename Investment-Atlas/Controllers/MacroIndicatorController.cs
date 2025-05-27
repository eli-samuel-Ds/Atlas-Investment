using Application.Services;
using Application.ViewModels.MacroIndicador;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace Investment_Atlas.Controllers
{
    public class MacroIndicatorController : Controller
    {
        private readonly MacroIndicatorService _service;

        public MacroIndicatorController(MacroIndicatorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> MacroIndicadores()
        {
            var items = await _service.GetAllAsync();
            var totalWeight = items.Sum(x => x.Weight);
            var vm = new MacroIndicatorListViewModel
            {
                Items = items.Select(x => new MacroIndicadorViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Weight = x.Weight,
                    IsHigherBetter = x.IsHigherBetter
                }),
                TotalWeight = totalWeight
            };
            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            var existing = (await _service.GetAllAsync()).Sum(x => x.Weight);
            var vm = new MacroIndicadorCreateViewModel
            {
                ExistingTotalWeight = existing
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MacroIndicadorCreateViewModel vm)
        {
            vm.ExistingTotalWeight = (await _service.GetAllAsync()).Sum(x => x.Weight);
            if (ModelState.IsValid)
            {
                if (vm.ExistingTotalWeight + vm.Weight > 1)
                {
                    ModelState.AddModelError(nameof(vm.Weight), "La suma de pesos no puede exceder 1.");
                }
            }

            if (!ModelState.IsValid)
                return View(vm);

            var success = await _service.AddAsync(new MacroIndicatorDtos
            {
                Id = 0,
                Name = vm.Name,
                Weight = vm.Weight,
                IsHigherBetter = vm.IsHigherBetter
            });

            if (!success)
                ModelState.AddModelError(string.Empty, "Error al crear macroindicador.");

            return success ? RedirectToAction(nameof(MacroIndicadores)) : View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var all = await _service.GetAllAsync();
            var existing = all.Sum(x => x.Weight) - dto.Weight;

            var vm = new MacroIndicadorEditViewModel
            {
                id = dto.Id,
                Name = dto.Name,
                Weight = dto.Weight,
                IsHigherBetter = dto.IsHigherBetter,
                ExistingTotalWeight = existing
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MacroIndicadorEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.ExistingTotalWeight + vm.Weight > 1)
                {
                    ModelState.AddModelError(nameof(vm.Weight), "La suma de pesos no puede exceder 1.");
                }
            }

            if (!ModelState.IsValid)
                return View(vm);

            var success = await _service.UpdateAsync(new MacroIndicatorDtos
            {
                Id = vm.id,
                Name = vm.Name,
                Weight = vm.Weight,
                IsHigherBetter = vm.IsHigherBetter
            });

            if (!success)
                ModelState.AddModelError(string.Empty, "Error al editar macroindicador.");

            return success ? RedirectToAction(nameof(MacroIndicadores)) : View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = new MacroIndicadorDeleteViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Weight = dto.Weight,
                IsHigherBetter = dto.IsHigherBetter
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                ModelState.AddModelError(string.Empty, "Error al eliminar macroindicador.");

            return RedirectToAction(nameof(MacroIndicadores));
        }
    }
}