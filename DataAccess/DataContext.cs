using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Member> Members { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Association> Associations { get; set; }
    public DbSet<AssociationCooperation> AssociationCooperations { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }
}