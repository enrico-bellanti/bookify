using Bookify.Dtos;

namespace Bookify.Services
{
    public interface IKeycloakUserService
    {
        Task<Guid?> AddUserAsync(AddKeycloakUserDto keycloakUserDto);
        Task<bool> UpdateUserAsync(string userUuid, UpdateKeycloakUserDto userDto);
        Task<bool> UpdateUserPasswordAsync(string userUuid, string password);
        bool CompareUserRoles(string[] requiredRoles, string[] userRoles);
    }
}
