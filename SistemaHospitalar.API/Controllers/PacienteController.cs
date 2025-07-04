using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaHospitalar.Application.InputModels.Paciente;
using SistemaHospitalar.Application.Repositories.PacienteRepositories;
using SistemaHospitalar.Application.ViewModels.Paciente;
using SistemaHospitalar.Application.ViewModels.Usuario;
using SistemaHospitalar.Core.Exceptions.ExcpetionsGenericas;
using SistemaHospitalar.Core.Exceptions.PacienteExceptions;
using SistemaHospitalar.Core.Models;

namespace SistemaHospitalar.API.Controllers
{
    [ApiController, Route("api/paciente")]
    public class PacienteController : ControllerBase
    {
        private IPacienteRepository _repository { get; set; }
        private ILogger<PacienteController> _logger { get; set; }
        public PacienteController(IPacienteRepository repository, ILogger<PacienteController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatePacienteDTO model)
        {
            try
            {
                var data = await _repository.Create(model);
                return Ok(new ApiResponse<UsuarioDetailsDTO>
                {
                    Success = true,
                    Mensagem = "Cadastro feito com sucesso!",
                    Data = data 
                });
            }
            catch (DadosInvalidosException ex)
            {
                _logger.LogError(ex, $"Erro ao criar paciente com usuárioId: {model.UsuarioId}");
                return BadRequest(new ApiResponse<object>
                { 
                    Success = false,
                    Mensagem = ex.Message,
                    Data = null
                });
            }
            catch(UsuarioNaoEncontradoException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o usuário com id: {model.UsuarioId}");
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Mensagem = ex.Message,
                    Data = null
                });
            }
            catch(DocumentoJaCadastradoException ex)
            {
                _logger.LogError(ex, $"Erro ao criar paciente, pois um paciente já possui esse documento: {model.CPF}");
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Mensagem = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("get-all")]
        [Authorize(Roles = "Admin,Medico,Paciente")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.GetAll();
            return Ok(new ApiResponse<List<PacienteListaDTO>>
            {
                Success = true,
                Mensagem = null,
                Data = data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var data = await _repository.GetById(id);
                return Ok(new ApiResponse<FullPacienteDTO>
                { 
                    Success = true,
                    Mensagem = null,
                    Data = data
                });
            }
            catch(PacienteNaoEncontradoException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar paciente com id: {id}");
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Mensagem = ex.Message,
                    Data = null
                });
            }
            catch (UsuarioNaoEncontradoException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o usuário do paciente com id: {id}");
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Mensagem = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePacienteDTO model)
        {
            try
            {
                var data = await _repository.Update(model, id);
                return Ok(new ApiResponse<UsuarioDetailsDTO>
                { 
                    Success = true,
                    Mensagem = "Usuário atualizado com sucesso!",
                    Data = data 
                });
            }
            catch (PacienteNaoEncontradoException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar paciente com id: {id}");
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Mensagem = ex.Message,
                    Data = null
                });
            }
            catch (UsuarioNaoEncontradoException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar usuário do paciente com id: {id}");
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Mensagem = ex.Message,
                    Data = null
                });
            }
        }
    }
}
