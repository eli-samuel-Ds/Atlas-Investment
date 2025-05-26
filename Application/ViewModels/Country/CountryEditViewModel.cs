using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Country
{
    public class CountryEditViewModel
    {
        [Required(ErrorMessage = "Es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Es obligatorio")]
        [StringLength(100, ErrorMessage = "No puede ser mayor que 100")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Es obligatorio")]
        [RegularExpression("^[A-Z]{2}$", ErrorMessage = "Iso de 2 letras")]
        public string IsoCode { get; set; } = null!;
    }
}
