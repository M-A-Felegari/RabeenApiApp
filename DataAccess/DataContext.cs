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
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>()
            .HasMany(m => m.Achievements)
            .WithOne(a => a.Owner)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Association>()
            .HasMany(a => a.Cooprations)
            .WithOne(c => c.Association)
            .OnDelete(DeleteBehavior.Cascade);
    }
}