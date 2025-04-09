using System.Linq;
using System.Security.Claims;
using Bookify.Data.Pagination;
using Bookify.Dtos;
using Bookify.Dtos.Accommodation;
using Bookify.Helpers;
using Bookify.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly IAccommodationService _accommodationService;
        private readonly IKeycloakUserService _keycloakUserService;
        private readonly IUserService _userService;
        public AccommodationController(
            IAccommodationService accommodationService,
            IKeycloakUserService keycloakUserService,
            IUserService userService
            )
        {
            _accommodationService = accommodationService;
            _keycloakUserService = keycloakUserService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<AccommodationDto>>> Get(
            [FromQuery] int? page = null,
            [FromQuery] int? size = null,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? isDescending = null,
            [FromQuery] string includes = null,
            [FromQuery] string filters = null
        )
        {
            //var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (userUuid == null)
            //{
            //    return Unauthorized();
            //}
            //string[] userRoles = User.FindAll(ClaimTypes.Role)
            //                         .Select(claim => claim.Value)
            //                         .ToArray();
            //var requiredRoles = new[] { "help-desk" };
            //var isAdmin = _keycloakUserService.CompareUserRoles(requiredRoles, userRoles);
            //if (isAdmin)
            //{
            //return Ok(await _accommodationService.GetPagedAccommodationsAsync(
            //    null,
            //    page ?? 0,
            //    size ?? 25,
            //    sortBy ?? "Id",
            //    isDescending ?? false,
            //    ParseIncludes(includes)
            //));
            //}

            // Parse the filters from the query string
            var filterQuery = QueryHelper.ParseFilters(filters);

            return Ok(await _accommodationService.GetPagedAccommodationsAsync(
                page ?? 0,
                size ?? 25,
                sortBy ?? "Id",
                isDescending ?? false,
                filterQuery,
                QueryHelper.ParseIncludes(includes)
            ));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AccommodationDto>> Get(int id, [FromQuery] string includes = null)
        {
            //var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (userUuid == null)
            //{
            //    return Unauthorized();
            //}
            // Get the accommodation DTO with any requested includes
            //var accommodationDto = await _accommodationService.GetAccommodationByIdAsync(id, QueryHelper.ParseIncludes(includes));
            //if (accommodationDto == null)
            //{
            //    return NotFound();
            //}
            //// Check if user is admin
            //string[] userRoles = User.FindAll(ClaimTypes.Role)
            //                       .Select(claim => claim.Value)
            //                       .ToArray();
            //var requiredRoles = new[] { "help-desk" };
            //var isAdmin = _keycloakUserService.CompareUserRoles(requiredRoles, userRoles);
            //// If admin, return the DTO directly
            //if (isAdmin)
            //{
            //    return accommodationDto; // You can return the type directly
            //}
            //// Get the current user
            //var user = await _userService.GetUserByUuid(userUuid);
            //if (user == null)
            //{
            //    return Unauthorized(); // User not found in the system
            //}
            //// Check if user is the owner of the accommodation
            //var isOwner = accommodationDto.OwnerId == user.Id;
            //// Only allow access if user is owner
            //if (!isOwner)
            //{
            //    return Forbid(); // Return 403 Forbidden status code
            //}
            var accommodationDto = await _accommodationService.GetAccommodationByIdAsync(id, QueryHelper.ParseIncludes(includes));
            return Ok(accommodationDto);
        }

        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<AccommodationDto>> Post([FromForm] AccommodationCreate accommodationCreate)
        {
            var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userUuid == null)
            {
                return Unauthorized();
            }

            // Get the current user
            var user = await _userService.GetUserByUuid(userUuid);
            if (user == null)
            {
                return NotFound("User not found");
            }

            try
            {
                var newAccommodation = await _accommodationService.AddAccommodationAsync(accommodationCreate, user.Id);
                return Ok(newAccommodation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<AccommodationDto>> Put(int id, [FromBody] AccommodationUpdate accommodationUpdate)
        {
            var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userUuid == null)
            {
                return Unauthorized();
            }

            // Get the current user
            var user = await _userService.GetUserByUuid(userUuid);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // First check if accommodation exists
            var existingAccommodation = await _accommodationService.GetAccommodationByIdAsync(id);
            if (existingAccommodation == null)
            {
                return NotFound();
            }

            string[] userRoles = User.FindAll(ClaimTypes.Role)
                                    .Select(claim => claim.Value)
                                    .ToArray();
            var requiredRoles = new[] { "help-desk" };
            var isAdmin = _keycloakUserService.CompareUserRoles(requiredRoles, userRoles);

            // Check if user is the owner of the accommodation
            var isOwner = existingAccommodation.OwnerId == user.Id;

            // Only allow update if user is admin or owner
            if (!isAdmin && !isOwner) // Corretta la logica OR -> AND
            {
                return Forbid(); // Return 403 Forbidden status code
            }

            try
            {
                var updatedAccommodation = await _accommodationService.UpdateAccommodationAsync(id, accommodationUpdate);
                if (updatedAccommodation == null)
                {
                    return NotFound();
                }
                return Ok(updatedAccommodation);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userUuid == null)
            {
                return Unauthorized();
            }

            // First check if accommodation exists
            var accommodation = await _accommodationService.GetAccommodationByIdAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            // Get the current user
            var user = await _userService.GetUserByUuid(userUuid);
            if (user == null)
            {
                return NotFound("User not found");
            }

            string[] userRoles = User.FindAll(ClaimTypes.Role)
                                     .Select(claim => claim.Value)
                                     .ToArray();
            var requiredRoles = new[] { "help-desk" };
            // Await the async method to get the boolean result
            var isAdmin = _keycloakUserService.CompareUserRoles(requiredRoles, userRoles);

            // Check if user is the owner of the accommodation
            var isOwner = accommodation.OwnerId == user.Id;

            // Only allow deletion if user is admin or owner
            if (!isAdmin && !isOwner)
            {
                return Forbid(); // Return 403 Forbidden status code
            }

            var result = await _accommodationService.DeleteAccommodationByIdAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent(); // 204 No Content is the standard response for successful DELETE operations
        }
    }
}