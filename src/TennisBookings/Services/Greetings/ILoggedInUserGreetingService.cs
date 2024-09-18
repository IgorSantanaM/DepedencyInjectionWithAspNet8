namespace TennisBookings.Services.Greetings
{
	public interface ILoggedInUserGreetingService
	{
		UserGreeting GetLoggedInGreeting(string name);
	}
}
