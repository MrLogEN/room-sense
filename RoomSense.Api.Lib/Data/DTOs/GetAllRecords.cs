namespace RoomSense.Api.Lib.Data.DTOs;

public class GetAllRecords
{
    public DateTime TimeStamp { get; set; }
    public float Temperature { get; set; }
    public float Humidity { get; set; }
    public string ClusterName { get; set; }
}