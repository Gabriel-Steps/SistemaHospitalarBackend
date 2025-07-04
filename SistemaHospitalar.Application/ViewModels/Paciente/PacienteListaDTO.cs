namespace SistemaHospitalar.Application.ViewModels.Paciente
{
    public class PacienteListaDTO
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string PlanoSaude { get; set; } = null!;
        public string DiretorioImagem { get; set; } = null!;
    }
}
