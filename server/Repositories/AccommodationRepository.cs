using Bookify.Data;
using Bookify.Entities;

namespace Bookify.Repositories
{
    public class AccommodationRepository : Repository<Accommodation, int>, IAccommodationRepository
    {
        public AccommodationRepository(BookifyDbContext context) : base(context)
        {
        }
    }
}
