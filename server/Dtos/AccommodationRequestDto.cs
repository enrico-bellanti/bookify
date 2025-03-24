using static Bookify.Entities.Accommodation;

namespace Bookify.Dtos
{
    public class AccommodationRequestDto
    {
        public required string Name { get; set; }
        public required AccommodationType Type { get; set; }

        public required AddressDto Address { get; set; }
    }
}
