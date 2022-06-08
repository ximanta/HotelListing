using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.data
{
    public class Hotel
    {

        /*Entity framework will automatically create an auto incremented Id*/
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int rating { get; set; }
        
        [ForeignKey(nameof(CountryId))]
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
