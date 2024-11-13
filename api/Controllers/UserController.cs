using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register) {
            try {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                var user = new User {
                    UserName = register.Username,
                    Email = register.Email
                };

                var createdUser = await _userManager.CreateAsync(user, register.Password);

                if (createdUser.Succeeded) {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded) { return Ok("User created"); }
                    else { return StatusCode(500, roleResult.Errors); }
                } else {
                    return StatusCode(500, createdUser.Errors);
                }
            } catch (Exception e) {
                return StatusCode(500, e);
            }
        }
    }
}