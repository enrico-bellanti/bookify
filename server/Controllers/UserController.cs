using Bookify.Dtos;
using Bookify.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int? page = null,
            [FromQuery] int? size = null,
            [FromQuery] string sortBy = null,
            [FromQuery] bool? isDescending = null)
        {
            // Usa i valori passati se presenti, altrimenti usa null per far utilizzare i default del service
            var addresses = await _userService.GetAllUsers(
                page ?? 0,
                size ?? 25,
                sortBy ?? "Id",
                isDescending ?? false);

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "help-desk")]
        public async Task<IActionResult> Get(int id)
        {
            var hero = await _userService.GetUserByID(id);
            if (hero == null)
            {
                return NotFound("User not found");
            }
            return Ok(hero);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserRequestDto userObj)
        {
            try
            {
                if (userObj.Address == null)
                {
                    return BadRequest(new { message = "Address information is required" });
                }

                var user = await _userService.AddUser(userObj);
                if (user == null)
                {
                    return BadRequest();
                }

                return Ok(new
                {
                    message = "User Created Successfully.",
                    id = user.Id
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to create user" });
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateUserDto userObject)
        {
            var hero = await _userService.UpdateUser(id, userObject);
            if (hero == null)
            {
                return NotFound("User not found");
            }

            return Ok(new
            {
                message = "User Updated Successfully",
                id = hero.Id
            });
        }

    }
}
