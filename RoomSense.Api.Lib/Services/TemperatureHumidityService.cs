using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomSense.Api.Lib.Data;
using RoomSense.Api.Lib.Data.DTOs;

namespace RoomSense.Api.Lib.Services;

public class TemperatureHumidityService
{
    private readonly TemperatureHumidityDbContext _context;

    public TemperatureHumidityService(TemperatureHumidityDbContext context)
    {
        _context = context;
    }

    public void CreateRecord(CreateTemperatureHumidity record)
    {
        
    }
}