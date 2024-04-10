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
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllRecords>>> GetAllRecords()
    { 
        var result = await _temperatureHumidityService.GetAllRecords();
        return Ok(result);
    }
}