using Bookify.Data;
using Bookify.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories
{
    public class AddressRepository : Repository<Address, int>, IAddressRepository
    {
        public AddressRepository(BookifyDbContext context) : base(context)
        {
        }
    }
}
