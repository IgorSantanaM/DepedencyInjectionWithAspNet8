using Moq;

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

			var sut = new IndexModel(mockWeatherForecaster.Object, NullLogger<IndexModel>.Instance, null);

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

			var sut = new IndexModel(mockWeatherForecaster.Object, NullLogger<IndexModel>.Instance, null);

			// Act
			await sut.OnGet();

			// Assert
			Assert.Contains("We're sorry but it's raining here.", sut.WeatherDescription);
		}
	}
}
