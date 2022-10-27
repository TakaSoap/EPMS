using EPMS_API.Models;
using EPMS_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EPMS_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> Get() => await _userService.GetAsync();

        [HttpGet("token")]
        public string GetToken() => HttpContext.Request.Headers["Authorization"];

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(NewUser newUser)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claimValue = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            string userAuthingId;

            if (claimValue != null)
            {
                userAuthingId = claimValue;
            }
            else
            {
                return BadRequest();
            }

            var authingUser = await AuthingClient.Client.Users.Detail(userAuthingId);

            var user = new User()
            {
                Name = newUser.name,
                UserType = newUser.userType,
                baseId = newUser.baseId,
                PhoneNumber = authingUser.Phone,
                Email = authingUser.Email,
                UserIdAuthing = userAuthingId
            };

            await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
    }
}