namespace SistemaHospitalar.Core.Entities
{
    public class Prontuario
    {
        public Guid Id { get; set; }

        public Guid ConsultaId { get; set; }
        public Consulta Consulta { get; set; } = new();

        public string Diagnostico { get; set; } = null!;
        public string ObservacoesMedicas { get; set; } = null!;

        public ICollection<Prescricao> Prescricoes { get; set; } = [];
        public ICollection<Exame> Exames { get; set; } = [];
    }
}
