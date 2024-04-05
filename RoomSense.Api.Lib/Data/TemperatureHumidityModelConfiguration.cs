using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomSense.Api.Lib.Models;

namespace RoomSense.Api.Lib.Data;

public class TemperatureHumidityModelConfiguration :
    IEntityTypeConfiguration<TemperatureHumidity>
{
    public void Configure(EntityTypeBuilder<TemperatureHumidity> builder)
    {
        builder
            .ToTable("temphum")
            .HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .HasColumnType("char(36)")
            .IsRequired()
            .HasColumnName("id");

        builder
            .Property(t => t.TimeStamp)
            .IsRequired()
            .HasColumnType("timestamp")
            .HasColumnName("time");

        builder
            .Property(t => t.Humidity)
            .IsRequired()
            .HasColumnName("hum");

        builder
            .Property(t => t.Temperature)
            .IsRequired()
            .HasColumnName("temp");

        builder
            .Property(t => t.Cluster)
            .HasColumnName("cluster");
    }
}