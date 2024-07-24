namespace RabeenApi.Dtos;

public class BaseResult<T>
{
    public Status Code { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public T Data { get; set; }
}