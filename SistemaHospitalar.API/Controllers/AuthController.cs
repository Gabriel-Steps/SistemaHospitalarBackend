using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SistemaHospitalar.Application.InputModels.Usuario;
using SistemaEstoqueBackend.Core.Entities;
using SistemaHospitalar.Infra;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SistemaHospitalar.Core.Entities;
using SistemaEstoqueBackend.Application.InputModels.Usuario;

namespace SistemaEstoqueBackend.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly HospitalDbContext _context;

        public AuthController(IOptions<JwtSettings> jwtOptions, HospitalDbContext context)
        {
            _jwtSettings = jwtOptions.Value;
            _context = context;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(CreateUsuarioDTO dto)
        {
            if (_context.Usuarios.Any(u => u.Email == dto.Email))
                return BadRequest("Email já registrado");

            var usuario = new Usuario
            {
                NomeCompleto = dto.NomeCompleto,
                Email = dto.Email,
                SenhaHash = dto.SenhaHash,
                Telefone = dto.Telefone,
                DiretorioImagem = dto.DiretorioImagem,
                CriadoEm = DateTime.Now,
                Role = dto.Role
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuário registrado com sucesso");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUsuarioDTO dto)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == dto.Email && u.SenhaHash == dto.Senha);

            if (usuario == null)
                return Unauthorized();

            var token = GerarToken(usuario);

            return Ok(new
            {
                token,
                usuario = new { usuario.Id, usuario.NomeCompleto, usuario.Email, usuario.Role }
            });
        }

        private string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, usuario.NomeCompleto),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Role, usuario.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
