using System.Linq.Expressions;
using Bookify.Data;
using Bookify.Data.Pagination;
using Bookify.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories
{
    public class AccommodationRepository : Repository<Accommodation, int>, IAccommodationRepository
    {
        public AccommodationRepository(BookifyDbContext context) : base(context)
        {
        }
    }
}
