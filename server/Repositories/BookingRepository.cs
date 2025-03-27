using Bookify.Data;
using Bookify.Entities;

namespace Bookify.Repositories
{
    public class BookingRepository : Repository<Booking, int>, IBookingRepository
    {
        public BookingRepository(BookifyDbContext context) : base(context)
        {
        }
    }
}
