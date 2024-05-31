using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWeb.Core.src.Common;
using ShopWeb.Core.src.Entity;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Controller.src
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            // _configuration = configuration;
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var rs = await _authenticationService.RegisterServiceAsync(request);
            if (!rs)
            {
                return NotFound("Email has registerd!");
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto request)
        {
            try
            {
                var rs = await _authenticationService.LoginServiceAsync(request);
                return Ok(new { token = rs });
            }
            catch (AppException ex)
            {
                // Log the exception for debugging purposes
                // Console.WriteLine($"Password is wrong during login: {ex.Message}");

                // Check if the exception indicates unauthorized access
                if (ex.StatusCode == HttpStatusCode.Unauthorized)
                {
                    // Return a 401 Unauthorized response
                    return StatusCode((int)ex.StatusCode, ex.Message);
                }
                else
                {
                    // For other exceptions, return a generic error message
                    return StatusCode(500, "An error occurred during login. Please try again later.");
                }
            }
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfileFromTokenAsync()
        {

            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User is not logged in");
            }
            Guid guidId;
            if (!Guid.TryParse(userId, out guidId))
            {
                return BadRequest("Invalid user ID");
            }
            var userProfile = await _authenticationService.GetUserProfileFromTokenAsync(guidId);

            return Ok(userProfile);
        }

    }
}