using Microsoft.EntityFrameworkCore;
using ApiCabanas.Models;

namespace ApiCabanas.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cabana> Cabanas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cabana>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Localizacao).IsRequired().HasMaxLength(300);
            entity.Property(e => e.PrecoPorNoite).HasColumnType("TEXT");
        });
    }
}
