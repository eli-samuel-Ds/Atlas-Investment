using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.ReturnRateConfig
{
    public class ReturnRateConfgViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "La tasa mínima debe ser mayor o igual a 0.")]
        [Display(Name = "Tasa mínima estimada de retorno")]
        public decimal MinRate { get; set; }

        [Required(ErrorMessage = "Es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "La tasa máxima debe ser mayor o igual a 0.")]
        [Display(Name = "Tasa máxima estimada de retorno")]
        public decimal MaxRate { get; set; }
    }
}
