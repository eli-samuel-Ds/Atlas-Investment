using Persistence.Common;

namespace Persistence.Entities
{
    public class MacroIndicator : MacroCountry
    {
        public required decimal Weight { get; set; }
        public required bool IsHigherBetter { get; set; }
        public ICollection<CountryIndicator> CountryIndicators { get; set; }
        public ICollection<SimulationMacroIndicator> SimulationEntries { get; set; }
    }
}
