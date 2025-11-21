namespace Restaurants.API.Controllers
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get(int NumberOfResults, int MinTemp, int MaxTemp);
    }

    public class WeatherForecastService: IWeatherForecastService
    {
        private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
        public IEnumerable<WeatherForecast> Get(int NumberOfResults, int MinTemp, int MaxTemp)
        {
            return Enumerable.Range(1, NumberOfResults).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(MinTemp, MaxTemp),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
