using Bookify.Data;
using Bookify.Dtos;
using Bookify.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Bookify.Services
{
    public class UserService: IUserService
    {
        private readonly BookifyDbContext _db;
        private readonly IKeycloakUserService _keycloakUserService;
        public UserService(
            BookifyDbContext db,
            IKeycloakUserService keycloakUserService
        )
        {
            _db = db;
            _keycloakUserService = keycloakUserService;
        }

        public async Task<List<User>> GetAllUsers(bool? isActive)
        {
            if (isActive == null) { return await _db.Users.ToListAsync(); }

            return await _db.Users.Where(u => u.IsActive).ToListAsync();
        }

        public async Task<User?> AddUser(AddUserRequestDto obj)
        {
            var checkPassword = CheckPassword(obj.Password, obj.ConfirmPassword);
            if (!checkPassword)
            {
                throw new ArgumentException("Password and confirmation password do not match.");
            }

            var addKeycloakUser = new AddKeycloakUserDto()
            {
                Username = obj.Username,
                Email = obj.Email,
                Password = obj.Password,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
            };
            var userUuid = await _keycloakUserService.AddUserAsync(addKeycloakUser);

            if (userUuid == null)
            {
                throw new ApplicationException("Failed to create user in Keycloak authentication service.");
            }

            // Create and save the address first
            var address = new Address
            {
                Street = obj.Address.Street,
                Number = obj.Address.Number,
                City = obj.Address.City,
                Province = obj.Address.Province,
                PostalCode = obj.Address.PostalCode,
                Country = obj.Address.Country,
                AdditionalInfo = obj.Address.AdditionalInfo,
                Latitude = obj.Address.Latitude,
                Longitude = obj.Address.Longitude
            };

            _db.Addresses.Add(address);
            await _db.SaveChangesAsync(); // Save to get the AddressId

            var user = new User()
            {
                Username = obj.Username,
                Email = obj.Email,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                PhoneNumber = obj.PhoneNumber,
                Uuid = userUuid.Value,
                AddressId = address.Id, // Link the address to the user
                Address = address // Set the navigation property
            };

            _db.Users.Add(user);
            var result = await _db.SaveChangesAsync();
            return result > 0 ? user : throw new DbUpdateException("Failed to save user to database.");
        }

        public Task<bool> DeleteUserByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByID(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User?> GetUserByUuid(string uuid)
        {
            if (Guid.TryParse(uuid, out Guid guidUuid))
            {
                return await _db.Users.FirstOrDefaultAsync(u => u.Uuid == guidUuid);
            }
            return null;
        }


        public async Task<User?> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var updateKeycloakUser = new UpdateKeycloakUserDto();

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return null;


            if (!string.IsNullOrEmpty(updateUserDto.Email))
            {
                updateKeycloakUser.Email = updateUserDto.Email;
                user.Email = updateUserDto.Email;
            }

            if (!string.IsNullOrEmpty(updateUserDto.FirstName))
            {
                updateKeycloakUser.FirstName = updateUserDto.FirstName;
                user.FirstName = updateUserDto.FirstName;
            }

            if (!string.IsNullOrEmpty(updateUserDto.LastName))
            {
                updateKeycloakUser.LastName = updateUserDto.LastName;
                user.LastName = updateUserDto.LastName;
            }

            if (updateUserDto.IsActive.HasValue)
            {
                // Access the actual boolean value using .Value
                bool isActiveValue = updateUserDto.IsActive.Value;

                // Assign the unwrapped value to your entities
                updateKeycloakUser.IsActive = isActiveValue;
                user.IsActive = isActiveValue;
            }

            var response = await _keycloakUserService.UpdateUserAsync(user.Uuid.ToString(), updateKeycloakUser);

            if (!response)
            {
                throw new ApplicationException("Failed to update user in Keycloak authentication service.");
            }

            _db.Users.Update(user);
            var result = await _db.SaveChangesAsync();
            return result > 0 ? user : throw new DbUpdateException("Failed to update user to database.");
        }


        private bool CheckPassword(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        public async Task<bool> UpdatePassword(int id, string password, string confirmPassword)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return false;

            var checkPassword = CheckPassword(password, confirmPassword);
            if (!checkPassword)
            {
                throw new ArgumentException("Password and confirmation password do not match.");
            }

            return await _keycloakUserService.UpdateUserPasswordAsync(user.Uuid.ToString(), password);
        }

    }
}
