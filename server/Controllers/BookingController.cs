using System.Security.Claims;
using Bookify.Dtos;
using Bookify.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IKeycloakUserService _keycloakUserService;
        private readonly IUserService _userService;
        public BookingController(
            IBookingService bookingService,
            IKeycloakUserService keycloakUserService,
            IUserService userService
            )
        {
            _bookingService = bookingService;
            _keycloakUserService = keycloakUserService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
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
                return Ok(await _bookingService.GetAllBookings());
            }
            return Ok(await _bookingService.GetAllBookings(userUuid));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userUuid == null)
            {
                return Unauthorized();
            }
            // First check if booking exists
            var booking = await _bookingService.GetBookingByID(id);
            if (booking == null)
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
                return Ok(booking);
            }

            var user = await _userService.GetUserByUuid(userUuid);
            if (user == null)
            {
                return Unauthorized(); // User not found in the system
            }
            // Check if user is the owner of the accommodation
            var isOwner = booking.UserId == user.Id;
            // Only allow access if user is admin or owner
            if (!isOwner)
            {
                return Forbid(); // Return 403 Forbidden status code
            }
            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookingRequestDto bookingRequestDto)
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

            var requestBooking = new BookingDto()
            {
                CheckInDate = bookingRequestDto.CheckInDate,
                CheckOutDate = bookingRequestDto.CheckOutDate,
                TotalPrice = bookingRequestDto.TotalPrice,
                UserId = user.Id,
                AccommodationId = bookingRequestDto.AccommodationId,
            };

            try
            {
                var newAccommodation = await _bookingService.AddBooking(requestBooking);
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
        public async Task<IActionResult> Put(int id, [FromBody] BookingDto bookingDto)
        {
            var userUuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userUuid == null)
            {
                return Unauthorized();
            }

            // First check if accommodation exists
            var existingAccommodation = await _bookingService.GetBookingByID(id);
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
            var isOwner = existingAccommodation.UserId == user.Id;

            // Only allow update if user is admin or owner
            if (!isAdmin || !isOwner)
            {
                return Forbid(); // Return 403 Forbidden status code
            }

            try
            {
                var updatedBooking = await _bookingService.UpdateBooking(id, bookingDto);
                if (updatedBooking == null)
                {
                    return NotFound();
                }
                return Ok(updatedBooking);
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
            var booking = await _bookingService.GetBookingByID(id);
            if (booking == null)
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
            var isOwner = booking.UserId == user.Id;

            // Only allow deletion if user is admin or owner
            if (!isAdmin || !isOwner)
            {
                return Forbid(); // Return 403 Forbidden status code
            }

            var result = await _bookingService.DeleteBookingByID(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent(); // 204 No Content is the standard response for successful DELETE operations
        }


    }
}
