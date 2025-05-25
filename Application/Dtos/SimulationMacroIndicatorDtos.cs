using Application.Dtos.AllDtos;

namespace Persistence.Entities
{
    public class SimulationMacroIndicatorDtos : AllDtos<int>
    {
        public required int MacroIndicatorId { get; set; }
        public required MacroIndicatorDtos MacroIndicator { get; set; }
        public required decimal Weight { get; set; }
    }
}
