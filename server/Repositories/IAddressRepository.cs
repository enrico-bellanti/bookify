using Bookify.Entities;
using Microsoft.EntityFrameworkCore;
using static Bookify.Repositories.IRepository;

namespace Bookify.Repositories
{
    public interface IAddressRepository : IRepository<Address, int>
    {
        // Address-specific methods
    }
}
