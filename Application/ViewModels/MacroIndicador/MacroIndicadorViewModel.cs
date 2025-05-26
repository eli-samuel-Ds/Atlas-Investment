using Application.ViewModels.Basic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.MacroIndicador
{
    public class MacroIndicadorViewModel : BasicViewModels<int>
    {
        [Required(ErrorMessage = "Es obligatorio")]
        [Range(0, 1, ErrorMessage = "El peso debe estar entre 0 y 1.")]
        public decimal Weight { get; set; }

        [Required]
        public bool IsHigherBetter { get; set; }
    }
}
