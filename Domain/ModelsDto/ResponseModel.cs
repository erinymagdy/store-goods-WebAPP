
namespace Domain.ModelsDto
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public object Error { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int ValidationErrorCode { get; set; }
    }
}