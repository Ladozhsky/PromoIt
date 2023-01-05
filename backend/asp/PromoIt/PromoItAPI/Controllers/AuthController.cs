using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoItAPI.Models;
using PromoItAPI.ModelsDto;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

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
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == request.userName);

            if (existingUser == null)
            {
                return BadRequest("User not found");
            }
            if (!VerifyPasswordHash(request.password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }

            return Ok("Token");
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
