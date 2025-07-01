namespace SistemaHospitalar.Core.Entities
{
    public class Paciente
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = new();

        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string PlanoSaude { get; set; } = null!;
        public string NumeroCarteiraPlano { get; set; } = null!;

        public ICollection<Consulta> Consultas { get; set; } = [];
    }
}
