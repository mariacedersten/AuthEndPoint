using Microsoft.AspNetCore.Mvc;
using AuthEndPoint.Models;

namespace AuthEndPoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly List<User> _users = new List<User>();

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Username and password are required");
            }

            if (_users.Any(u => u.UserName == user.UserName))
            {
                return BadRequest("Username already exists");
            }

            _users.Add(user);
            return Ok("User registered successfully");
        }

        [HttpGet("login")]
        public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        {
            var user = _users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok("Login successful");
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var userList = _users.Select(u => new { u.UserName }).ToList();
            return Ok(userList);
        }
    }
} 