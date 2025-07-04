namespace SistemaHospitalar.Core.Exceptions.UsuarioException
{
    public class UsuarioNaoEncontradoLoginException : AppException
    {
        public UsuarioNaoEncontradoLoginException() : base("E-mail ou senha incorretos", 401) { }
    }
}
