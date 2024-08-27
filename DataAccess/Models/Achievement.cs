namespace DataAccess.Models;

public class Achievement : BaseModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ExtraInformation { get; set; } = string.Empty; //for example: 1402-1403 or 27-7-1394 etc.
    public Member Owner { get; set; }
}