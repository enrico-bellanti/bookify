using Microsoft.EntityFrameworkCore;
using static Bookify.Entities.Booking;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Dtos
{
    public class BookingRequestDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        [Precision(18, 2)]
        public decimal TotalPrice { get; set; }
        public int AccommodationId { get; set; }
    }
}
