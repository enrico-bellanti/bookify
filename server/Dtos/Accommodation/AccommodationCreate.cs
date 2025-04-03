using static Bookify.Entities.Accommodation;

namespace Bookify.Dtos.Accommodation
{
    public class AccommodationCreate
    {
        public required string Name { get; set; }
        public required AccommodationType Type { get; set; }
        public required int OwnerId { get; set; }
        public required AddressDto Address { get; set; }
    }
}
