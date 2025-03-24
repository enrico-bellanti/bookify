using Bookify.Entities;
using Microsoft.EntityFrameworkCore;
using static Bookify.Entities.Booking;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Dtos
{
    public class BookingDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [ForeignKey("User")]
        // Relazioni
        public int UserId { get; set; }  // Cliente che ha prenotato
        public int AccommodationId { get; set; }
    }
}
