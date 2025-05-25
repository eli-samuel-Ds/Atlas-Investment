using Application.Dtos.AllDtos;

namespace Persistence.Entities
{
    public class CountryIndicatorDtos : AllDtos<int>
    {
        public required int CountryId { get; set; }
        public required CountryDtos Country { get; set; }
        public required int MacroIndicatorId { get; set; }
        public required MacroIndicatorDtos MacroIndicator { get; set; }
        public required int Year { get; set; }
        public required decimal Value { get; set; }
    }
}
