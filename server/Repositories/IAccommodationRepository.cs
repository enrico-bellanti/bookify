using Bookify.Data.Pagination;
using System.Linq.Expressions;
using Bookify.Entities;
using static Bookify.Repositories.IRepository;

namespace Bookify.Repositories
{
    public interface IAccommodationRepository : IRepository<Accommodation, int>
    {
    }
}
