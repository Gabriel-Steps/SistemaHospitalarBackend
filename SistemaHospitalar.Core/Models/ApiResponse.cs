namespace SistemaHospitalar.Core.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Mensagem { get; set; }
        public T? Data { get; set; }
    }
}
