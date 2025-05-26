using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.SimulationMacroIndicator
{
    public class SimulationMacroIndicatorEditViewModel
    {
        [Required]
        public int Id { get; set; }

        public string MacroIndicatorName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es obligatorio.")]
        public decimal Weight { get; set; }

        public decimal OtherTotalWeight { get; set; }
    }
}
