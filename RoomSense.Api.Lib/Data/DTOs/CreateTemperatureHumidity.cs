namespace RoomSense.Api.Lib.Data.DTOs;

public record CreateTemperatureHumidity(
    float Temperature,
    float Humidity,
    string ClusterName);