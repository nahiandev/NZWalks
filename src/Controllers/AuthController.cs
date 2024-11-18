using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Route("user_action/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _user_manager;
        public AuthController(UserManager<IdentityUser> user_manager)
        {
            _user_manager = user_manager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO new_user)
        {
            var identity_user = new IdentityUser
            {
                UserName = new_user.Username,
                Email = new_user.Username
            };

            var user = await _user_manager.CreateAsync(identity_user, new_user.Password);

            if(user.Succeeded)
            {
                var roles_length = new_user.Roles.Length;

                if(roles_length > 0)
                {
                    user = await _user_manager.AddToRolesAsync(identity_user, new_user.Roles);
                    
                    if(user.Succeeded)
                    {
                        return Ok("User registered successfully");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }
    }
}
