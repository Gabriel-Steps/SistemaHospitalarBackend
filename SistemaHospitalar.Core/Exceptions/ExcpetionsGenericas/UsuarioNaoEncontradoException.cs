namespace SistemaHospitalar.Core.Exceptions.ExcpetionsGenericas
{
    public class UsuarioNaoEncontradoException : AppException
    {
        public UsuarioNaoEncontradoException() : base("O usuário informado não foi encontrado", 404) { }
    }
}
