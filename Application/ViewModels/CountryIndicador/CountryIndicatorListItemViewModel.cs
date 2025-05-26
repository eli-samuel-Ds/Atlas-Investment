namespace Application.ViewModels.CountryIndicador
{
    public class CountryIndicatorListItemViewModel
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = null!;
        public string MacroIndicatorName { get; set; } = null!;
        public decimal Value { get; set; }
        public int Year { get; set; }
    }
}
