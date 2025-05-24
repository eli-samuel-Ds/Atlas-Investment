using Persistence.Common;

namespace Persistence.Entities
{
    public class ReturnRateConfig : AllEntity<int>
    {
        public required decimal MinRate { get; set; }
        public required decimal MaxRate { get; set; }
    }
}
