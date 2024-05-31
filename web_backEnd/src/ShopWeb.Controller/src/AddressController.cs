using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWeb.Core.src.Common;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Controller.src
{
    [ApiController]
    [Route("api/v1/addresses")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<AddressReadDto>> GetAllAddressesByUserIdAsync()
        {
            string customerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (customerId == null)
            {
                throw AppException.Unauthorized("User is not authorized");
            }

            Guid idValue = Guid.Parse(customerId);
            var addresses = await _addressService.GetAllAddressesByUserIdAsync(idValue);
            return addresses;
        }

        [HttpGet("{addressId:guid}")]
        public async Task<ActionResult<AddressReadDto>> GetAddressByIdAsync([FromRoute] Guid addressId)
        {
            var address = await _addressService.GetAddressByIdAsync(addressId);
            if (address == null)
            {
                throw AppException.NotFound($"Address with ID {addressId} not found");
            }

            return address;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AddressReadDto>> CreateAddressAsync([FromBody] AddressCreateDto addressDto)
        {
            if (addressDto == null)
            {
                throw AppException.BadRequest("Invalid request body");
            }
            string customerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (customerId == null)
            {
                throw AppException.Unauthorized("User is not authorized");
            }

            Guid idValue = Guid.Parse(customerId);
            var createdAddress = await _addressService.CreateAddressAsync(addressDto, idValue);
            if (createdAddress == null)
            {
                throw AppException.InternalServerError("Failed to create address");
            }

            return createdAddress;
        }

        [Authorize]
        [HttpPut("{addressId:guid}")]
        public async Task<ActionResult<AddressReadDto>> UpdateAddressByIdAsync([FromRoute] Guid addressId, [FromBody] AddressUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                throw AppException.BadRequest("Invalid request body");
            }

            var updatedAddress = await _addressService.UpdateAddressByIdAsync(updateDto, addressId);
            if (updatedAddress == null)
            {
                throw AppException.NotFound($"Address with ID {addressId} not found");
            }

            return Ok(updatedAddress);
        }

        [Authorize]
        [HttpDelete("{addressId:guid}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid addressId)
        {
            var result = await _addressService.DeleteAddressByIdAsync(addressId);
            if (!result)
            {
                throw AppException.NotFound($"Address with ID {addressId} not found");
            }

            return NoContent();
        }
    }
}
