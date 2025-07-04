namespace SistemaHospitalar.Application.ViewModels.Paciente
{
    public class FullPacienteDTO
    {
        public string NomeCompleto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string DiretorioImagem { get; set; } = null!;
        public DateTime CriadoEm { get; set; }
        public string Genero { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string PlanoSaude { get; set; } = null!;
        public string NumeroCarteiraPlano { get; set; } = null!;
    }
}
