using System.Diagnostics.Metrics;
using System.IO;
using Bookify.Data;
using Bookify.Dtos;
using Bookify.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bookify.Services
{
    public class AddressService : IAddressService
    {
        private readonly BookifyDbContext _db;
        public AddressService(BookifyDbContext db)
        {
            _db = db;
        }
        public async Task<List<Address>> GetAllAddresses()
        {
            return await _db.Addresses.ToListAsync();
        }
        public async Task<Address?> GetAddressByID(int id)
        {
            return await _db.Addresses.FirstOrDefaultAsync(hero => hero.Id == id);
        }

        public async Task<Address?> AddAddress(AddressDto obj)
        {
            var address = new Address
            {
                Street = obj.Street,
                Number = obj.Number,
                City = obj.City,
                Province = obj.Province,
                PostalCode = obj.PostalCode,
                Country = obj.Country,
                AdditionalInfo = obj.AdditionalInfo,
                Latitude = obj.Latitude,
                Longitude = obj.Longitude
            };

            _db.Addresses.Add(address);
            var result = await _db.SaveChangesAsync();
            return result >= 0 ? address : null;
        }
        public async Task<Address?> UpdateAddress(int id, AddressDto obj)
        {
            var address = await _db.Addresses.FirstOrDefaultAsync(index => index.Id == id);
            if (address != null)
            {
                address.Street = obj.Street;
                address.Number = obj.Number;
                address.City = obj.City;
                address.Province = obj.Province;
                address.PostalCode = obj.PostalCode;
                address.Country = obj.Country;
                address.AdditionalInfo = obj.AdditionalInfo;
                address.Latitude = obj.Latitude;
                address.Longitude = obj.Longitude;

                var result = await _db.SaveChangesAsync();
                return result >= 0 ? address : null;
            }
            return null;
        }

        public async Task<bool> DeleteAddressByID(int id)
        {
            var address = await _db.Addresses.FirstOrDefaultAsync(index => index.Id == id);
            if (address != null)
            {
                _db.Addresses.Remove(address);
                var result = await _db.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }
    }
}
