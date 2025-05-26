namespace Application.ViewModels.CountryIndicador
{
    public class CountryIndicatorListViewModel
    {
        public IEnumerable<CountryIndicatorListItemViewModel> Items { get; set; } = new List<CountryIndicatorListItemViewModel>();

        public int? FilterCountryId { get; set; }
        public int? FilterYear { get; set; }

        public IEnumerable<SimpleItemViewModel> Countries { get; set; }
            = new List<SimpleItemViewModel>();
    }
}