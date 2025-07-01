namespace SistemaHospitalar.Core.Entities
{
    public class Notificacao
    {
        public Guid Id { get; set; }

        public Guid IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = new();
        public string Mensagem { get; set; } = null!;
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; } = null!; // Visualizado ou NaoVisualizado
    }
}
