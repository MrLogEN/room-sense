using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomSense.Api.Lib.Models;

namespace RoomSense.Api.Lib.Data;

public class TemperatureHumidityModelConfiguration :
    IEntityTypeConfiguration<TemperatureHumidity>
{
    public void Configure(EntityTypeBuilder<TemperatureHumidity> builder)
    {
        throw new NotImplementedException();
    }
}