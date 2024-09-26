using TennisBookings.Shared.Weather;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Services.AddSingleton<IWeatherForecaster, RandomWeatherForecaster>();

app.MapGet("/weather/{city}", async (string city, IWeatherForecaster forecaster) =>
	{
		var forecast = await forecaster.GetCurrentWeatherAsync(city);
		return forecaster.Weather;
	});

app.Run();
