using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using HackatonShop.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HackatonShop.Controllers
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 2000; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork _uow;

        public UserController(UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("[action]")]
        public IActionResult Token(string email, string password)
        {
            var identity = _uow.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
 
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: new List<Claim>(){new Claim(ClaimsIdentity.DefaultNameClaimType,email)
                    , new Claim(ClaimsIdentity.DefaultRoleClaimType, identity.Role)
                    ,new Claim(ClaimsIdentity.DefaultIssuer, identity.Id.ToString())
                },
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
            var response = new
            {
                access_token = encodedJwt,
                email = identity.Email
            };
 
            return Ok(response);
        }

    }
}