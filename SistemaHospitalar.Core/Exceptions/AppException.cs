namespace SistemaHospitalar.Core.Exceptions
{
    public abstract class AppException : Exception
    {
        public int StatusCode { get; set; }
        public AppException(string message, int statusCode = 400) : base(message) 
        {
            StatusCode = statusCode;
        }
    }
}
