namespace webapi.AppService.Common
{
    public class QueryOrCommandResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}
