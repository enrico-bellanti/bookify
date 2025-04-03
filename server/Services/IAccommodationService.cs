using Bookify.Data.Pagination;
using Bookify.Dtos.Accommodation;
using Bookify.Entities;

namespace Bookify.Services
{
    public interface IAccommodationService
    {
        Task<PagedResult<AccommodationDto>> GetPagedAccommodationsAsync(
            int page = 0,
            int size = 25,
            string sortBy = "Id",
            bool isDescending = false,
            params string[] includes);
        Task<AccommodationDto> GetAccommodationByIdAsync(
            int id,
            params string[] includes);
        Task<AccommodationDto?> AddAccommodationAsync(AccommodationCreate obj);
        Task<AccommodationDto?> UpdateAccommodationAsync(int id, AccommodationUpdate obj);
        Task<bool> DeleteAccommodationByIdAsync(int id);
    }
}
