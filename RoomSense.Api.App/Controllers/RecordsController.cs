using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RoomSense.Api.Lib.Authentication;
using RoomSense.Api.Lib.Data.DTOs;
using RoomSense.Api.Lib.Data.DTOs.Responses;
using RoomSense.Api.Lib.Services;

namespace RoomSense.Api.App.Controllers;

[Controller]
[Route("[controller]")]
public class RecordsController : ControllerBase
{
    private readonly ITemperatureHumidityService _temperatureHumidityService;

    public RecordsController(ITemperatureHumidityService temperatureHumidity)
    {
        _temperatureHumidityService = temperatureHumidity;
    }

    [HttpPost("Create")]
    [ServiceFilter(typeof(ApiKeyFilter))]
    public async Task<IActionResult> CreateRecord([FromBody] CreateTemperatureHumidity record)
    {
        await _temperatureHumidityService.CreateRecord(record);
        return Created();
    }

    [HttpGet]
    public async Task<ActionResult<GetRecordsResponse>> GetAllRecords()
    { 
        var result = await _temperatureHumidityService.GetAllRecords();
        return Ok(new GetRecordsResponse()
        {
            Status = 200,
            Message = "Records fetched successfully.",
            Data = result ?? Enumerable.Empty<GetAllRecords>()
        });
    }

    [HttpGet("Filter/Date")]
    public async Task<ActionResult<GetRecordsResponse>> GetRecordsFilteredByDate([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        try
        {
            var result = await _temperatureHumidityService.GetRecordsFilteredByDate(start, end);
            return Ok(new GetRecordsResponse()
            {
                Status = 200,
                Message = "Records fetched successfully.",
                Data = result ?? Enumerable.Empty<GetAllRecords>()
            });
        }
        catch (Exception e)
        {
            return BadRequest(new GetRecordsResponse()
            {
                Status = 400,
                Message = "Starting date must precede ending date",
            });
        }
    }

    [HttpGet("Filter/Cluster")]
    public async Task<ActionResult<GetRecordsResponse>> GetRecordsFilteredByCluster([FromQuery] string clusterName)
    {
        var result = await _temperatureHumidityService
            .GetRecordsFilteredByCluster(clusterName);
        
        return Ok(new GetRecordsResponse()
        {
            Status = 200,
            Message = "Records fetched successfully.",
            Data = result ?? Enumerable.Empty<GetAllRecords>()
        });
    }

    [HttpGet("Filter/ClusterAndDate")]
    public async Task<ActionResult<GetRecordsResponse>> GetRecordsFilteredByClusterAndDate(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end,
        [FromQuery] string clusterName)
    {
        try
        {
            var result = await _temperatureHumidityService
                .GetRecordsFilteredByClusterAndDate(start, end, clusterName);

            return Ok(new GetRecordsResponse()
            {
                Status = 200,
                Message = "Records fetched successfully.",
                Data = result ?? Enumerable.Empty<GetAllRecords>()
            });
        }
        catch (Exception e)
        {
            return BadRequest(new GetRecordsResponse()
            {
                Status = 400,
                Message = "Starting date must precede ending date",
            });
        }
    }
}