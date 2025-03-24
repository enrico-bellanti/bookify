using Bookify.Data;
using Bookify.Dtos;
using Bookify.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Services
{
    public class BookingService : IBookingService
    {
        private readonly BookifyDbContext _db;
        public BookingService(BookifyDbContext db)
        {
            _db = db;
        }

        public async Task<List<Booking>> GetAllBookings(string userUuid = null)
        {
            if (userUuid != null)
            {
                // Parse the string UUID to a valid GUID
                if (!Guid.TryParse(userUuid, out Guid userGuid))
                {
                    throw new ArgumentException("Invalid UUID format");
                }
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Uuid == userGuid);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with UUID {userUuid} not found");
                }
                return await _db.Bookings
                    .Where(a => a.UserId == user.Id)
                    .ToListAsync();
            }
            return await _db.Bookings
                .ToListAsync();
        }

        public async Task<Booking?> GetBookingByID(int id)
        {
            return await _db.Bookings
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Booking?> AddBooking(BookingDto obj)
        {
            // Create new booking from DTO
            var booking = new Booking
            {
                CheckInDate = obj.CheckInDate,
                CheckOutDate = obj.CheckOutDate,
                TotalPrice = obj.TotalPrice,
                UserId = obj.UserId,
                AccommodationId = obj.AccommodationId
                //CreatedAt = DateTime.UtcNow,
                //UpdatedAt = DateTime.UtcNow
            };

            // Add to database and save changes
            await _db.Bookings.AddAsync(booking);
            await _db.SaveChangesAsync();

            return booking;
        }

        public async Task<Booking?> UpdateBooking(int id, BookingDto obj)
        {
            // Find existing accommodation with address
            var booking = await _db.Bookings
                .FirstOrDefaultAsync(a => a.Id == id);

            if (booking == null)
            {
                return null;
            }

            // Validate owner exists if changing owner
            if (obj.UserId != booking.UserId)
            {
                var owner = await _db.Users.FirstOrDefaultAsync(u => u.Id == obj.UserId);
                if (owner == null)
                {
                    throw new KeyNotFoundException($"User with ID {obj.UserId} not found");
                }
            }

            // Update accommodation properties
            booking.CheckInDate = obj.CheckInDate;
            booking.CheckOutDate = obj.CheckOutDate;
            booking.Status = obj.Status;
            booking.TotalPrice = obj.TotalPrice;
            //accommodation.UpdatedAt = DateTime.UtcNow;

            // Save changes
            await _db.SaveChangesAsync();

            return booking;
        }
        public async Task<bool> DeleteBookingByID(int id)
        {
            var booking = await _db.Bookings.FirstOrDefaultAsync(a => a.Id == id);
            if (booking == null)
            {
                return false;
            }

            _db.Bookings.Remove(booking);
            await _db.SaveChangesAsync();

            return true;
        }

    }
}
