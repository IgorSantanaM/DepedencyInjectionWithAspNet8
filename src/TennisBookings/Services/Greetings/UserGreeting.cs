namespace TennisBookings.Services.Greetings
{
	public record class UserGreeting (string Greeting)
	{
		public override string ToString()
		{
			return Greeting;
		}

		public static implicit operator string(UserGreeting g) => g.ToString();
		public static implicit operator UserGreeting(string g) => new(g);
	}
}
