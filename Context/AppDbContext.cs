using APISensor.Models;
using Microsoft.EntityFrameworkCore;

namespace APISensor.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 

    }

    public DbSet<Dados> Dados { get; set; }
    public DbSet<CSVDados> CSVDados { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dados>().HasKey(c => c.EquipmentId);
        modelBuilder.Entity<CSVDados>().HasKey(c => c.CSVId);
    }

}
