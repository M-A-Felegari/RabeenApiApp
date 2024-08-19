namespace RabeenApi.Services.Implementations;

public class FileSaver : IFileSaver
{
    public static string SaveProfilePath { get; } = $@"wwwroot\members-profile";
    public static string SaveCvPath{ get; } = $@"wwwroot\members-cv";
    public static string SaveAssociationLogoPath { get; } = $@"wwwroot\associations-logo";
    public static string SaveCooperationImagePath { get; } = $@"wwwroot\cooperation-image";
    public async Task SaveFileAsync(IFormFile file, string path)
    {
        CreateDirectoryIfNotExist(path);
        RemoveFileIfExist(path);

        await using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    public void RemoveFileIfExist(string path)
    {
        if (File.Exists(path))
            File.Delete(path);
    }

    private static void CreateDirectoryIfNotExist(string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }
    }
}