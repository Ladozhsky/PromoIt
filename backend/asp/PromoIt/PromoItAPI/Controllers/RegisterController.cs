using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromoItAPI.Models;
using PromoItAPI.ModelsDto;

namespace PromoItAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly promoitContext _context;

        public RegisterController(promoitContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return UserToDTO(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(UserDto userDto, string username)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (existingUser == null)
            {

                User user = new User
                {
                    UserId = userDto.UserId,
                    UserName = userDto.UserName,
                    Password = userDto.Password,
                    Email = userDto.Email,
                    Address = userDto.Address,
                    TelNumber = userDto.TelNumber,
                    RoleId = userDto.RoleId,
                    CompanyId = userDto.CompanyId
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, UserToDTO(user));
            }
            else
            {
                return BadRequest("User with this username is already exists");
            }
        }

        private static UserDto UserToDTO(User user) =>
        new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Password = user.Password,
            Email = user.Email,
            Address = user.Address,
            TelNumber = user.TelNumber,
            RoleId = user.RoleId,
            CompanyId = user.CompanyId
        };
    }
}
