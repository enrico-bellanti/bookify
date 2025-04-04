using Bookify.Entities;
using static Bookify.Entities.Accommodation;
namespace Bookify.Dtos.Accommodation
{
    public class AccommodationDto
    {
        // Costruttore vuoto per permettere l'istanziazione e il setting manuale
        public AccommodationDto()
        {
        }

        // Costruttore che accetta un oggetto Accommodation
        public AccommodationDto(Entities.Accommodation accommodation)
        {
            Id = accommodation.Id;
            Uuid = accommodation.Uuid;
            Name = accommodation.Name;
            ImgUrl = accommodation.ImgUrl;
            Type = accommodation.Type;
            OwnerId = accommodation.OwnerId;
            Address = accommodation.Address;
            Bookings = accommodation.Bookings;
        }

        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public AccommodationType Type { get; set; }
        public int OwnerId { get; set; }
        public Address Address { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}