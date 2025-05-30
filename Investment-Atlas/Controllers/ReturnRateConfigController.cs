using Application.Services;
using Application.ViewModels.ReturnRateConfig;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities;

public class ReturnRateConfigController : Controller
{
    private readonly ReturnRateConfigService _service;

    public ReturnRateConfigController(ReturnRateConfigService service)
    {
        _service = service;
    }

    public async Task<IActionResult> ReturnRate()
    {
        var configs = await _service.GetAllAsync();
        var config = configs.OrderByDescending(c => c.Id).FirstOrDefault();

        var viewModel = config != null
            ? new ReturnRateConfgViewModel
            {
                Id = config.Id,
                MinRate = config.MinRate,
                MaxRate = config.MaxRate
            }
            : new ReturnRateConfgViewModel();

        return View("ReturnRate", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Save(ReturnRateConfgViewModel viewModel)
    {
        if (viewModel.MinRate >= viewModel.MaxRate)
        {
            ModelState.AddModelError("", "La tasa mínima debe ser menor que la tasa máxima.");
        }

        if (!ModelState.IsValid)
        {
            return View("ReturnRate", viewModel);
        }

        if (viewModel.Id == 0)
        {
            var existing = (await _service.GetAllAsync()).OrderByDescending(c => c.Id).FirstOrDefault();
            if (existing != null)
                viewModel.Id = existing.Id;
        }

        var dto = new ReturnRateConfigDtos
        {
            Id = viewModel.Id,
            MinRate = viewModel.MinRate,
            MaxRate = viewModel.MaxRate
        };

        if (dto.Id == 0)
            await _service.AddAsync(dto);
        else
            await _service.UpdateAsync(dto);

        return RedirectToAction(nameof(ReturnRate));
    }

    public IActionResult Atras()
    {
        return RedirectToAction("Index", "Home");
    }
}
