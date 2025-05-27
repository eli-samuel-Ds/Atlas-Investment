using Application.Services;
using Application.ViewModels.Country;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

namespace Investment_Atlas.Controllers
{
    public class PaisController : Controller
    {
        private readonly CountryService _service;

        public PaisController(CountryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Paises()
        {
            var dtos = await _service.GetAllAsync();
            var model = new CountryListViewModels
            {
                Countries = dtos.Select(d => new Application.ViewModels.Pais.CountryViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    IsoCode = d.IsoCode
                })
            };
            return View(model);
        }

        public IActionResult Create()
        {
            return View(new CountryCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new CountryDtos { 
                    Id = 0,
                    Name = model.Name, 
                    IsoCode = model.IsoCode 
                };

                if (await _service.AddAsync(dto))
                    return RedirectToAction(nameof(Paises));
                ModelState.AddModelError("", "Error al crear el país.");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            var model = new CountryEditViewModel { 
                Id = dto.Id, 
                Name = dto.Name, 
                IsoCode = dto.IsoCode 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new CountryDtos { 
                    Id = model.Id, 
                    Name = model.Name, 
                    IsoCode = model.IsoCode 
                };

                if (await _service.UpdateAsync(dto))
                    return RedirectToAction(nameof(Paises));
                ModelState.AddModelError("", "Error al editar el país.");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();
            var model = new CountryDeleteViewModel { 
                Id = dto.Id, 
                Name = dto.Name, 
                IsoCode = dto.IsoCode 
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Paises));
        }
    }
}