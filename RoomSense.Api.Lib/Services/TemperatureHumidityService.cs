using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomSense.Api.Lib.Data;
using RoomSense.Api.Lib.Data.DTOs;
using RoomSense.Api.Lib.Models;

namespace RoomSense.Api.Lib.Services;

public class TemperatureHumidityService
{
    private readonly TemperatureHumidityDbContext _context;

    public TemperatureHumidityService(TemperatureHumidityDbContext context)
    {
        _context = context;
    }

    public async Task CreateRecord(CreateTemperatureHumidity record)
    {
        
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
}