using FTGO.AnalyticalService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FTGO.AnalyticalService.Infrastructure.AppDbContext;

public class AnalyticDbContext(DbContextOptions<AnalyticDbContext> options) : DbContext(options)
{
    public virtual DbSet<Person> Persons { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}