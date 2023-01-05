//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.CodeAnalysis.Scripting;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using PromoItAPI.Models;
//using PromoItAPI.ModelsDto;
//using System.Security.Cryptography;
//using System.Text;

//namespace PromoItAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LoginController : ControllerBase
//    {
//        private readonly promoitContext _context;

//        public LoginController(promoitContext context)
//        {
//            _context = context;
//        }

//        [HttpPost]
//        public async Task<ActionResult<UserDto>> Login(LoginDto login)
//        {
//            private bool ValidateCredentials(string username, string password)
//            {
//                // Look up the user's password hash in the database
//                string passwordHash = GetPasswordHashFromDatabase(login.username);

//                // Compare the password hash to the provided password
//                return BCrypt.Net.BCrypt.Verify(password, passwordHash);
//            }


//            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == login.username);

//            if (existingUser != null)
//            {

//                LoginDto user = new LoginDto();
//                {

//                    login.username = user.username;
//                    login.password = user.password;
                   
//                };

//                byte[] passwordBytes = Encoding.UTF8.GetBytes(login.password);

//                SHA256 sha256 = SHA256.Create();

//                byte[] hash = sha256.ComputeHash(passwordBytes);

//                string hashString = Convert.ToBase64String(hash);

//                login.password = hashString;

//                string passwordHash = 

//                var rightPassword = _context.Users.FindAsync(u => u.Password == login.password);
//            }
//            else
//            {
//                return BadRequest("User with this username is already exists");
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<UserDto>> GetUser(int id)
//        {
//            var user = await _context.Users.FindAsync(id);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            return UserToDTO(user);
//        }

//        private static UserDto UserToDTO(User user) =>
//        new UserDto
//        {
//            UserId = user.UserId,
//            UserName = user.UserName,
//            Password = user.Password,
//            Email = user.Email,
//            Address = user.Address,
//            TelNumber = user.TelNumber,
//            RoleId = user.RoleId,
//            CompanyId = user.CompanyId
//        };

//        public string GetPasswordHashFromDatabase(string username)
//        {
//            using (SqlConnection connection = new SqlConnection("promoConnection"))
//            {
//                connection.Open();

//                string sql = "SELECT password FROM user WHERE user_name = @username";
//                using (SqlCommand command = new SqlCommand(sql, connection))
//                {
//                    command.Parameters.AddWithValue("@username", username);
//                    return (string)command.ExecuteScalar();
//                }
//            }
//        }

//    }
//}
