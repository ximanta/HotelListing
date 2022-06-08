namespace HotelListing.API.data
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string countryCode { get; set; }

        public virtual IList<Hotel> Hotels { get; set; }


    }
}