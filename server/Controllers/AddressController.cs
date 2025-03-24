using Bookify.Dtos;
using Bookify.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var addresses = await _addressService.GetAllAddresses();
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var address = await _addressService.GetAddressByID(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddressDto addressObject)
        {
            var address = await _addressService.AddAddress(addressObject);

            if (address == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Address Created Successfully",
                id = address.Id
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] AddressDto addressObject)
        {
            var address = await _addressService.UpdateAddress(id, addressObject);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Address Updated Successfully",
                id = address.Id
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!await _addressService.DeleteAddressByID(id))
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Address Deleted Successfully",
                id
            });
        }
    }
}
