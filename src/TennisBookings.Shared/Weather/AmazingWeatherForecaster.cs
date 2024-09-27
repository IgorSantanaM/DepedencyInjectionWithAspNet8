using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisBookings.Shared.Weather
{
	public class AmazingWeatherForecaster : IWeatherForecaster
	{

		public Task<WeatherResult> GetCurrentWeatherAsync(string city)
		{
			return Task.FromResult(new WeatherResult()
			{
				City = city,
				Weather = new WeatherCondition()
				{
					Summary = "Sun"
				}
			});
		}
		
	}
}
