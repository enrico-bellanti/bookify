using Bookify.Data.Pagination;
using Bookify.Dtos;
using Bookify.Entities;

namespace Bookify.Services
{
    public interface IBookingService
    {
        Task<PagedResult<Booking>> GetAllBookings(
            string userUuid = null,
            int page = 0,
            int size = 25,
            string sortBy = "Id",
            bool isDescending = false
        );
        Task<Booking?> GetBookingByID(int id);
        Task<Booking?> AddBooking(BookingDto obj);
        Task<Booking?> UpdateBooking(int id, BookingDto obj);
        Task<bool> DeleteBookingByID(int id);
    }
}
