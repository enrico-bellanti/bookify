using Bookify.Entities;
using static Bookify.Repositories.IRepository;

namespace Bookify.Repositories
{
    public interface IBookingRepository : IRepository<Booking, int>
    {
    }
}
