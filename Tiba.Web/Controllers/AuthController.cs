using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Tiba.Domain.Data;
using Tiba.Domain.Models;
using Tiba.Web.Dtos;

namespace Tiba.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration config;
        private readonly ILogger logger;

        public AuthController(IAuthRepository repo, IConfiguration config, ILoggerFactory loggerFactory)
        {
            this.config = config;
            this.repo = repo;
            logger = loggerFactory.CreateLogger("AuthController");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // todo : validate user 

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await repo.UserExsitsAsync(userForRegisterDto.Username))
                return BadRequest("Username already exsits");

            var userToCreate = new User
            {
                UserName = userForRegisterDto.Username,
            };

            var createdUser = await repo.RegisterAsync(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {

            var userFromRepo = await repo.LoginAsync(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claim = new[]{
                new Claim(ClaimTypes.NameIdentifier ,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name ,userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
