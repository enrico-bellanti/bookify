using Bookify.Dtos;
using Bookify.Entities;

namespace Bookify.Services
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAddresses();
        Task<Address?> GetAddressByID(int id);
        Task<Address?> AddAddress(AddressDto obj);
        Task<Address?> UpdateAddress(int id, AddressDto obj);
        Task<bool> DeleteAddressByID(int id);
    }
}
