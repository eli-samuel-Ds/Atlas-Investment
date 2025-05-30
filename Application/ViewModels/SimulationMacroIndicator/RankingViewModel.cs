namespace Application.ViewModels.SimulationMacroIndicator
{
    public class RankingViewModel
    {
        public int Year { get; set; }
        public List<RankingResultItemViewModel> Results { get; set; } = new List<RankingResultItemViewModel>();
    }
}
