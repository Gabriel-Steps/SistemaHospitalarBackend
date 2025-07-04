using Microsoft.EntityFrameworkCore;
using SistemaHospitalar.Application.InputModels.Paciente;
using SistemaHospitalar.Application.ViewModels.Paciente;
using SistemaHospitalar.Application.ViewModels.Usuario;
using SistemaHospitalar.Core.Entities;
using SistemaHospitalar.Core.Exceptions.ExcpetionsGenericas;
using SistemaHospitalar.Core.Exceptions.PacienteExceptions;
using SistemaHospitalar.Infra;

namespace SistemaHospitalar.Application.Repositories.PacienteRepositories
{
    public interface IPacienteRepository
    {
        public Task<UsuarioDetailsDTO> Create(CreatePacienteDTO model);
        public Task<List<PacienteListaDTO>> GetAll();
        public Task<FullPacienteDTO> GetById(Guid id);
        public Task<UsuarioDetailsDTO> Update(UpdatePacienteDTO model, Guid idPaciente);
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
                throw new DadosInvalidosException();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == model.UsuarioId) ??
                throw new UsuarioNaoEncontradoException();

            if (await _context.Pacientes.FirstOrDefaultAsync(p => p.CPF == model.CPF) != null)
                throw new DocumentoJaCadastradoException();

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

        public async Task<List<PacienteListaDTO>> GetAll()
        {
            var lista = await (
                from paciente in _context.Pacientes.AsNoTracking()
                join usuario in _context.Usuarios.AsNoTracking()
                    on paciente.UsuarioId equals usuario.Id
                select new PacienteListaDTO
                {
                    Id = paciente.Id,
                    NomeCompleto = usuario.NomeCompleto,
                    Email = usuario.Email,
                    Telefone = usuario.Telefone,
                    CPF = paciente.CPF,
                    PlanoSaude = paciente.PlanoSaude,
                    DiretorioImagem = usuario.DiretorioImagem
                }
            ).ToListAsync();

            return lista;

        }

        public async Task<FullPacienteDTO> GetById(Guid id)
        {
            var paciente = await _context.Pacientes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id) ??
                throw new PacienteNaoEncontradoException();
            var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == paciente.UsuarioId) ??
                throw new UsuarioNaoEncontradoException();
            return new FullPacienteDTO
            {
                NomeCompleto = usuario.NomeCompleto,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                DiretorioImagem = usuario.DiretorioImagem,
                CriadoEm = usuario.CriadoEm,
                Genero = paciente.Genero,
                CPF = paciente.CPF,
                PlanoSaude = paciente.PlanoSaude,
                NumeroCarteiraPlano = paciente.NumeroCarteiraPlano
            };
        }

        public async Task<UsuarioDetailsDTO> Update(UpdatePacienteDTO model, Guid idPaciente)
        {
            var paciente = await _context.Pacientes.FindAsync(idPaciente) ??
                throw new PacienteNaoEncontradoException();
            paciente.DataNascimento = model.DataNascimento;
            paciente.Genero = model.Genero;
            paciente.CPF = model.CPF;
            paciente.PlanoSaude = model.PlanoSaude;
            paciente.NumeroCarteiraPlano = model.NumeroCarteiraPlano;
            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
            var usuario = await _context.Usuarios.FindAsync(paciente.UsuarioId) ??
                throw new UsuarioNaoEncontradoException();
            return new UsuarioDetailsDTO
            {
                Id = paciente.Id,
                Email = usuario.Email,
                Nome = usuario.NomeCompleto,
                Role = usuario.Role
            };
        }
    }
}
