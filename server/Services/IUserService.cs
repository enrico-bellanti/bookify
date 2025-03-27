using Bookify.Data.Pagination;
using Bookify.Dtos;
using Bookify.Entities;

namespace Bookify.Services
{
    public interface IUserService
    {
        Task<PagedResult<User>> GetAllUsers(
            int page = 0,
            int size = 25,
            string sortBy = "Id",
            bool isDescending = false
        );
        Task<User?> GetUserByID(int id);
        Task<User?> GetUserByUuid(string uuid);
        Task<User?> AddUser(AddUserRequestDto obj);
        Task<User?> UpdateUser(int id, UpdateUserDto obj);
        Task<bool> DeleteUserByID(int id);
        Task<bool> UpdatePassword(int id, string password, string confirmPassword);
    }
}
