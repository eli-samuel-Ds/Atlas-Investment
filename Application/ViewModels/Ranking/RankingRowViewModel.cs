namespace Application.ViewModels.Ranking
{
    public class RankingRowViewModel
    {
        public string CountryName { get; set; } = "";
        public string IsoCode { get; set; } = "";
        public decimal Score { get; set; }
        public decimal ReturnRate { get; set; }
    }
}
