using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels.Basic
{
    public class BasicViewModels<Tid>
    {
        public Tid? Id { get; set; }

        [Required(ErrorMessage = "Es obligatorio")]
        [StringLength(100, ErrorMessage = "No puede ser mayor a 100")]
        public string Name { get; set; } = null!;
    }
}
