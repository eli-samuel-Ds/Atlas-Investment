using Application.Dtos.AllDtos;

namespace Persistence.Entities
{
    public class ReturnRateConfigDtos : AllDtos<int>
    {
        public required decimal MinRate { get; set; }
        public required decimal MaxRate { get; set; }
    }
}
