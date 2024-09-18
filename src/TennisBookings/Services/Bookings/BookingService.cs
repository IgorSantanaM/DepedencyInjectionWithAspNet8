namespace TennisBookings.Services.Bookings
{

	public class BookingService : IBookingService
    {
		private readonly ICourtService _courtService;
		private readonly IEnumerable<IUnavailabilityProvider> _unavailabilityProviders;
        
        public BookingService(
			ICourtService courtService,
			IEnumerable<IUnavailabilityProvider> unavailabilityProviders)
        {
			_courtService = courtService;
			_unavailabilityProviders = unavailabilityProviders;
		}

        public async Task<HourlyAvailabilityDictionary> GetBookingAvailabilityForDateAsync(DateTime date)
        {
            var bookingAvailability = await InitialiseAvailability();

            foreach (var provider in _unavailabilityProviders)
            {
                var hourlyUnavailability = await provider
					.GetHourlyUnavailabilityAsync(date);

                foreach (var unavailability in hourlyUnavailability)
                {
                    var courtUnavailability = bookingAvailability[unavailability.Hour];

                    foreach (var courtId in unavailability.UnavailableCourtIds)
                    {
                        courtUnavailability[courtId] = false;
                    }
                }
            }

            return bookingAvailability;
        }

        public async Task<int> GetMaxBookingSlotForCourtAsync(DateTime startTime, DateTime endTime, int courtId)
        {
            var hourlyAvailability = await
				GetBookingAvailabilityForDateAsync(startTime.Date);

            var hoursToCheck = Enumerable
				.Range(startTime.Hour, endTime.Hour - startTime.Hour);

            var lastHourAvailable = endTime.Hour;

            foreach (var hourToCheck in hoursToCheck)
            {
                if (hourlyAvailability[hourToCheck][courtId])
                    continue;

                lastHourAvailable = hourToCheck;
                break;
            }

            return lastHourAvailable - startTime.Hour;
        }

        private async Task<HourlyAvailabilityDictionary> InitialiseAvailability()
        {
            var bookingAvailability = new HourlyAvailabilityDictionary();

            var allCourtIds = await _courtService.GetCourtIds();

            for (var i = 0; i < 24; i++)
            {
                var availability = new Dictionary<int, bool>();

                foreach (var id in allCourtIds)
                {
                    availability[id] = true;
                }

                bookingAvailability[i] = availability;
            }

            return bookingAvailability;
        }
    }
}
