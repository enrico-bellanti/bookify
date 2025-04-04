using Bookify.Data;
using Bookify.Data.Pagination;
using Bookify.Dtos.Accommodation;
using Bookify.Entities;
using Bookify.Repositories;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
namespace Bookify.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IUserRepository _userRepository;
        private readonly Cloudinary _cloudinary;

        public AccommodationService(
            IAccommodationRepository accommodationRepository, 
            IUserRepository userRepository,
            Cloudinary cloudinary
        )
        {
            _accommodationRepository = accommodationRepository;
            _userRepository = userRepository;
            _cloudinary = cloudinary;
        }

        // Public method that can accept include parameters
        public async Task<PagedResult<AccommodationDto>> GetPagedAccommodationsAsync(
            int page = 0,
            int size = 25,
            string sortBy = "Id",
            bool isDescending = false,
            params string[] includes)
        {
            var accommodationsResult = await GetAllAccommodations(
                page,
                size,
                sortBy,
                isDescending,
                includes
            );

            // Convert items to DTOs
            var dtoItems = accommodationsResult.Items.Select(accommodation =>
                new AccommodationDto(accommodation)).ToList();

            // Create a new PagedResult with DTOs
            return new PagedResult<AccommodationDto>
            {
                Items = dtoItems,
                TotalCount = accommodationsResult.TotalCount,
                PageNumber = accommodationsResult.PageNumber,
                PageSize = accommodationsResult.PageSize
            };
        }

        private async Task<PagedResult<Accommodation>> GetAllAccommodations(
            int page = 0,
            int size = 25,
            string sortBy = "Id",
            bool isDescending = false,
            params string[] includes)
        {
            var sortDirection = isDescending ? SortDirection.DESC : SortDirection.ASC;
            var sort = Sort.By(sortDirection, sortBy);
            var pageRequest = PageRequest.Of(page, size, sort);

            //if (userUuid != null)
            //{
            //    // Parse the string UUID to a valid GUID
            //    if (!Guid.TryParse(userUuid, out Guid userGuid))
            //    {
            //        throw new ArgumentException("Invalid UUID format");
            //    }
            //    var user = await _userRepository.SingleOrDefaultAsync(u => u.Uuid == userGuid);
            //    if (user == null)
            //    {
            //        throw new KeyNotFoundException($"User with UUID {userUuid} not found");
            //    }
            //    return await _accommodationRepository.GetAllAsync(
            //        pageRequest,
            //        filter: b => b.OwnerId == user.Id,
            //        includeProperties: includes
            //    );
            //}

            // Using the new repository method with includes instead of the specific GetAllWithAddressAsync
            return await _accommodationRepository.GetAllAsync(
                pageRequest,
                filter: null,
                cancellationToken: default,
                includeProperties: includes
            );
        }

        public async Task<AccommodationDto?> GetAccommodationByIdAsync(int id, params string[] includes)
        {
            var accommodation = await GetAccommodationByID(id, includes);
            if (accommodation == null)
            {
                return null;
            }
            return new AccommodationDto(accommodation);
        }

        private async Task<Accommodation?> GetAccommodationByID(int id, params string[] includes)
        {
            // More efficient than SingleOrDefaultAsync with a predicate when fetching by primary key
            return await _accommodationRepository.GetByIdAsync(id, default, includes);

            // Alternative approach if you prefer to use the predicate version
            // return await _accommodationRepository.SingleOrDefaultAsync(
            //     accommodation => accommodation.Id == id, 
            //     default,
            //     includes);
        }

        public async Task<AccommodationDto?> AddAccommodationAsync(AccommodationCreate obj, int ownerId)
        {
            var accommodation = await AddAccommodation(obj, ownerId);
            if (accommodation == null)
            {
                return null;
            }

            return new AccommodationDto(accommodation);
        }

        private async Task<Accommodation?> AddAccommodation(AccommodationCreate obj, int ownerId)
        {
            // Create address from DTO
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

            var file = obj.ImgFile;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult == null)
            {
                return null;
            }

            var imgUrl = uploadResult.SecureUrl.ToString();

            // Create new accommodation from DTO
            var accommodation = new Accommodation
            {
                Name = obj.Name,
                Type = obj.Type,
                ImgUrl = imgUrl,
                OwnerId = ownerId,
                Address = address,
                //CreatedAt = DateTime.UtcNow,
                //UpdatedAt = DateTime.UtcNow
            };

            // Add to database and save changes
            return await _accommodationRepository.CreateAsync(accommodation);
        }

        public async Task<AccommodationDto?> UpdateAccommodationAsync(int id, AccommodationUpdate obj)
        {
            var accommodation = await UpdateAccommodation(id, obj);
            if (accommodation == null)
            {
                return null;
            }

            return new AccommodationDto(accommodation);
        }

        private async Task<Accommodation?> UpdateAccommodation(int id, AccommodationUpdate obj)
        {
            // Find existing accommodation with address
            var accommodation = await _accommodationRepository.SingleOrDefaultAsync(accommodation => accommodation.Id == id);

            if (accommodation == null)
            {
                return null;
            }

            // Update address properties
            if (obj.Address != null)
            {
                accommodation.Address.Street = obj.Address.Street;
                accommodation.Address.Number = obj.Address.Number;
                accommodation.Address.City = obj.Address.City;
                accommodation.Address.Province = obj.Address.Province;
                accommodation.Address.PostalCode = obj.Address.PostalCode;
                accommodation.Address.Country = obj.Address.Country;
                accommodation.Address.AdditionalInfo = obj.Address.AdditionalInfo;
                accommodation.Address.Latitude = obj.Address.Latitude;
                accommodation.Address.Longitude = obj.Address.Longitude;
            }

            // Update accommodation properties
            accommodation.Name = obj.Name;
            accommodation.Type = obj.Type;

            if (obj.ImgFile != null)
            {
                var file = obj.ImgFile;

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = true
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult == null)
                {
                    return null;
                }

                var imgUrl = uploadResult.SecureUrl.ToString();
                accommodation.ImgUrl = imgUrl;
            }
            //accommodation.UpdatedAt = DateTime.UtcNow;

            // Save changes
            return await _accommodationRepository.UpdateAsync(accommodation);
        }

        public async Task<bool> DeleteAccommodationByIdAsync(int id)
        {
            var accommodation = await _accommodationRepository.SingleOrDefaultAsync(a => a.Id == id);
            if (accommodation == null)
            {
                return false;
            }

            return await _accommodationRepository.DeleteByIdAsync(id);
        }
    }
}