using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WepApiProjetoReact.Services.Authenticate;
using WepApiProjetoReact.ViewModels;

namespace WepApiProjetoReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticateInterface _authenticate;

        public AccountController(IConfiguration configuration, IAuthenticateInterface authenticate)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authenticate = authenticate ?? throw new ArgumentNullException(nameof(authenticate));
        }

        [HttpPost("CriarUsuario")]
        public async Task<ActionResult<UserToken>> CriarUsuario([FromBody] RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "As Senhas nao Conferem");
                return BadRequest(ModelState);
            }
            var result = await _authenticate.RegisterUser(model.Email, model.Password);

            if (result)
            {
                return Ok($"Usuario {model.Email} criado com sucesso");
            }
            else
            {
                ModelState.AddModelError("CriarUsuario", "Registro invalido");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUsuario")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);

            if (result)
            {
                return GenerateToken(userInfo);
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Login invalido");
                return BadRequest(ModelState);
            }
        }

        private ActionResult<UserToken> GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuToken", "Tokens"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(20);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt: Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }
    }
}
