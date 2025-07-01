namespace SistemaHospitalar.Core.Entities
{
    public class Medico
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = new();

        public string CRM { get; set; } = null!;
        public string Especialidade { get; set; } = null!;

        public ICollection<Consulta> Consultas { get; set; } = [];
    }
}
