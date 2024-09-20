<<<<<<< HEAD
using Moq;
=======
using Microsoft.Extensions.Options;
using Moq;
using TennisBookings.Configuration;
>>>>>>> refs/remotes/origin/master

namespace TennisBookings.Tests.Pages
{
	public class IndexTests
	{
		[Fact]
		public async Task ReturnsExpectedViewModel_WhenWeatherIsSun()
		{
			// Arrange
			var mockWeatherForecaster = new Mock<IWeatherForecaster>();
			mockWeatherForecaster
				.Setup(x => x.GetCurrentWeatherAsync(It.IsAny<string>()))
				.ReturnsAsync(new WeatherResult
				{
					City = "TestCity",
					Weather = new WeatherCondition { Summary = "Sun" }
				});

<<<<<<< HEAD
			var sut = new IndexModel(mockWeatherForecaster.Object, NullLogger<IndexModel>.Instance);
=======
			var sut = new IndexModel(mockWeatherForecaster.Object, NullLogger<IndexModel>.Instance, new EnabledConfig());
>>>>>>> refs/remotes/origin/master

			// Act
			await sut.OnGet();

			// Assert
			Assert.Contains("It's sunny right now.", sut.WeatherDescription);
		}

		[Fact]
		public async Task ReturnsExpectedViewModel_WhenWeatherIsRain()
		{
			// Arrange
			var mockWeatherForecaster = new Mock<IWeatherForecaster>();
			mockWeatherForecaster
				.Setup(x => x.GetCurrentWeatherAsync(It.IsAny<string>()))
				.ReturnsAsync(new WeatherResult
				{
					City = "TestCity",
					Weather = new WeatherCondition { Summary = "Rain" }
				});

<<<<<<< HEAD
			var sut = new IndexModel(mockWeatherForecaster.Object, NullLogger<IndexModel>.Instance);
=======
			var sut = new IndexModel(mockWeatherForecaster.Object, NullLogger<IndexModel>.Instance, new EnabledConfig());
>>>>>>> refs/remotes/origin/master

			// Act
			await sut.OnGet();

			// Assert
			Assert.Contains("We're sorry but it's raining here.", sut.WeatherDescription);
		}
<<<<<<< HEAD
=======
		private class EnabledConfig : IOptionsSnapshot<FeaturesConfiguration>
		{
			public FeaturesConfiguration Value => new FeaturesConfiguration
			{
				EnableWeatherForecast = true
			};

			public FeaturesConfiguration Get(string? name)
			{
				throw new NotImplementedException();
			}
		}
>>>>>>> refs/remotes/origin/master
	}
}
