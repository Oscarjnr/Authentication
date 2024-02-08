using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignupSignin.Context;
using SignupSignin.Models;

namespace SignupSignin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User userObject)
        {
            if (userObject == null)
                return BadRequest();

            var userExist = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == userObject.Email && x.Password == userObject.Password);
            if (userExist == null)
                return NotFound(new {Message = "Invalid email or password"});

            return Ok(new
            {
                Message = "Login Successfull!"
            });
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp([FromBody] User userObject)
        {
            if (userObject == null)
                return BadRequest();
            
            await _applicationDbContext.Users.AddAsync(userObject);
            await _applicationDbContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered Successfull!"
            });
        }




    }
}
