using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomSense.Api.Lib.Models;

namespace RoomSense.Api.Lib.Data;

public class ClusterModelConfiguration : 
    IEntityTypeConfiguration<Cluster>
{
    public void Configure(EntityTypeBuilder<Cluster> builder)
    {
        //Id column
        builder
            .ToTable("cluster")
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .IsRequired()
            .IsFixedLength()
            .HasColumnType("char(36)")
            .HasColumnName("id");
        
        //Name column
        builder
            .HasAlternateKey(c => c.Name);
        
        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasColumnName("name");
        
        //Relationship of Cluster with TemperatureHumidity
        builder
            .HasMany<TemperatureHumidity>(c => c.Records)
            .WithOne(t => t.Cluster)
            .HasForeignKey(t => t.Cluster)
            .HasConstraintName("FK_temphum_cluster")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}