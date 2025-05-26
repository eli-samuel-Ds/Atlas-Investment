using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.CountryIndicador
{
    public class CountryIndicatorEditViewModel
    {
        [Required]
        public int Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string MacroIndicatorName { get; set; } = string.Empty;
        public int Year { get; set; }

        [Required(ErrorMessage = "Es obligatorio.")]
        public decimal Value { get; set; }
    }
}
