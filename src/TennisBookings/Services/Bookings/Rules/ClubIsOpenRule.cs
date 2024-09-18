using Microsoft.Extensions.Options;

namespace TennisBookings.Services.Bookings.Rules
{
	public class ClubIsOpenRule : ICourtBookingRule
{
		private readonly ClubConfiguration _clubConfiguration;

		public ClubIsOpenRule(IOptions<ClubConfiguration> options)
		{
			_clubConfiguration = options.Value;
		}

		public Task<bool> CompliesWithRuleAsync(CourtBooking booking)
		{
			var startHourPasses = booking.StartDateTime.Hour >= _clubConfiguration.OpenHour;
			var endHourPasses = booking.EndDateTime.Hour <= _clubConfiguration.CloseHour;

			return Task.FromResult(startHourPasses && endHourPasses);
		}

		public string ErrorMessage => "Can't make a booking when the club is closed";
	}
}
