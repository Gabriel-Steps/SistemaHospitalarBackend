namespace SistemaHospitalar.Core.Entities
{
    public class Consulta
    {
        public Guid Id { get; set; }

        public Guid MedicoId { get; set; }
        public Medico Medico { get; set; } = new();

        public Guid PacienteId { get; set; }
        public Paciente Paciente { get; set; } = new();

        public Guid PronturarioId { get; set; } = new();
        public Prontuario Prontuario { get; set; } = new();

        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        public string Status { get; set; } = null!;// Agendada, Confirmada, Realizada, Cancelada

        public string Observacoes { get; set; } = null!;
    }
}
