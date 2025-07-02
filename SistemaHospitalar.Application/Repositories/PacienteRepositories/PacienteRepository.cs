using Microsoft.EntityFrameworkCore;
using SistemaHospitalar.Application.InputModels.Paciente;
using SistemaHospitalar.Application.ViewModels.Usuario;
using SistemaHospitalar.Core.Entities;
using SistemaHospitalar.Infra;

namespace SistemaHospitalar.Application.Repositories.PacienteRepositories
{
    public interface IPacienteRepository
    {
        public Task<UsuarioDetailsDTO> Create(CreatePacienteDTO model);
    }
    public class PacienteRepository : IPacienteRepository
    {
        private readonly HospitalDbContext _context;
        public PacienteRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDetailsDTO> Create(CreatePacienteDTO model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Parâmetros inválidos");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == model.UsuarioId);
            if (usuario == null)
                throw new InvalidOperationException("O usuário informado não existe");

            Paciente paciente = new()
            {
                UsuarioId = model.UsuarioId,
                DataNascimento = model.DataNascimento,
                Genero = model.Genero,
                CPF = model.CPF,
                PlanoSaude = model.PlanoSaude,
                NumeroCarteiraPlano = model.NumeroCarteiraPlano
            };

            await _context.Pacientes.AddAsync(paciente);
            await _context.SaveChangesAsync();
            
            return new UsuarioDetailsDTO { 
                Id = paciente.Id,
                Email = usuario.Email,
                Nome = usuario.NomeCompleto,
                Role = usuario.Role
            };
        }
    }
}
