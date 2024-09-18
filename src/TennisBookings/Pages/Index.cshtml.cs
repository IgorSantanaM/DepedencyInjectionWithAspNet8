using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TennisBookings.Pages
{
    public class IndexModel : PageModel
    {
		public string WeatherDescription { get; private set; } =
            "We don't have the latest weather information right now, " +
			"please check again later.";

        public bool ShowWeatherForecast => true;
        public bool ShowGreeting => false;
        public string Greeting => "Welcome to Tennis by the Sea";

        public async Task OnGet()
        {
			var forecaster = new RandomWeatherForecaster();

            try
            {
                var currentWeather = await forecaster
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
				// TODO
			}
        }
    }
}
