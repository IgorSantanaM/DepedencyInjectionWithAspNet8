using Microsoft.AspNetCore.Mvc.RazorPages;
<<<<<<< HEAD
=======
using Microsoft.Extensions.Options;
>>>>>>> refs/remotes/origin/master

namespace TennisBookings.Pages
{
    public class IndexModel : PageModel
    {
		private readonly IWeatherForecaster _weatherForecaster;
		private readonly ILogger<IndexModel> _logger;
<<<<<<< HEAD

		public IndexModel(IWeatherForecaster weatherForecaster, ILogger<IndexModel> logger)
		{
			_weatherForecaster = weatherForecaster;
			_logger = logger;
=======
		private readonly FeaturesConfiguration _config;

		public IndexModel(IWeatherForecaster weatherForecaster, ILogger<IndexModel> logger, IOptionsSnapshot<FeaturesConfiguration> options)
		{
			_weatherForecaster = weatherForecaster;
			_logger = logger;
			_config = options.Value;
>>>>>>> refs/remotes/origin/master
		}
		public string WeatherDescription { get; private set; } =
            "We don't have the latest weather information right now, " +
			"please check again later.";

<<<<<<< HEAD
        public bool ShowWeatherForecast => true;
=======
        public bool ShowWeatherForecast{ get; private set; }
>>>>>>> refs/remotes/origin/master
        public bool ShowGreeting => false;
        public string Greeting => "Welcome to Tennis by the Sea";

        public async Task OnGet()
        {
<<<<<<< HEAD
            try
            {
                var currentWeather = await _weatherForecaster
					.GetCurrentWeatherAsync("Eastbourne");

                switch (currentWeather.Weather.Summary)
                {
                    case "Sun":
                        WeatherDescription = "It's sunny right now. " +
							"A great day for tennis!";
                        break;

                    case "Cloud":
                        WeatherDescription = "It's cloudy at the moment " +
							"and the outdoor courts are in use.";
                        break;

                    case "Rain":
                        WeatherDescription = "We're sorry but it's raining here. " +
							"No outdoor courts in use.";
                        break;

                    case "Snow":
                        WeatherDescription = "It's snowing!! Outdoor courts will " +
							"remain closed until the snow has cleared.";
                        break;
                }
            }
            catch
            {
				_logger.LogError("OH NOOOOOOOOO!");
=======
			if (ShowWeatherForecast)
			{
				try
				{
					var currentWeather = await _weatherForecaster
						.GetCurrentWeatherAsync("Eastbourne");

					switch (currentWeather.Weather.Summary)
					{
						case "Sun":
							WeatherDescription = "It's sunny right now. " +
								"A great day for tennis!";
							break;

						case "Cloud":
							WeatherDescription = "It's cloudy at the moment " +
								"and the outdoor courts are in use.";
							break;

						case "Rain":
							WeatherDescription = "We're sorry but it's raining here. " +
								"No outdoor courts in use.";
							break;

						case "Snow":
							WeatherDescription = "It's snowing!! Outdoor courts will " +
								"remain closed until the snow has cleared.";
							break;
					}
				}
				catch
				{
					_logger.LogError("OH NOOOOOOOOO!");
				}
>>>>>>> refs/remotes/origin/master
			}
        }
    }
}
