using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Entities
{
    [PrimaryKey("Id")]
    public class Booking
    {
        public enum BookingStatus
        {
            Pending = 1,
            Confirmed = 2,
            Completed = 3,
            Cancelled = 4
        }
        public Booking()
        {
            Uuid = Guid.NewGuid();
        }
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [ForeignKey("User")]
        // Relazioni
        public int UserId { get; set; }  // Cliente che ha prenotato
        public User User { get; set; }
        [ForeignKey("Accommodation")]
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
    }
}
