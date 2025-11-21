using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

public class TemperatureRequest
{
    public int MaxTemp { get; set; }
    public int MinTemp { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;
    
    public WeatherForecastController(IWeatherForecastService WeatherForecastService)
    {
        _weatherForecastService = WeatherForecastService;
    }

    [HttpPost("generate")]
    public ActionResult<IEnumerable<WeatherForecast>> generate([FromBody] TemperatureRequest request, [FromQuery] int count)
    {   
        var MaxTemp = request.MaxTemp;
        var MinTemp = request.MinTemp;
        if (MinTemp > MaxTemp || count < 1)
        {
            return BadRequest("wrong parameters");
        }
        var result = _weatherForecastService.Get(count, MinTemp, MaxTemp);
        return Ok(result);
    }

}
