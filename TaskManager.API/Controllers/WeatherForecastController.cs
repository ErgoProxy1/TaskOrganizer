using FirebaseAdmin;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly FirestoreDb _firestoreDb;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, FirestoreDb firestoreDb)
    {
        _logger = logger;
        this._firestoreDb = firestoreDb;

  }

  [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var collection = _firestoreDb.Collection("weathertest");
        await collection.AddAsync(new Test() { Name = "John", Hobby = "Hockey" });
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

[FirestoreData]
public class Test {
  [FirestoreProperty(nameof(Name))]
  public string? Name { get; set; }
  
  [FirestoreProperty(nameof(Hobby))]
  public string? Hobby { get; set; }
}
