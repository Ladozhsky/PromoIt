//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PromoItAPI.Models;
//using PromoItAPI.ModelsDto;

//namespace PromoItAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        private readonly promoitContext _context;

//        public UsersController(promoitContext context)
//        {
//            _context = context;
//        }

//        //GET: api/Users
//       //[HttpGet]
//       // public async Task<ActionResult<IEnumerable<User>>> GetUsers()
//       // {
//       //     return await _context.Users.ToListAsync();
//       // }

//		[HttpGet]
//		public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
//		{
//			return await _context.Users.Select(u => UserToDTO(u)).ToListAsync();
//		}


//		// GET: api/Users/5
//		//[HttpGet("{id}")]
//  //      public async Task<ActionResult<User>> GetUser(int id)
//  //      {
//  //          var user = await _context.Users.FindAsync(id);

//  //          if (user == null)
//  //          {
//  //              return NotFound();
//  //          }

//  //          return user;
//  //      }

//		[HttpGet("{id}")]
//		public async Task<ActionResult<UserDto>> GetUser(int id)
//		{
//			var user = await _context.Users.FindAsync(id);

//			if (user == null)
//			{
//				return NotFound();
//			}

//			return UserToDTO(user);
//		}

//		// PUT: api/Users/5
//		//[HttpPut("{id}")]
//		//public async Task<IActionResult> PutUser(int id, User user)
//		//{
//		//    if (id != user.UserId)
//		//    {
//		//        return BadRequest();
//		//    }

//		//    _context.Entry(user).State = EntityState.Modified;

//		//    try
//		//    {
//		//        await _context.SaveChangesAsync();
//		//    }
//		//    catch (DbUpdateConcurrencyException)
//		//    {
//		//        if (!UserExists(id))
//		//        {
//		//            return NotFound();
//		//        }
//		//        else
//		//        {
//		//            throw;
//		//        }
//		//    }

//		//    return NoContent();
//		//}

//		[HttpPut("{id}")]
//		public async Task<IActionResult> PutUser(int id, UserDto userDto)
//		{
//			if (id != userDto.UserId)
//			{
//				return BadRequest();
//			}

//            var user = await _context.Users.FindAsync(id);
//			if (user == null)
//            {
//                return NotFound();
//            }

//            user.UserName = userDto.UserName;
//            user.Password = userDto.Password;
//            user.Email= userDto.Email;
//            user.Address = userDto.Address;
//            user.TelNumber = userDto.TelNumber;
//            user.RoleId = userDto.RoleId;
//            user.CompanyId = userDto.CompanyId;

//			try
//			{
//				await _context.SaveChangesAsync();
//			}
//			catch (DbUpdateConcurrencyException) when (!UserExists(id))
//			{
//                return NotFound();
//			}

//			return NoContent();
//		}

//		// POST: api/Users
//		//[HttpPost]
//		//public async Task<ActionResult<User>> PostUser(User user)
//		//{
//		//    _context.Users.Add(user);
//		//    await _context.SaveChangesAsync();

//		//    return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
//		//}

//		[HttpPost]
//		public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
//		{
//            var user = new User
//            {
//                UserId= userDto.UserId,
//                UserName = userDto.UserName,
//                Password = userDto.Password,
//                Email = userDto.Email,
//                Address = userDto.Address,
//                TelNumber = userDto.TelNumber,
//                RoleId = userDto.RoleId,
//                CompanyId = userDto.CompanyId
//            };

//			_context.Users.Add(user);
//			await _context.SaveChangesAsync();

//			return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, UserToDTO(user));
//		}

//		// DELETE: api/Users/5

//		[HttpDelete("{id}")]
//		public async Task<IActionResult> DeleteUser(int id)
//		{
//			var user = await _context.Users.FindAsync(id);
//			if (user == null)
//			{
//				return NotFound();
//			}

//			_context.Users.Remove(user);
//			await _context.SaveChangesAsync();

//			return NoContent();
//		}

//		private bool UserExists(int id)
//        {
//            return _context.Users.Any(e => e.UserId == id);
//        }

//		private static UserDto UserToDTO(User user) =>
//	    new UserDto
//	   {
//		   UserId = user.UserId,
//		   UserName = user.UserName,
//           Password = user.Password,
//           Email = user.Email,
//		   Address = user.Address,
//           TelNumber= user.TelNumber,
//           RoleId= user.RoleId,
//           CompanyId = user.CompanyId
//	   };
//	}
//}
