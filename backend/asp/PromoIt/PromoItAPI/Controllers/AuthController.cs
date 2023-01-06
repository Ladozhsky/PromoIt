﻿using Microsoft.AspNetCore.Http;
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
                CreatePasswordHash(request.Password);

                user.UserName = request.UserName;
                //user.PasswordHash = request.passwordHash;
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
            var existingUser = _context.Users.SingleOrDefault(u => u.UserName == request.userName && u.PasswordHash == CreatePasswordHash(request.password));

            if (existingUser == null)
            {
                return BadRequest("User not found or wrong password");
            }
            //if (!VerifyPasswordHash(request.password, user.PasswordHash))
            //{
            //    if (user.PasswordHash == null)
            //    {
            //        return BadRequest("password hash is null");
            //    }
            //    return BadRequest("Wrong password");
            //}
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

        private string CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                string passwordHash;
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                SHA256 sha256 = SHA256.Create();

                byte[] hash = sha256.ComputeHash(passwordBytes);

                return passwordHash = Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPasswordHash(string password, string passwordHash)
        {
            if (passwordHash == null)
            {
                return false;
            }

            string computedHash;

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            SHA256 sha256 = SHA256.Create();

            byte[] hash = sha256.ComputeHash(passwordBytes);

            computedHash = Convert.ToBase64String(hash);

            return computedHash.SequenceEqual(passwordHash);
      
        }
    }
}
