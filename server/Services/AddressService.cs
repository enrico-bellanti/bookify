using System.Data;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq.Expressions;
using Bookify.Data;
using Bookify.Data.Pagination;
using Bookify.Dtos;
using Bookify.Entities;
using Bookify.Repositories;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Bookify.Repositories.IRepository;

namespace Bookify.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<PagedResult<Address>> GetAllAddresses(
            int page = 0, 
            int size = 25, 
            string sortBy = "Id", 
            bool isDescending = false
        )
        {
            var sortDirection = isDescending ? SortDirection.DESC : SortDirection.ASC;
            var sort = Sort.By(sortDirection, sortBy);
            var pageRequest = PageRequest.Of(page, size, sort);

            return await _addressRepository.GetAllAsync(pageRequest);
        }

        public async Task<Address?> GetAddressByID(int id)
        {
            return await _addressRepository.SingleOrDefaultAsync(address => address.Id == id);
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

            return await _addressRepository.CreateAsync(address);
        }
        public async Task<Address?> UpdateAddress(int id, AddressDto obj)
        {
            var address = await _addressRepository.SingleOrDefaultAsync(address => address.Id == id);
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

                return await _addressRepository.UpdateAsync(address);
            }
            return null;
        }

        public async Task<bool> DeleteAddressByID(int id)
        {
            return await _addressRepository.DeleteByIdAsync(id);
        }
    }
}
