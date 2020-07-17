using EMultiplex.Models.Requests;
using EMultiplex.Models.Responses;
using EMultiplex.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EMultiplex.Api.Identity.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/identity")]
    public class IdentityController : Controller
    {

        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {

            var result = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!result.IsSuccess)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "Error occurred" }
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {

            var result = await _identityService.LoginAsync(request.Email, request.Password);

            if (!result.IsSuccess)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "Error occurred" }
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            });
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] UserRefreshTokenRequest request)
        {

            var result = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!result.IsSuccess)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "Error occurred" }
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            });
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("createRole")]
        public async Task<IActionResult> CreateRoleAsync(string roleName)
        {

            var result = await _identityService.AddRoleAsync(roleName);

            if (!result.IsSuccess)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "Error occurred" }
                });
            }
            return Ok(result.IsSuccess);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addRoleToUser")]
        public async Task<IActionResult> AddRoleToUserAsync([FromBody] AssignUserRoleRequest request)
        {

            var result = await _identityService.AddUserRoleAsync(request.Email, request.RoleName);

            if (!result.IsSuccess)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "Error occurred" }
                });
            }
            return Ok(result.IsSuccess);
        }
    }
}
