using Application.ViewModels.Basic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Pais
{
    public class CountryViewModel : BasicViewModels<int>
    {
        [Required(ErrorMessage = "Es obligatorio")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "Iso de 2 letras")]
        public string IsoCode { get; set; } = null!;
    }
}

