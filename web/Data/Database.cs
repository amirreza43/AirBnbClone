using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace web
{
  public class Database : DbContext
  {
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Property> Properties { get; set; }
    public Database(DbContextOptions<Database> options) : base(options) { }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Property>()
      .HasMany(p => p.Customers)
      .WithMany(c => c.Properties)
      .UsingEntity<DateRange>(
        // the functions below need to be in a specific order
        j => j.HasOne(d => d.Customer).WithMany(c => c.DateRanges),
        j => j.HasOne(d => d.Property).WithMany(p => p.DateRanges)
      );
    }
  
  }
}
