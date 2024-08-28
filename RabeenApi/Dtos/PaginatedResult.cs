namespace RabeenApi.Dtos;

public class PaginatedResult<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; } = [];
}