namespace DataAccess.Models;

public class Association : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string UniversityName { get; set; } = string.Empty;
    public string ContactLink { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public IEnumerable<AssociationCooperation> Cooprations { get; set; } = new List<AssociationCooperation>();
}