using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.CountryIndicador
{
    public class CountryIndicatorCreateViewModel
    {
        [Required(ErrorMessage = "Es obligatorio.")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "El macroindicador es obligatorio.")]
        public int MacroIndicatorId { get; set; }

        [Required(ErrorMessage = "Es obligatorio.")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Es obligatorio.")]
        [Range(1900, 3000, ErrorMessage = "Año inválido.")]
        public int Year { get; set; }

        public IEnumerable<SimpleItemViewModel> Countries { get; set; }
            = new List<SimpleItemViewModel>();
        public IEnumerable<SimpleItemViewModel> MacroIndicators { get; set; }
            = new List<SimpleItemViewModel>();
    }
}
