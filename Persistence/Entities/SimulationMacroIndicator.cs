using Persistence.Common;
namespace Persistence.Entities
{
    public class SimulationMacroIndicator : AllEntity<int>
    {
        public required int MacroIndicatorId { get; set; }
        public required MacroIndicator MacroIndicator { get; set; }
        public required decimal Weight { get; set; }
    }
}
