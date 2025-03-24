using Bookify.Data;
using Bookify.Dtos;
using Bookify.Entities;
using Microsoft.EntityFrameworkCore;
namespace Bookify.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly BookifyDbContext _db;
        public AccommodationService(BookifyDbContext db)
        {
            _db = db;
        }
        public async Task<List<Accommodation>> GetAllAccommodations(string userUuid = null)
        {
            if (userUuid != null)
            {
                // Parse the string UUID to a valid GUID
                if (!Guid.TryParse(userUuid, out Guid userGuid))
                {
                    throw new ArgumentException("Invalid UUID format");
                }
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Uuid == userGuid);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with UUID {userUuid} not found");
                }
                return await _db.Accommodations
                    .Include(a => a.Address)
                    .Where(a => a.OwnerId == user.Id)
                    .ToListAsync();
            }
            return await _db.Accommodations
                .Include(a => a.Address)
                .ToListAsync();
        }

        public async Task<Accommodation?> GetAccommodationByID(int id)
        {
            return await _db.Accommodations
                .Include(a => a.Address)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Accommodation?> AddAccommodation(AccomodationDto obj)
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

            // Create new accommodation from DTO
            var accommodation = new Accommodation
            {
                Name = obj.Name,
                Type = obj.Type,
                OwnerId = obj.OwnerId,
                Address = address,
                //CreatedAt = DateTime.UtcNow,
                //UpdatedAt = DateTime.UtcNow
            };

            // Add to database and save changes
            await _db.Accommodations.AddAsync(accommodation);
            await _db.SaveChangesAsync();

            return accommodation;
        }

        public async Task<Accommodation?> UpdateAccommodation(int id, AccomodationDto obj)
        {
            // Find existing accommodation with address
            var accommodation = await _db.Accommodations
                .Include(a => a.Address)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (accommodation == null)
            {
                return null;
            }

            // Validate owner exists if changing owner
            if (obj.OwnerId != accommodation.OwnerId)
            {
                var owner = await _db.Users.FirstOrDefaultAsync(u => u.Id == obj.OwnerId);
                if (owner == null)
                {
                    throw new KeyNotFoundException($"Owner with ID {obj.OwnerId} not found");
                }
            }

            // Update address properties
            if (accommodation.Address != null)
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
            else
            {
                // Create new address if none exists
                accommodation.Address = new Address
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
            }

            // Update accommodation properties
            accommodation.Name = obj.Name;
            accommodation.Type = obj.Type;
            accommodation.OwnerId = obj.OwnerId;
            //accommodation.UpdatedAt = DateTime.UtcNow;

            // Save changes
            await _db.SaveChangesAsync();

            return accommodation;
        }

        public async Task<bool> DeleteAccommodationByID(int id)
        {
            var accommodation = await _db.Accommodations.FirstOrDefaultAsync(a => a.Id == id);
            if (accommodation == null)
            {
                return false;
            }

            _db.Accommodations.Remove(accommodation);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}