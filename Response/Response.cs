namespace ExcelCrudMVC.Responses
{
    public class Response<T>
    {
        public Response(string message, T data, bool success = true)
        {
            Message = message;
            Data = data;
            Success = success;
        }

        public string Message { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
    }
}
