using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Authentication;
using TaskManagement.Infrastructure.Services;

namespace TaskManagement.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtOptions _options;

        public UserController(IUserService userService, IOptions<JwtOptions> options) 
        {
            _userService = userService;
            _options = options.Value;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]        
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDTO) 
        { 
            var response = await _userService.LoginUserAsync(loginDTO);

            if (response.Result == ServiceResult.Success)
            {
                HttpContext.Response.Cookies.Append(_options.CookieName, response.Data);
                return Ok(response);
            }

            if (response.Result == ServiceResult.NotFound)
            {
                return NotFound(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterAsync([FromBody] UserDTO userDTO) 
        {
            var response = await _userService.RegisterUserAsync(userDTO);

            if (response.Result == ServiceResult.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
