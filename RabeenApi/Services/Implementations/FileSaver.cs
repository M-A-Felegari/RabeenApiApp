namespace RabeenApi.Services.Implementations;

public class FileSaver : IFileSaver
{
    public async Task SaveFileAsync(IFormFile file, string path)
    {
        CreateDirectoryIfNotExist(path);
        if (File.Exists(path))
            File.Delete(path);

        await using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    private void CreateDirectoryIfNotExist(string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }
    }
}