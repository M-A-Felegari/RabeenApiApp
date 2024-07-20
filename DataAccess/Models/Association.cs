namespace DataAccess.Models;

public class Association : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string UniversityName { get; set; } = string.Empty;
    public IEnumerable<AssociationCoopration> Cooprations { get; set; } = new List<AssociationCoopration>();
}