using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisBookings.Shared.Weather
{
	public class AmazingWeatherForecaster
	{
		public Task<WeatherResult> GetWeatherResultAsync(string city) =>
		Task.FromResult(new WeatherResult()
		{
			City = city,
			Weather = new WeatherCondition()
			{
				Summary = "Sun"
			}
		});
	}
}
