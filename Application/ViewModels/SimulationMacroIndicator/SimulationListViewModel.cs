using Application.ViewModels.CountryIndicador;

namespace Application.ViewModels.SimulationMacroIndicator
{
    public class SimulationListViewModel
    {
        public IEnumerable<SimulationMacroIndicatorListItemViewModel> Items { get; set; } = new List<SimulationMacroIndicatorListItemViewModel>();

        public decimal TotalWeight { get; set; }

        public IEnumerable<SimpleItemViewModel> YearOptions { get; set; } = new List<SimpleItemViewModel>();

        public int SelectedYear { get; set; }
    }
}
