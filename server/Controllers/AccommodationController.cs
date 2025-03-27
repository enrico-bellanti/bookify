using System.Linq;
using System.Security.Claims;
using Bookify.Dtos;
using Bookify.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> Get(
            [FromQuery] int? page = null,
            [FromQuery] int? size = null,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? isDescending = null
        )
        {
            var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userUuid == null)
            {
                return Unauthorized();
            }
            string[] userRoles = User.FindAll(ClaimTypes.Role)
                                     .Select(claim => claim.Value)
                                     .ToArray();
            var requiredRoles = new[] { "help-desk" };
            // Await the async method to get the boolean result
            var isAdmin = _keycloakUserService.CompareUserRoles(requiredRoles, userRoles);
            if (isAdmin)
            {
                return Ok(await _accommodationService.GetAllAccommodations(
                    null,
                    page ?? 0,
                    size ?? 25,
                    sortBy ?? "Id",
                    isDescending ?? false
                ));
            }
            return Ok(await _accommodationService.GetAllAccommodations(
                userUuid,
                page ?? 0,
                size ?? 25,
                sortBy ?? "Id",
                isDescending ?? false
            ));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userUuid == null)
            {
                return Unauthorized();
            }
            // First check if accommodation exists
            var accommodation = await _accommodationService.GetAccommodationByID(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            // Check if user is admin
            string[] userRoles = User.FindAll(ClaimTypes.Role)
                                     .Select(claim => claim.Value)
                                     .ToArray();
            var requiredRoles = new[] { "help-desk" };
            // Await the async method to get the boolean result
            var isAdmin = _keycloakUserService.CompareUserRoles(requiredRoles, userRoles);

            if (isAdmin)
            {
                return Ok(accommodation);
            }

            var user = await _userService.GetUserByUuid(userUuid);
            if (user == null)
            {
                return Unauthorized(); // User not found in the system
            }
            // Check if user is the owner of the accommodation
            var isOwner = accommodation.OwnerId == user.Id;
            // Only allow access if user is admin or owner
            if (!isOwner)
            {
                return Forbid(); // Return 403 Forbidden status code
            }
            return Ok(accommodation);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccommodationRequestDto accommodationRequestDto)
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

            var requestAccommodation = new AccomodationDto()
            {
                Name = accommodationRequestDto.Name,
                Type = accommodationRequestDto.Type,
                OwnerId = user.Id,
                Address = accommodationRequestDto.Address
            };

            try
            {
                var newAccommodation = await _accommodationService.AddAccommodation(requestAccommodation);
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
        public async Task<IActionResult> Put(int id, [FromBody] AccomodationDto accommodationDto)
        {
            var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userUuid == null)
            {
                return Unauthorized();
            }

            // First check if accommodation exists
            var existingAccommodation = await _accommodationService.GetAccommodationByID(id);
            if (existingAccommodation == null)
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
            var isOwner = existingAccommodation.OwnerId == user.Id;

            // Only allow update if user is admin or owner
            if (!isAdmin || !isOwner)
            {
                return Forbid(); // Return 403 Forbidden status code
            }

            try
            {
                var updatedAccommodation = await _accommodationService.UpdateAccommodation(id, accommodationDto);
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
        public async Task<IActionResult> Delete(int id)
        {
            var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userUuid == null)
            {
                return Unauthorized();
            }

            // First check if accommodation exists
            var accommodation = await _accommodationService.GetAccommodationByID(id);
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
            if (!isAdmin || !isOwner)
            {
                return Forbid(); // Return 403 Forbidden status code
            }

            var result = await _accommodationService.DeleteAccommodationByID(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent(); // 204 No Content is the standard response for successful DELETE operations
        }
    }
}