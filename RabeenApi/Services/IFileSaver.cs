namespace RabeenApi.Services;

public interface IFileSaver
{
    public Task SaveFileAsync(IFormFile file, string path);
    public void RemoveFileIfExist(string path);
}