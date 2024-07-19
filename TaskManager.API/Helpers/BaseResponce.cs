namespace TaskManager.API.Helpers
{
    public class BaseResponce<T>
    {
        public bool IsOkay { get; set; }
        public int StatusCode { get; set; } = 200;
        public T? Data { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
