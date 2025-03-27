using Bookify.Data.Pagination;
using Bookify.Dtos;
using Bookify.Entities;

namespace Bookify.Services
{
    public interface IAccommodationService
    {
        Task<PagedResult<Accommodation>> GetAllAccommodations(
            string userUuid = null,
            int page = 0,
            int size = 25,
            string sortBy = "Id",
            bool isDescending = false
        );
        Task<Accommodation?> GetAccommodationByID(int id);
        Task<Accommodation?> AddAccommodation(AccomodationDto obj);
        Task<Accommodation?> UpdateAccommodation(int id, AccomodationDto obj);
        Task<bool> DeleteAccommodationByID(int id);
    }
}
