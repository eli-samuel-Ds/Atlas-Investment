using Application.Dtos.AllDtos;

namespace Persistence.Entities
{
    public class SimulationMacroIndicatorDtos : AllDtos<int>
    {
        public int MacroIndicatorId { get; set; }
        public decimal Weight { get; set; }

        public MacroIndicatorDto MacroIndicator { get; set; } = null!;
    }
}
