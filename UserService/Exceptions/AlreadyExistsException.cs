namespace HotelListing.API.Exceptions
{
    public class AlreadyExistsException : ApplicationException
    {
        public AlreadyExistsException(string name, object key) : base($"{name} with id ({key}) already exists")
        {

        }
    }
}
