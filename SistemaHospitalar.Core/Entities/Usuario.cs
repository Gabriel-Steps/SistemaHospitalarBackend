namespace SistemaHospitalar.Core.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string SenhaHash { get; set; } = null!;

        public string Telefone { get; set; } = null!;
        public string Role { get; set; } = null!; // Admin, Medico, Secretaria, Paciente
        public string DiretorioImagem { get; set; } = null!;

        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
