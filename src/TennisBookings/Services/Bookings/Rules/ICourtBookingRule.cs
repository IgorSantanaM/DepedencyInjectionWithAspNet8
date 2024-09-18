namespace TennisBookings.Services.Bookings.Rules
{
	public interface ICourtBookingRule
	{
		Task<bool> CompliesWithRuleAsync(CourtBooking booking);

		string ErrorMessage { get; }
	}
}
