using Application.Dtos.AllDtos;

namespace Persistence.Entities
{
    public class CountryDtos : MacroCountryDtos
    {
        public required string IsoCode { get; set; }
    }
}