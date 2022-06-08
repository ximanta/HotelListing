using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Country
{
    public class IncomingCountryDTO
    {
        [Required]
        public String Name { get; set; }
        public String CountryCode { get; set; }

    }
}
