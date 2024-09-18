using System.Text.Json;

namespace TennisBookings.Services.Greetings
{
	public class GreetingService : IHomePageGreetingService
	{
		private static readonly ThreadLocal<Random> Random = new(() => new Random());

		public GreetingService(IWebHostEnvironment webHostEnvironment)
		{
			var webRootPath = webHostEnvironment.WebRootPath;
			var greetingsJson = File.ReadAllText(webRootPath + "/greetings.json");
			var greetingsData = JsonSerializer.Deserialize<GreetingData>(greetingsJson);

			if (greetingsData is not null)
			{
				Greetings = greetingsData.Greetings;
				LoginGreetings = greetingsData.LoginGreetings;
			}
		}

		public string[] Greetings { get; } = Array.Empty<string>();

		public string[] LoginGreetings { get; } = Array.Empty<string>();

		public string GreetingColour => "blue";

		public string GetRandomHomePageGreeting()
		{
			return GetRandomValue(Greetings);
		}

		private static string GetRandomValue(IReadOnlyList<string> greetings)
		{
			if (greetings.Count == 0)
				return string.Empty;

			var greetingToUse = Random.Value!.Next(greetings.Count);

			return greetingToUse >= 0 ? greetings[greetingToUse] : string.Empty;
		}

		private class GreetingData
		{
			public string[] Greetings { get; set; } = Array.Empty<string>();

			public string[] LoginGreetings { get; set; } = Array.Empty<string>();
		}
	}
}
