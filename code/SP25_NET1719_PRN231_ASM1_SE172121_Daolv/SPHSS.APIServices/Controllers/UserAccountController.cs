using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SPHSS.Repository.Models;
using SPHSS.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SPHSS.APIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserAccountService _userAccountsService;

        public UserAccountController(IConfiguration config, UserAccountService userAccountsService)
        {
            _config = config;
            _userAccountsService = userAccountsService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userAccountsService.Authenticate(request.UserName, request.Password);

            if (user == null || user.Result == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user.Result);

            return Ok(token);
        }

        private string GenerateJSONWebToken(UserAccount systemUserAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                new(ClaimTypes.Name, systemUserAccount.UserName),
                //new(ClaimTypes.Email, systemUserAccount.Email),
                new(ClaimTypes.Role, systemUserAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record LoginRequest(string UserName, string Password);
    }
}
