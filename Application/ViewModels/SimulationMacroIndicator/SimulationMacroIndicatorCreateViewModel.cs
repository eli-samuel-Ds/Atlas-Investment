using Application.ViewModels.CountryIndicador;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.SimulationMacroIndicator
{
    public class SimulationMacroIndicatorCreateViewModel
    {
        [Required(ErrorMessage = "Debe escoger un macroindicador.")]
        public int MacroIndicatorId { get; set; }

        [Required(ErrorMessage = "Es obligatorio.")]
        public decimal Weight { get; set; }

        public decimal ExistingTotalWeight { get; set; }

        public IEnumerable<SimpleItemViewModel> AvailableMacroIndicators { get; set; } = new List<SimpleItemViewModel>();
    }
}
