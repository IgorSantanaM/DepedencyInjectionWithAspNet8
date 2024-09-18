namespace TennisBookings.Services.Time
{
	public class TimeService : IUtcTimeService
	{
		public DateTime CurrentUtcDateTime => DateTime.UtcNow;
	}
}
