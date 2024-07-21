using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Member> Members { get; set; }
    public DbSet<Achievement> Achievments { get; set; }
    public DbSet<Association> Associations { get; set; }
    public DbSet<AssociationCoopration> AssociationCooprations { get; set; }
    public DbSet<ContactRequest> ContactRequests { get; set; }
}