
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using RoomSense.Api.Lib.Data;
using RoomSense.Api.Lib.Data.DTOs;
using RoomSense.Api.Lib.Models;
using RoomSense.Api.Lib.Services;

namespace RoomSense.Api.Lib.Tests;

public class TemperatureHumidityServiceTest
{
    private readonly TemperatureHumidityDbContext _contextMock 
        = Substitute.For<TemperatureHumidityDbContext>(
            new DbContextOptions<TemperatureHumidityDbContext>()
            );
    private readonly TemperatureHumidityService _service;

    public TemperatureHumidityServiceTest()
    {
        _service = new TemperatureHumidityService(_contextMock);
    }
    
    [Fact]
    public async Task CreateRecord_CreatesARecord()
    {
        //arrange
        var newRecord = new CreateTemperatureHumidity(10, 10, "Room 1"); //dto
        
        var records = new List<TemperatureHumidity>(); //list of entities
        
        
        var mappedRecord = new TemperatureHumidity //new entity mapped from the dto
        {
            Id = Guid.NewGuid(),
            Cluster = new Cluster
            {
                Id = Guid.NewGuid(),
                Name = newRecord.ClusterName,
            },
            Temperature = newRecord.Temperature,
            Humidity = newRecord.Humidity,
            TimeStamp = DateTime.Now
        };
        
        _contextMock
            .When(x => x.AddAsync(mappedRecord))
            .Do(x => records.Add(mappedRecord)); //configuring the DbContext's AddAsync method
        
        //action
        _service.CreateRecord(newRecord);

        //assert

        Assert.Contains(mappedRecord, records);
    }
}