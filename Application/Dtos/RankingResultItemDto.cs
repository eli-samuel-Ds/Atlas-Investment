namespace Application.Dtos
{
    public class RankingResultItemDto
    {
        public string CountryName { get; set; } = null!;
        public string IsoCode { get; set; } = null!;
        public decimal Score { get; set; }
        public decimal ReturnRate { get; set; }
    }
}
