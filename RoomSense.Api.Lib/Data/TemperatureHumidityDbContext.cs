using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomSense.Api.Lib.Models;

namespace RoomSense.Api.Lib.Data;

public class TemperatureHumidityDbContext : DbContext
{
    public DbSet<TemperatureHumidity> TemperaturesAndHumidities { get; set; }
    public DbSet<Cluster> Clusters { get; set; }

    public TemperatureHumidityDbContext(DbContextOptions<TemperatureHumidityDbContext> options)
    : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("room_sense")
            .ApplyConfiguration(new ClusterModelConfiguration())
            .ApplyConfiguration(new TemperatureHumidityModelConfiguration());
        
        
        base.OnModelCreating(modelBuilder);
    }
}