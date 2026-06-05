namespace WebApplicationApi.Entity;

public class ResponseModel<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
    public int StatusCode { get; set; }
}