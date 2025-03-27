using Bookify.Data.Pagination;
using Bookify.Dtos;
using Bookify.Entities;

namespace Bookify.Services
{
    public interface IAddressService
    {
        Task<PagedResult<Address>> GetAllAddresses(
            int page = 0,
            int size = 25,
            string sortBy = "Id",
            bool isDescending = false
        );
        Task<Address?> GetAddressByID(int id);
        Task<Address?> AddAddress(AddressDto obj);
        Task<Address?> UpdateAddress(int id, AddressDto obj);
        Task<bool> DeleteAddressByID(int id);
    }
}
