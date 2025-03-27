using Bookify.Entities;
using static Bookify.Repositories.IRepository;

namespace Bookify.Repositories
{
    public interface IUserRepository : IRepository<User, int>
    {
    }
}
