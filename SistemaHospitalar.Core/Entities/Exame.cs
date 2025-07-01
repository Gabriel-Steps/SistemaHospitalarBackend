namespace SistemaHospitalar.Core.Entities
{
    public class Exame
    {
        public Guid Id { get; set; }

        public Guid ProntuarioId { get; set; }
        public Prontuario Prontuario { get; set; } = new();

        public string Tipo { get; set; } = null!;
        public string Resultado { get; set; } = null!;

        public string ArquivoUrl { get; set; } = null!;
    }
}
