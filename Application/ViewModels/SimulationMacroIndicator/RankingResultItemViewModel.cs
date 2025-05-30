namespace Application.ViewModels.SimulationMacroIndicator
{
    public class RankingResultItemViewModel
    {
        public string CountryName { get; set; } = string.Empty;
        public string IsoCode { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public decimal ReturnRate { get; set; }
    }
}
