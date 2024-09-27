
using TennisBookings.Caching;

namespace TennisBookings.Services
{
	public class CachedWeatherForecaster : IWeatherForecaster
	{
		private readonly IWeatherForecaster _forecaster;
		private readonly IUtcTimeService _utcTimeService;
		private readonly IDistributedCache<WeatherResult> _cache;

		public CachedWeatherForecaster(IWeatherForecaster forecaster,
			IUtcTimeService utcTimeService,
			IDistributedCache<WeatherResult> cache)
		{
			_forecaster = forecaster;
			_utcTimeService = utcTimeService;
			_cache = cache;
		}


		public async Task<WeatherResult> GetCurrentWeatherAsync(string city)
		{
			var cacheKey = $"Weather_{city}_ {_utcTimeService.CurrentUtcDateTime:yyyy/MM/dd}";

			var (isCached, forecast) = await _cache.TryGetValueAsync(cacheKey);

			if (isCached)
				return forecast!;

			var result = await _forecaster.GetCurrentWeatherAsync(city);

			await _cache.SetAsync(cacheKey, result, 60);

			return result;

		}
	}
}
