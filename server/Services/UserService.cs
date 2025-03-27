using Bookify.Data;
using Bookify.Data.Pagination;
using Bookify.Dtos;
using Bookify.Entities;
using Bookify.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Bookify.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IKeycloakUserService _keycloakUserService;

        public UserService(
            IUserRepository userRepository,
            IKeycloakUserService keycloakUserService,
            IAddressRepository addressRepository
        )
        {
            _userRepository = userRepository;
            _keycloakUserService = keycloakUserService;
            _addressRepository = addressRepository;
        }

        public async Task<PagedResult<User>> GetAllUsers(
            int page = 0,
            int size = 25,
            string sortBy = "Id",
            bool isDescending = false
        )
        {
            var sortDirection = isDescending ? SortDirection.DESC : SortDirection.ASC;
            var sort = Sort.By(sortDirection, sortBy);
            var pageRequest = PageRequest.Of(page, size, sort);

            return await _userRepository.GetAllAsync(pageRequest);
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
            await _addressRepository.CreateAsync(address);

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

            var newUser = await _userRepository.CreateAsync(user);
            if (newUser == null)
            {
                throw new DbUpdateException("Failed to save user to database.");
            }
            return newUser;
        }

        public Task<bool> DeleteUserByID(int id)
        {
            //rememeber to do anlt if admin and delete keyclaok record
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByID(int id)
        {
            return await _userRepository.SingleOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User?> GetUserByUuid(string uuid)
        {
            if (Guid.TryParse(uuid, out Guid guidUuid))
            {
                return await _userRepository.SingleOrDefaultAsync(u => u.Uuid == guidUuid); ;
            }
            return null;
        }


        public async Task<User?> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var updateKeycloakUser = new UpdateKeycloakUserDto();

            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == id);
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

            var updatedUser = await _userRepository.UpdateAsync(user);
            if (updatedUser == null)
            {
                throw new DbUpdateException("Failed to update user to database.");
            }
            return updatedUser;
        }


        private bool CheckPassword(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        public async Task<bool> UpdatePassword(int id, string password, string confirmPassword)
        {
            var user = await _userRepository.SingleOrDefaultAsync(u => u.Id == id);
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
