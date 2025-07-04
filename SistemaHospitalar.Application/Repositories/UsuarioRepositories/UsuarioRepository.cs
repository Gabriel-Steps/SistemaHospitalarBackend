using Microsoft.EntityFrameworkCore;
using SistemaEstoqueBackend.Application.InputModels.Usuario;
using SistemaHospitalar.Application.InputModels.Usuario;
using SistemaHospitalar.Application.ViewModels.Usuario;
using SistemaHospitalar.Core.Entities;
using SistemaHospitalar.Core.Exceptions.UsuarioException;
using SistemaHospitalar.Core.Models;
using SistemaHospitalar.Infra;

namespace SistemaHospitalar.Application.Repositories.UsuarioRepositories
{
    public interface IUsuarioRepository
    {
        public Task<UsuarioDetailsDTO> Create(CreateUsuarioDTO model);
        public Task<UsuarioDetailsDTO> Login(LoginUsuarioDTO model);
    }
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly HospitalDbContext _context;
        public UsuarioRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task<UsuarioDetailsDTO> Create(CreateUsuarioDTO model)
        {
            if (_context.Usuarios.Any(u => u.Email == model.Email))
                throw new EmailJaRegistradoException();

            var usuario = new Usuario
            {
                NomeCompleto = model.NomeCompleto,
                Email = model.Email,
                SenhaHash = model.SenhaHash,
                Telefone = model.Telefone,
                DiretorioImagem = model.DiretorioImagem,
                CriadoEm = DateTime.Now,
                Role = model.Role
            };

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDetailsDTO
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nome = usuario.NomeCompleto,
                Role = model.Role
            };
        }

        public async Task<UsuarioDetailsDTO> Login(LoginUsuarioDTO model)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.SenhaHash == model.Senha) ??
                    throw new UsuarioNaoEncontradoLoginException();
            return new UsuarioDetailsDTO
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nome = usuario.NomeCompleto,
                Role = usuario.Role
            };
        }
    }
}
