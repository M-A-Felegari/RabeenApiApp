namespace DataAccess.Models;

public class Member : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsPrimaryMember { get; set; }
    public string About { get; set; } = string.Empty;

    public IEnumerable<Achievment> Achievments { get; set; } = new List<Achievment>();
}