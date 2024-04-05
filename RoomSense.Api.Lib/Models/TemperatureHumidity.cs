namespace RoomSense.Api.Lib.Models;

public class TemperatureHumidity
{
    public Guid Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public float Temperature { get; set; }
    public float Humidity { get; set; }
    public Cluster Cluster { get; set; }
}