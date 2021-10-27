using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AuthCommon;
using AuthApiApplication.Models;

namespace AuthApiApplication.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthCotroller : ControllerBase
    {
        ApplicationContext db;
        private readonly IOptions<AuthOptions> AuthOptions;
        public AuthCotroller(IOptions<AuthOptions> authOptions, ApplicationContext context)
        {
            db = context;
            AuthOptions = authOptions;          

            if (!db.Accounts.Any())
            {
                db.Accounts.Add(
                    new Account
                    {
                        Id = Guid.Parse("db5c10b3-2b1e-4286-8f5a-f2fe32cac35b"),
                        AccountName = "root",
                        Password = "root",
                        Email = "root@gmail.com",
                        Roles = new Role[] { Role.Admin }
                    });
                db.Accounts.Add(
                    new Account
                    {
                        Id = Guid.Parse("db5c10b5-2b1e-4286-8f5a-f2fe32cac35b"),
                        AccountName = "guest",
                        Password = "guest",
                        Email = "guest@gmail.com",
                        Roles = new Role[] { Role.Guest }
                    });
                db.Accounts.Add(
                    new Account
                    {
                        Id = Guid.Parse("accfe27d-3713-461d-88b8-ec207bfced6d"),
                        AccountName = "Ivan",
                        Password = "ivan",
                        Email = "qwerty@gmail.com",
                        Roles = new Role[] { Role.User }
                    });
                db.SaveChanges();
            }
        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login request)
        {
            var user = AuthenticateUser(request.Email, request.Password);
            if (user != null)
            {
                var token = GenerateJWT(user);
                return Ok(new { access_token = token });
            }
            return Unauthorized();
        }
        private Account AuthenticateUser(string email, string password)
        {
            return db.Accounts.SingleOrDefault(a => a.Email == email && a.Password == password);
        }
        private string GenerateJWT(Account user)
        {
            var authParams = AuthOptions.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }
            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
