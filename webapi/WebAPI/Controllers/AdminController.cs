using AutoMapper;
using BCrypt.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.AppService.DTO.DTOUser;
using webapi.AppService.DTO.DTOAdmin;
using webapi.Domain.Models;
using webapi.Infrastructure.Database.Contexts;
using webapi.Infrastructure.Database.Entities;

namespace webapi.WebAPI.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AdminControllerV1 : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AdminControllerV1(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public static Admin admin = new Admin();



        [HttpPost("register")]



        public async Task<ActionResult<Admin>> Register(AdminDTO request)
        {

            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.AdminPassword);

            var admin = new Admin()
            {
                AdminName = request.AdminName,
                AdminPassword = passwordHash,
            }; _appDbContext.Admins.Add(admin);

            await _appDbContext.SaveChangesAsync();

            return admin;

        }

        [HttpPost("login")]
        public async Task<ActionResult<Guid>> Login(AdminDTO request)
        {
            var admin = await _appDbContext.Admins.Where(r => r.AdminName == request.AdminName).FirstOrDefaultAsync();


            if (admin != null)
            {
                if (BCrypt.Net.BCrypt.Verify(request.AdminPassword, admin.AdminPassword))
                {
                    string token = CreateToken(admin);
                    return Ok(token);
                }

                else
                {
                    return Ok("wrong passsword");
                }

            }

            else


                return NotFound("user not found");
        }

        private string CreateToken(Admin admin)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.AdminName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }

}
