namespace Application.ViewModels.Ranking
{
    public class RankingResultViewModel
    {
        public List<RankingRowViewModel> RankingRows { get; set; } = new List<RankingRowViewModel>();

        public string? ErrorMessage { get; set; }

        public string? MacroIndicatorMaintenanceUrl { get; set; }

        public string? CountryIndicatorMaintenanceUrl { get; set; }
    }
}
