using Bookify.Data;
using Bookify.Entities;

namespace Bookify.Repositories
{
    public class UserRepository: Repository<User, int>, IUserRepository
    {
        public UserRepository(BookifyDbContext context) : base(context)
        { }
    }
}
