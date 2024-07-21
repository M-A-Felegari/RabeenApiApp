﻿namespace DataAccess.Models;

public class Member : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public bool IsMainMember { get; set; }
    public string About { get; set; } = string.Empty;

    public IEnumerable<Achievement>? Achievments { get; set; } = new List<Achievement>();
}