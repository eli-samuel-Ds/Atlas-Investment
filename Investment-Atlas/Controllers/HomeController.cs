using Application.Services;
using Application.ViewModels.Ranking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Entities;

namespace Investment_Atlas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContextApp _context;
        private readonly SimulationMacroIndicatorService _simService;
        private readonly ReturnRateConfigService _returnRateService;
        private readonly MacroIndicatorService _macroService;
        private readonly CountryIndicatorService _countryIndicatorService;
        private readonly CountryService _countryService;

        public HomeController(
            ContextApp context,
            SimulationMacroIndicatorService simService,
            ReturnRateConfigService returnRateService,
            MacroIndicatorService macroService,
            CountryIndicatorService countryIndicatorService,
            CountryService countryService)
        {
            _context = context;
            _simService = simService;
            _returnRateService = returnRateService;
            _macroService = macroService;
            _countryIndicatorService = countryIndicatorService;
            _countryService = countryService;
        }

        public async Task<IActionResult> Index()
        {
            var años = await _context.Set<CountryIndicator>()
                                     .Select(ci => ci.Year)
                                     .Distinct()
                                     .OrderByDescending(y => y)
                                     .ToListAsync();

            if (!años.Any())
            {
                var vmVacio = new SelectYearViewModel();
                return View(vmVacio);
            }

            var añoPredeterminado = años.First();

            var vm = new SelectYearViewModel
            {
                AvailableYears = años,
                SelectedYear = añoPredeterminado
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SelectYearViewModel vmSelect)
        {
            int añoSeleccionado = vmSelect.SelectedYear;

            var todosMacro = await _macroService.GetAllAsync(); 
            decimal sumaPesos = todosMacro.Sum(m => m.Weight);
            if (sumaPesos != 1m)
            {
                var resultVm = new RankingResultViewModel
                {
                    ErrorMessage = "Se deben ajustar los pesos de los macroindicadores registrados hasta que la suma de los mismos sea igual a 1.",
                    MacroIndicatorMaintenanceUrl = Url.Action("Index", "MacroIndicator") 
                };
                return View("Result", resultVm);
            }

            var macroConPeso = todosMacro.Where(m => m.Weight > 0m).ToList();
            int cantidadMacroConPeso = macroConPeso.Count;

            var indicadoresAnno = await _context.Set<CountryIndicator>()
                                                .Where(ci => ci.Year == añoSeleccionado)
                                                .Include(ci => ci.Country)
                                                .Include(ci => ci.MacroIndicator)
                                                .ToListAsync();

            var agrupadoPorPais = indicadoresAnno
                .GroupBy(ci => new { ci.CountryId, ci.Country.Name, ci.Country.IsoCode })
                .Select(g => new
                {
                    CountryId = g.Key.CountryId,
                    CountryName = g.Key.Name,
                    IsoCode = g.Key.IsoCode,
                    MacroIdsRegistrados = g.Select(ci => ci.MacroIndicatorId).Distinct().ToList()
                })
                .ToList();

            var elegibles = new List<(int CountryId, string CountryName, string IsoCode)>();

            foreach (var pais in agrupadoPorPais)
            {
                var macroIdsConPeso = macroConPeso.Select(m => m.Id).ToList();
                int conteoEnPais = pais.MacroIdsRegistrados
                                      .Count(id => macroIdsConPeso.Contains(id));

                if (conteoEnPais == cantidadMacroConPeso)
                {
                    elegibles.Add((pais.CountryId, pais.CountryName, pais.IsoCode));
                }
            }

            if (elegibles.Count < 2)
            {
                string mensaje;
                if (elegibles.Count == 0)
                {
                    mensaje = "No hay países que cumplan con los requisitos para el año seleccionado.";
                }
                else 
                {
                    mensaje = $"No hay suficientes países para poder calcular el ranking y la tasa de retorno. El único país que cumple con los requisitos es {elegibles[0].CountryName}, debe agregar más indicadores a los demás países en el año seleccionado.";
                }

                var vmErrorUnPais = new RankingResultViewModel
                {
                    ErrorMessage = mensaje,
                    CountryIndicatorMaintenanceUrl = Url.Action("IndicadorPaises", "CountryIndicator") 
                };
                return View("Result", vmErrorUnPais);
            }

            var indicadoresElegibles = indicadoresAnno
                .Where(ci => elegibles.Any(e => e.CountryId == ci.CountryId)
                             && macroConPeso.Any(m => m.Id == ci.MacroIndicatorId))
                .ToList();

            var maximosPorMacro = new Dictionary<int, decimal>();
            var minimosPorMacro = new Dictionary<int, decimal>();

            foreach (var macro in macroConPeso)
            {
                var valoresDeMacro = indicadoresElegibles
                                      .Where(ci => ci.MacroIndicatorId == macro.Id)
                                      .Select(ci => ci.Value)
                                      .ToList();
                if (valoresDeMacro.Any())
                {
                    maximosPorMacro[macro.Id] = valoresDeMacro.Max();
                    minimosPorMacro[macro.Id] = valoresDeMacro.Min();
                }
                else
                {
                    maximosPorMacro[macro.Id] = 0m;
                    minimosPorMacro[macro.Id] = 0m;
                }
            }

            var rankingTemp = new List<RankingRowViewModel>();

            foreach (var pais in elegibles)
            {
                var regsPais = indicadoresElegibles
                    .Where(ci => ci.CountryId == pais.CountryId)
                    .ToList();

                decimal scoringPais = 0m;

                foreach (var macro in macroConPeso)
                {
                    var ci = regsPais.FirstOrDefault(x => x.MacroIndicatorId == macro.Id);
                    decimal valorP = ci?.Value ?? 0m;

                    decimal minV = minimosPorMacro[macro.Id];
                    decimal maxV = maximosPorMacro[macro.Id];

                    decimal normalizado;

                    if (maxV == minV)
                    {
                        normalizado = 0.5m;
                    }
                    else
                    {
                        if (macro.IsHigherBetter)
                        {
                            normalizado = (valorP - minV) / (maxV - minV);
                        }
                        else
                        {
                            normalizado = (maxV - valorP) / (maxV - minV);
                        }
                    }

                    normalizado = Math.Clamp(normalizado, 0m, 1m);

                    decimal subPuntaje = normalizado * macro.Weight;
                    scoringPais += subPuntaje;
                }

                scoringPais = Math.Clamp(scoringPais, 0m, 1m);

                var listaConfigRetorno = await _returnRateService.GetAllAsync();
                decimal rMin = 2m, rMax = 15m; 

                if (listaConfigRetorno != null && listaConfigRetorno.Any())
                {
                    var cfg = listaConfigRetorno.First();
                    if (cfg.MinRate > 0 && cfg.MaxRate > 0)
                    {
                        rMin = cfg.MinRate;
                        rMax = cfg.MaxRate;
                    }
                }

                decimal tasaRetornoPais = rMin + (rMax - rMin) * scoringPais;

                rankingTemp.Add(new RankingRowViewModel
                {
                    CountryName = pais.CountryName,
                    IsoCode = pais.IsoCode,
                    Score = Math.Round(scoringPais, 4), 
                    ReturnRate = Math.Round(tasaRetornoPais, 2) 
                });
            }

            var listaOrdenada = rankingTemp
                .OrderByDescending(r => r.Score)
                .ToList();

            var vmResult = new RankingResultViewModel
            {
                RankingRows = listaOrdenada
            };

            return View("Result", vmResult);
        }
    }
}
