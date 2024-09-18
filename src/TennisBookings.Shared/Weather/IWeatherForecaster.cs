
namespace TennisBookings.Shared.Weather
{
	public interface IWeatherForecaster
	{
		Task<WeatherResult> GetCurrentWeatherAsync(string city);
	}
}