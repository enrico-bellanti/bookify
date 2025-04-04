using static Bookify.Entities.Accommodation;

namespace Bookify.Dtos.Accommodation
{
    public class AccommodationUpdate
    {
        public string Name { get; set; }
        public AccommodationType Type { get; set; }
        public IFormFile? ImgFile { get; set; }
        public AddressDto? Address { get; set; }
    }
}
