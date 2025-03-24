using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Entities
{
    [PrimaryKey("Id")]
    public class Accommodation
    {
        public enum AccommodationType
        {
            Hotel = 1,
            Apartment = 2,
            Resort = 3,
            Villa = 4,
            Hostel = 5,
        }
        public Accommodation()
        {
            Uuid = Guid.NewGuid();
            Bookings = new HashSet<Booking>();
        }
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public required string Name { get; set; }
        public AccommodationType Type { get; set; }
        // Proprietario dell'accommodation
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual User Owner { get; set; }

        // Indirizzo dell'accommodation
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Address Address { get; set; }

        // Prenotazioni associate a questa accommodation
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}
