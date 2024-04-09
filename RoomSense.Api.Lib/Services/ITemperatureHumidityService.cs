using RoomSense.Api.Lib.Data.DTOs;

namespace RoomSense.Api.Lib.Services;

public interface ITemperatureHumidityService
{
    Task CreateRecord(CreateTemperatureHumidity record);
    Task<IEnumerable<GetAllRecords>> GetAllRecords();
    Task<IEnumerable<GetAllRecords>> GetRecordsFilteredByDate(DateTime start, DateTime end);
    Task<IEnumerable<GetAllRecords>> GetRecordsFilteredByCluster(string clusterName);

    Task<IEnumerable<GetAllRecords>> GetRecordsFilteredByClusterAndDate(DateTime start, DateTime end,
        string clusterName);
}