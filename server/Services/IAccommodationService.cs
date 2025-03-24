using Bookify.Dtos;
using Bookify.Entities;

namespace Bookify.Services
{
    public interface IAccommodationService
    {
        Task<List<Accommodation>> GetAllAccommodations(string userUuid = null);
        Task<Accommodation?> GetAccommodationByID(int id);
        Task<Accommodation?> AddAccommodation(AccomodationDto obj);
        Task<Accommodation?> UpdateAccommodation(int id, AccomodationDto obj);
        Task<bool> DeleteAccommodationByID(int id);
    }
}
