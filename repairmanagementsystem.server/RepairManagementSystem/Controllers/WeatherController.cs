using Microsoft.AspNetCore.Mvc;
using RepairManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
namespace RepairManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")] //[controller] will be replaced by the name of the controller class without the Controller suffix.
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching", "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet]
    [Authorize]
    public IEnumerable<WeatherForecast> GetWeatherForecasts()
    {
        return Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast(
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                Summaries[Random.Shared.Next(Summaries.Length)]
            )).ToArray();
    }
}

