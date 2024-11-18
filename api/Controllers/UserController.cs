using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signIn;
        public UserController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signIn)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signIn = signIn;
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
                    if (roleResult.Succeeded) { 
                        return Ok(new NewUserDto{
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.CreateToken(user)
                        }); 
                    }
                    else { return StatusCode(500, roleResult.Errors); }
                } else {
                    return StatusCode(500, createdUser.Errors);
                }
            } catch (Exception e) {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginDto login) {
            try{
                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == login.Username.ToLower());

                if(user == null) { return Unauthorized("Invalid username"); }

                var result = await _signIn.CheckPasswordSignInAsync(user, login.Password, false);
                if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect.");

                return Ok(new NewUserDto{
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                });
            } catch (Exception e) {
                return StatusCode(500, e);
            }
        }
    }
}