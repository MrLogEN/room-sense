using Microsoft.EntityFrameworkCore;
using RoomSense.Api.Lib.Data;
using RoomSense.Api.Lib.Data.DTOs;
using RoomSense.Api.Lib.Models;
using RoomSense.Api.Lib.Services;

namespace RoomSense.Api.Lib.Tests;

public class TemperatureHumidityServiceTest
{
    private readonly TemperatureHumidityDbContext _contextMock;
    private readonly TemperatureHumidityService _service;

    public TemperatureHumidityServiceTest()
    {
        var options = new DbContextOptionsBuilder<TemperatureHumidityDbContext>()
            .UseInMemoryDatabase(databaseName: "TempHumFakeDB")
            .Options;

        _contextMock = new TemperatureHumidityDbContext(options);
        _contextMock.Database.EnsureDeleted();
        SeedDatabase();
        _service = new TemperatureHumidityService(_contextMock);
    }
    
    [Fact]
    public async Task CreateRecord_CreatesARecordForExistingCluster()
    {
        //arrange
        
        var expectedCount = (await _contextMock.TemperaturesAndHumidities.ToListAsync()).Count + 1;
        var record = new CreateTemperatureHumidity(32, 55, "Room 1");

        //action

        await _service.CreateRecord(record);
        
        var actualCount = (await _contextMock.TemperaturesAndHumidities.ToListAsync()).Count;

        //assert

        
        Assert.Equal(expectedCount, actualCount);

    }
    
    [Fact]
    public async Task CreateRecord_CreatesARecordForNewCluster()
    {
        //arrange
        
        var expectedTempHumCount = (await _contextMock.TemperaturesAndHumidities.ToListAsync()).Count + 1;
        var expectedClusterCount = (await _contextMock.Clusters.ToListAsync()).Count + 1;

        var record = new CreateTemperatureHumidity(32, 55, "Room 3");

        //action

        await _service.CreateRecord(record);

        //assert

        var actualTempHumCount = (await _contextMock.TemperaturesAndHumidities.ToListAsync()).Count;
        var actualClusterCount = (await _contextMock.Clusters.ToListAsync()).Count;

        Assert.Equal(expectedTempHumCount, actualTempHumCount);
        Assert.Equal(expectedClusterCount, actualClusterCount);

    }

    [Fact]
    public async Task GetAllRecords_ShouldReturnACollection()
    {
        //arrange
        var expectedCount = (await _contextMock.TemperaturesAndHumidities.ToListAsync()).Count;
        //action
        var actualCount = (await _service.GetAllRecords()).ToList().Count;
        //assert
        Assert.Equal(expectedCount, actualCount);
    }

