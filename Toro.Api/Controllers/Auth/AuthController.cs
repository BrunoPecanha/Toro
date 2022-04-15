﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Toro.Api.Options;
using Toro.Domain;
using Toro.Domain.Commands;
using Toro.Domain.Entity;

namespace ToroApi.Controllers.Auth {  
    [Route("api/investor")]
    public class AuthController : Controller {
        private readonly IAuthService _service;
        private readonly AppSettingsOptions _appSettingsOptions;
        private readonly UserManager<User> _userManager;

      
        public AuthController(IAuthService service, IOptions<AppSettingsOptions> appSettings, UserManager<User> userManager) {
            _service = service;
            _appSettingsOptions = appSettings.Value;
            _userManager = userManager;
        }

        /// <summary>
        /// Endpoint de cadastro de usuário
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] RegisterDto regDto) {
            var command = new NewUserCommand() {
                Email = regDto.Email,
                Cpf = regDto.Cpf,
                Password = regDto.Password
            };

            var ret = await _service.Create(command);

            if (!ret.Valid)
                return BadRequest(ret.Message);

            return Ok();
        }


        /// <summary>
        /// Endpoint de login de usuário
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
            var command = new LoginCommand() {
                Email = loginDto.Email,
                Password = loginDto.Password
            };

            var ret = await _service.Login(command);

            if (!ret.Valid)
                return BadRequest(ret.Message);


            return Ok(await BuildJWT(loginDto.Email));
        }


        private async Task<string> BuildJWT(string email) {

            User user = await _userManager.FindByEmailAsync(email);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettingsOptions.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor {

                Issuer = _appSettingsOptions.Issuer,
                Audience = _appSettingsOptions.ValidIn,
                Expires = DateTime.UtcNow.AddHours(_appSettingsOptions.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
