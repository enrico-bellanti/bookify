using Bookify.Data;
using Bookify.Data.Pagination;
using Bookify.Dtos;
using Bookify.Entities;
using Bookify.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        public BookingService(
            IBookingRepository bookingRepository,
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<PagedResult<Booking>> GetAllBookings(
            string userUuid = null,
            int page = 0,
            int size = 25,
            string sortBy = "Id",
            bool isDescending = false
        )
        {
            var sortDirection = isDescending ? SortDirection.DESC : SortDirection.ASC;
            var sort = Sort.By(sortDirection, sortBy);
            var pageRequest = PageRequest.Of(page, size, sort);

            if (userUuid != null)
            {
                // Parse the string UUID to a valid GUID
                if (!Guid.TryParse(userUuid, out Guid userGuid))
                {
                    throw new ArgumentException("Invalid UUID format");
                }
                //var user = await _db.Users.FirstOrDefaultAsync(u => u.Uuid == userGuid);
                var user = await _userRepository.SingleOrDefaultAsync(u => u.Uuid == userGuid);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with UUID {userUuid} not found");
                }
                return await _bookingRepository.GetAllAsync(
                    pageRequest,
                    filter: b => b.UserId == user.Id
                );
            }
            return await _bookingRepository.GetAllAsync(pageRequest);
        }

        public async Task<Booking?> GetBookingByID(int id)
        {
            return await _bookingRepository.SingleOrDefaultAsync(booking => booking.Id == id);
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
            return await _bookingRepository.CreateAsync(booking);
        }

        public async Task<Booking?> UpdateBooking(int id, BookingDto obj)
        {
            var booking = await _bookingRepository.SingleOrDefaultAsync(booking => booking.Id == id);

            if (booking == null)
            {
                return null;
            }

            // Validate owner exists if changing owner
            if (obj.UserId != booking.UserId)
            {
                var owner = await _userRepository.SingleOrDefaultAsync(u => u.Id == obj.UserId);

                if (owner == null)
                {
                    throw new KeyNotFoundException($"User with ID {obj.UserId} not found");
                }
            }

            // Update booking properties
            booking.CheckInDate = obj.CheckInDate;
            booking.CheckOutDate = obj.CheckOutDate;
            booking.Status = obj.Status;
            booking.TotalPrice = obj.TotalPrice;
            //booking.UpdatedAt = DateTime.UtcNow;

            // Save changes
            return await _bookingRepository.UpdateAsync(booking);
        }

        public async Task<bool> DeleteBookingByID(int id)
        {
            var booking = await _bookingRepository.SingleOrDefaultAsync(booking => booking.Id == id);
            if (booking == null)
            {
                return false;
            }

            return await _bookingRepository.DeleteByIdAsync(booking.Id);
        }

    }
}
