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
using SistemaHospitalar.Core.Models;
using SistemaHospitalar.Application.ViewModels.Usuario;
using SistemaHospitalar.Application.Repositories.UsuarioRepositories;
using SistemaHospitalar.Core.Exceptions.UsuarioException;
using System.Threading.Tasks;

namespace SistemaEstoqueBackend.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly HospitalDbContext _context;
        private readonly IUsuarioRepository _repository;
        private readonly ILogger<AuthController> _logger;


        public AuthController(IOptions<JwtSettings> jwtOptions, HospitalDbContext context, IUsuarioRepository repository, ILogger<AuthController> logger)
        {
            _jwtSettings = jwtOptions.Value;
            _context = context;
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(CreateUsuarioDTO dto)
        {
            try
            {
                var data = await _repository.Create(dto);
                return Ok(new ApiResponse<UsuarioDetailsDTO> 
                {
                    Success = true,
                    Mensagem = null,
                    Data = data
                });
            }catch(EmailJaRegistradoException ex)
            {
                _logger.LogError(ex, $"Erro: Esse e-mail já está em uso: {dto.Email}");
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Mensagem = "E-mail já em uso",
                    Data = null
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDTO dto)
        {
            try
            {
                var data = await _repository.Login(dto);
                var token = GerarToken(data);
                return Ok(new ApiResponse<UsuarioDetailsDTO>
                {
                    Success = true,
                    Mensagem = "Login efetuado com sucesso",
                    Data = data
                });
            }catch(UsuarioNaoEncontradoLoginException ex)
            {
                _logger.LogError(ex, $"Erro: Erro ao efetuar login com e-mail de usuário: {dto.Email}");
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Mensagem = "E-mail ou senha inválidos",
                    Data = null
                });
            }            
        }

        private string GerarToken(UsuarioDetailsDTO usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, usuario.Nome),
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
