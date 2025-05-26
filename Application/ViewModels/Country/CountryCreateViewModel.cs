using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Country
{
    public class CountryCreateViewModel
    {
        [Required(ErrorMessage = "Es obligatorio.")]
        [StringLength(100, ErrorMessage = "No puede ser mas de 100")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Es obligatorio.")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "Dos 2 letras mayusculas")]
        public string IsoCode { get; set; } = null!;
    }
}
