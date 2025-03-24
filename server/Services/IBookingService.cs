using Bookify.Dtos;
using Bookify.Entities;

namespace Bookify.Services
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllBookings(string userUuid = null);
        Task<Booking?> GetBookingByID(int id);
        Task<Booking?> AddBooking(BookingDto obj);
        Task<Booking?> UpdateBooking(int id, BookingDto obj);
        Task<bool> DeleteBookingByID(int id);
    }
}
