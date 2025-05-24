using Persistence.Common;

namespace Persistence.Entities
{
    public class CountryIndicator : AllEntity<int>
    {
        public required int CountryId { get; set; }
        public required Country Country { get; set; }
        public required int MacroIndicatorId { get; set; }
        public required MacroIndicator MacroIndicator { get; set; }
        public required int Year { get; set; }
        public required decimal Value { get; set; }
    }
}
