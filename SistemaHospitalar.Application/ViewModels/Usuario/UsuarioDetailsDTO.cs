namespace SistemaHospitalar.Application.ViewModels.Usuario
{
    public class UsuarioDetailsDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
