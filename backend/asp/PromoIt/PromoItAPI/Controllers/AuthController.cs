using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoItAPI.Models;
using PromoItAPI.ModelsDto;
using System.Security.Cryptography;

namespace PromoItAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly promoitContext _context;

        public AuthController(promoitContext context)
        {
            _context = context;
        }

        public static User user = new User();
        public static LoginDto userLogin = new LoginDto();


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserDto request, string username)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (existingUser == null)
            {
                CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);

                user.UserName = request.UserName;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Email = request.Email;
                user.Address = request.Address;
                user.TelNumber = request.TelNumber;
                user.RoleId = request.RoleId;
                user.CompanyId = request.CompanyId;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            else
            {
                return BadRequest("User with this username is already exists");
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto request)
        {
            var users = _context.Users;
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == request.userName);

            if (existingUser == null)
            {
                return BadRequest("User not found");
            }
            return Ok("Token");
        }

        private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        private static LoginDto UserDTOtoLoginDTO(UserDto login) =>
        new LoginDto
        {
            userName = login.UserName,
            password = login.Password
        };
    }
}
