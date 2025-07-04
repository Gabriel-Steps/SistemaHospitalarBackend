namespace SistemaHospitalar.Core.Exceptions.ExcpetionsGenericas
{
    public class DadosInvalidosException : AppException
    {
        public DadosInvalidosException() : base("Parametros inválidos", 400) { }
    }
}