    [Fact]
    public async Task GetRecordsFilteredByDate_InvalidRange_ShouldThrow()
    {
        //arrange
        var start = new DateTime(2024, 5, 5, 12, 1, 1, 1);
        var end = new DateTime(2024, 5, 5, 10, 1, 1, 1);
        
        //action
        //assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _service.GetRecordsFilteredByDate(start, end));
        
    }
    [Fact]
    public async Task GetRecordsFilteredByDate_ShouldReturnOneRecord()
    {
        
        //arrange
        
        var start = new DateTime(2024, 5, 5, 12, 3, 48, 12);
        var end = new DateTime(2024, 5, 5, 15, 3, 48, 12);
        var expected = 1;
        
        //action

        var actual = (await _service.GetRecordsFilteredByDate(start, end)).ToList().Count;
        //assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetRecordsFilteredByCluster_ShouldReturnEmptyEnumerable()
    {
        //arrange
        var clusterName = "Never existing cluster";
        var expected = 0;
        //action
        var actual = (await _service.GetRecordsFilteredByCluster(clusterName)).ToList().Count;
        //assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task GetRecordsFilteredByCluster_ShouldReturnTwoRecords()
    {
        //arrange
        var clusterName = "Room 1";
        var expected = 2;
        //action
        var actual = (await _service.GetRecordsFilteredByCluster(clusterName)).ToList().Count;
        //assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetRecordsFilteredByClusterAndDate_NoMatchForDate_ShouldReturnEmptyEnumerable()
    {
        //arrange
        var start = new DateTime(2024, 5, 5, 12, 3, 48, 12);
        var end = new DateTime(2024, 5, 6, 12, 3, 48, 12);
        var clusterName = "Room 1";

        var expected = 0;
        
        //action
        var actual = (await _service.GetRecordsFilteredByClusterAndDate(start, end, clusterName)).ToList().Count;
        
        //assert
        Assert.Equal(expected, actual);
    }
    [Fact]
    public async Task GetRecordsFilteredByClusterAndDate_NoMatchForCluster_ShouldReturnEmptyEnumerable()
    {
        //arrange
        var start = new DateTime(2024, 5, 2, 12, 3, 48, 12);
        var end = new DateTime(2024, 5, 6, 12, 3, 48, 12);
        var clusterName = "Never existing cluster";

        var expected = 0;
        
        //action
        var actual = (await _service.GetRecordsFilteredByClusterAndDate(start, end, clusterName)).ToList().Count;
        
        //assert
        Assert.Equal(expected, actual);
    }
    [Fact]
    public async Task GetRecordsFilteredByClusterAndDate_NoMatchForClusterAndForDate_ShouldReturnEmptyEnumerable()
    {
        //arrange
        var start = new DateTime(2024, 1, 2, 12, 3, 48, 12);
        var end = new DateTime(2024, 1, 6, 12, 3, 48, 12);
        var clusterName = "Never existing cluster";

        var expected = 0;
        
        //action
        var actual = (await _service.GetRecordsFilteredByClusterAndDate(start, end, clusterName)).ToList().Count;
        
        //assert
        Assert.Equal(expected, actual);
    }
    [Fact]
    public async Task GetRecordsFilteredByClusterAndDate_ShouldReturnOneRecord()
    {
        //arrange
        var start = new DateTime(2024, 5, 4, 10, 5, 23, 300);
        var end = new DateTime(2024, 5, 4, 12, 7, 23, 300);
        var clusterName = "Room 1";

        var expected = 1;
        
        //action
        var actual = (await _service.GetRecordsFilteredByClusterAndDate(start, end, clusterName)).ToList().Count;
        
        //assert
        Assert.Equal(expected, actual);
    }
    [Fact]
    public async Task GetRecordsFilteredByClusterAndDate_ShouldReturnTwoRecords()
    {
        //arrange
        var start = new DateTime(2024, 5, 4, 10, 5, 23, 300);
        var end = new DateTime(2024, 5, 4, 13, 7, 23, 300);
        var clusterName = "Room 1";

        var expected = 2;
        
        //action
        var actual = (await _service.GetRecordsFilteredByClusterAndDate(start, end, clusterName)).ToList().Count;
        
        //assert
        Assert.Equal(expected, actual);
    }
    
    private void SeedDatabase()
    {
        
        //clusters
        var cluster1 = new Cluster()
        {
            Id = Guid.Parse("88C6F5B9-2402-4D83-A18D-20837054B344"),
            Name = "Room 1",
        };

        var cluster2 = new Cluster()
        {
            Id = Guid.Parse("22BC423C-2B98-4A63-AAD0-D4C8B9A014F3"),
            Name = "Room 2"
        };
        
        
        _contextMock.Clusters.Add(cluster1);
        _contextMock.Clusters.Add(cluster2);

        //records
        var record1 = new TemperatureHumidity()
        {
            Id = Guid.Parse("A8B37770-FC4E-494C-B855-C7B249F81C17"),
            ClusterId = cluster1.Id,
            Cluster = cluster1,
            Humidity = 40,
            Temperature = 23,
            TimeStamp = new DateTime(2024, 5, 4, 12, 5, 23, 300)
        };
        
        var record2 = new TemperatureHumidity()
        {
            Id = Guid.Parse("9C23DAE5-2C8B-47E9-97D3-0E8D562C3673"),
            ClusterId = cluster1.Id,
            Cluster = cluster1,
            Humidity = 43,
            Temperature = 21,
            TimeStamp = new DateTime(2024, 5, 4, 12, 10, 15, 111)
        };
        
        var record3 = new TemperatureHumidity()
        {
            Id = Guid.Parse("7D917A99-2D98-482F-A1B8-3BEDDFA08C78"),
            ClusterId = cluster2.Id,
            Cluster = cluster2,
            Humidity = 50,
            Temperature = 24,
            TimeStamp = new DateTime(2024, 5, 5, 12, 3, 48, 12)
        };
        
        _contextMock.TemperaturesAndHumidities.Add(record1);
        _contextMock.TemperaturesAndHumidities.Add(record2);
        _contextMock.TemperaturesAndHumidities.Add(record3);

        _contextMock.SaveChanges();

    }
}