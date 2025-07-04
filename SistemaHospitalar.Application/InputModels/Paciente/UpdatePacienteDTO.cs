namespace SistemaHospitalar.Application.InputModels.Paciente
{
    public class UpdatePacienteDTO
    {
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string PlanoSaude { get; set; } = null!;
        public string NumeroCarteiraPlano { get; set; } = null!;
    }
}
