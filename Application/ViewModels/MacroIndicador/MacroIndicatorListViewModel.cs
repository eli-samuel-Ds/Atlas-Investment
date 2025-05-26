namespace Application.ViewModels.MacroIndicador
{
    public class MacroIndicatorListViewModel
    {
        public IEnumerable<MacroIndicadorViewModel> Items { get; set; } = new List<MacroIndicadorViewModel>();
        public decimal TotalWeight { get; set; }
    }
}
