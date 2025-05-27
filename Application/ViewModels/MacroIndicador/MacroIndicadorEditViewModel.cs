using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.MacroIndicador
{
    public class MacroIndicadorEditViewModel
    {
        [Required]
        public int id { get; set; }

        [Required(ErrorMessage = "Es obligatorio.")]
        [StringLength(100, ErrorMessage = "Solo 100")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Es obligatorio.")]
        [Range(0.0001, 1.00, ErrorMessage = "Debe ser entre 0.0001 y 1.")]
        [DisplayFormat(DataFormatString = "{0:F4}", ApplyFormatInEditMode = true)]
        public decimal Weight { get; set; }

        [Required]
        public bool IsHigherBetter { get; set; }
        public decimal ExistingTotalWeight { get; set; }
    }
}
