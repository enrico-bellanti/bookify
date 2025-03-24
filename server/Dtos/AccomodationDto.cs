using Bookify.Entities;
using Microsoft.EntityFrameworkCore;
using static Bookify.Entities.Accommodation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Dtos
{
    public class AccomodationDto
    {
        public required string Name { get; set; }
        public required AccommodationType Type { get; set; }

        public required int OwnerId { get; set; }

        public required AddressDto Address { get; set; }
    }
}
