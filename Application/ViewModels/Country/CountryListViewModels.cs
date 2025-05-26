using Application.ViewModels.Pais;

namespace Application.ViewModels.Country
{
    public class CountryListViewModels
    {
        public IEnumerable<CountryViewModel> Countries { get; set; } = new List<CountryViewModel>();

    }
}
