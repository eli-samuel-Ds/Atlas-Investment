namespace Application.ViewModels.CountryIndicador
{
    public class CountryIndicatorDeleteViewModel
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string MacroIndicatorName { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Value { get; set; }
    }
}