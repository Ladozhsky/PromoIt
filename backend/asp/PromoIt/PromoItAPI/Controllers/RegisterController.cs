using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<UserDto>> Register(UserDto userDto)
        {
            var user = new User
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
