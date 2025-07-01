namespace SistemaHospitalar.Core.Entities
{
    public class Prescricao
    {
        public Guid Id { get; set; }

        public Guid ProntuarioId { get; set; }
        public Prontuario Prontuario { get; set; } = new();

        public string Medicamento { get; set; } = null!;
        public string Posologia { get; set; } = null!;
    }
}
