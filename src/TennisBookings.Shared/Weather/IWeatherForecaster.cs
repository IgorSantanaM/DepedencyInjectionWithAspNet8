
namespace TennisBookings.Shared.Weather
{
	public interface IWeatherForecaster
	{
		public string Weather { get; set; }
		Task<WeatherResult> GetCurrentWeatherAsync(string city);
	}
}
