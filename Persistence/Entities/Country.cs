using Persistence.Common;

namespace Persistence.Entities
{
    public class Country : MacroCountry
    {
        public required string IsoCode { get; set; }
        public ICollection<CountryIndicator> Indicators { get; set; }
    }
}