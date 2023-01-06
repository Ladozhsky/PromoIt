using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PromoItAPI.Models;
using PromoItAPI.ModelsDto;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PromoItAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly promoitContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(promoitContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
            //string token = CreateToken(user);
            return Ok("token");
        }

        //private string CreateToken(User user)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.UserName),
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim("RoleId", user.RoleId.ToString())
        //    };

        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        //        _configuration.GetSection("AppSettings:Token").Value));

        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: creds);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //    return jwt;
        //}

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
            if (passwordSalt == null)
            {
                return false;
                Console.WriteLine("salt is null");
            }
            using (var hmac = new HMACSHA512(passwordSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
