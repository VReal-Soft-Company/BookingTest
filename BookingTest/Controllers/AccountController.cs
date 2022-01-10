using BookingTest.BLL.Services;
using BookingTest.DLL.Entities;
using BookingTest.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserService userService)
        {
            _userService = userService; 
        }
        #region AuthRegion
        [AllowAnonymous]
        [HttpPost("Register")] 
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userService.RegisterAsync(model));
            }
            return BadRequest(ModelState);
           
        }
 
          
        [AllowAnonymous]
        [HttpPost("Login")] 
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userService.LoginAsync(model));
            }
            return BadRequest(ModelState);
        }

     
         
        #endregion 
    }
}
