namespace SistemaHospitalar.Core.Exceptions.PacienteExceptions
{
    public class PacienteNaoEncontradoException : AppException
    {
        public PacienteNaoEncontradoException() : base("Paciente não encontrado", 404) { }
    }
}
