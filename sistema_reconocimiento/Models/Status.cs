namespace sistema_reconocimiento.Models
{
    public class Status
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string AccountId { get; set; }
        public bool IsSuccess { get; set; }
    }
}
