using Application.Dtos.AllDtos;

namespace Persistence.Entities
{
    public class MacroIndicatorDtos : MacroCountryDtos
    {
        public required decimal Weight { get; set; }
        public required bool IsHigherBetter { get; set; }
    }
}
