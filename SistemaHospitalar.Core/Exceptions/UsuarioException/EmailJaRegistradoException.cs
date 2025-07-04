namespace SistemaHospitalar.Core.Exceptions.UsuarioException
{
    public class EmailJaRegistradoException : AppException
    {
        public EmailJaRegistradoException() : base("Esse E-mail já está em uso", 400) { }
    }
}
