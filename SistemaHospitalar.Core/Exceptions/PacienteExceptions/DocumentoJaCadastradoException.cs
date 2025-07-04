namespace SistemaHospitalar.Core.Exceptions.PacienteExceptions
{
    public class DocumentoJaCadastradoException : AppException
    {
        public DocumentoJaCadastradoException() : base("Um paciente já possui o CPF informado", 400) { }
    }
}
