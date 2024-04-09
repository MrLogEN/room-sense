using Microsoft.EntityFrameworkCore;
using RoomSense.Api.Lib.Data;
using RoomSense.Api.Lib.Data.DTOs;
using RoomSense.Api.Lib.Models;

namespace RoomSense.Api.Lib.Services;

public class TemperatureHumidityService : ITemperatureHumidityService
{
    private readonly TemperatureHumidityDbContext _context;
    private readonly IIdGenerator _idGenerator;
    private readonly ITimeStampGenerator _timeStampGenerator;

    public TemperatureHumidityService(TemperatureHumidityDbContext context, IIdGenerator idGenerator, ITimeStampGenerator timeStampGenerator)
    {
        _context = context;
        _idGenerator = idGenerator;
        _timeStampGenerator = timeStampGenerator;
    }

    public async Task CreateRecord(CreateTemperatureHumidity record)
    {
        var cluster = await _context.Clusters.SingleOrDefaultAsync(c => c.Name == record.ClusterName);

        if (cluster is not null)
        {
            var temphum = new TemperatureHumidity()
            {
                Id = _idGenerator.GenerateId(),
                Cluster = cluster,
                ClusterId = cluster.Id,
                Temperature = record.Temperature,
                Humidity = record.Humidity,
                TimeStamp = _timeStampGenerator.GenerateTimeStamp()
            };
            await _context.AddAsync(temphum);
            await _context.SaveChangesAsync();
        }
        else
        {
            cluster = new Cluster()
            {
                Id = _idGenerator.GenerateId(),
                Name = record.ClusterName
            };
            
            await _context.AddAsync(cluster);
            
            var temphum = new TemperatureHumidity()
            {
                Id = _idGenerator.GenerateId(),
                Cluster = cluster,
                ClusterId = cluster.Id,
                Temperature = record.Temperature,
                Humidity = record.Humidity,
                TimeStamp = _timeStampGenerator.GenerateTimeStamp()
            };
            
            await _context.AddAsync(temphum);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<GetAllRecords>> GetAllRecords()
    {
        return await _context.TemperaturesAndHumidities
            .Select(t => new GetAllRecords()
            {
                ClusterName = t.Cluster.Name,
                Humidity = t.Humidity,
                Temperature = t.Temperature,
                TimeStamp = t.TimeStamp
            }).ToListAsync();
    }

    public async Task<IEnumerable<GetAllRecords>> GetRecordsFilteredByDate(DateTime start, DateTime end)
    {
        if (start > end)
        {
            throw new ArgumentException("starting date must precede end date!");
        }

        var result = await _context.TemperaturesAndHumidities
            .Where(t => t.TimeStamp >= start && t.TimeStamp <= end)
            .Select(t => new GetAllRecords()
            {
                ClusterName = t.Cluster.Name,
                Humidity = t.Humidity,
                Temperature = t.Temperature,
                TimeStamp = t.TimeStamp
            }).ToListAsync();
        
        return result;
    }
    public async Task<IEnumerable<GetAllRecords>> GetRecordsFilteredByCluster(string clusterName)
    {
        return Enumerable.Empty<GetAllRecords>();
    }

    public async Task<IEnumerable<GetAllRecords>> GetRecordsFilteredByClusterAndDate(DateTime start, DateTime end,
        string clusterName)
    {
        return Enumerable.Empty<GetAllRecords>();
    }
}