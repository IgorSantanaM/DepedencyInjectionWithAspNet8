namespace TennisBookings.Services.Unavailability
{
	public class UpcomingHoursUnavailabilityProvider : IUnavailabilityProvider
    {
        private readonly ICourtService _courtService;
		private readonly IUtcTimeService _utcTimeService;

		public UpcomingHoursUnavailabilityProvider(ICourtService courtService,
            IUtcTimeService utcTimeService)
        {
            _courtService = courtService;
			_utcTimeService = utcTimeService;
		}

        public async Task<IEnumerable<HourlyUnavailability>> GetHourlyUnavailabilityAsync(DateTime date)
        {
            var courts = await _courtService.GetCourtIds();

            if (date.Date != DateTime.UtcNow.Date)
                return Array.Empty<HourlyUnavailability>();

            var firstHourAvailable = GetFirstHourAvailable();

            var unavailableHours = new List<HourlyUnavailability>();

            for (var i = 0; i < firstHourAvailable; i++)
            {
                unavailableHours.Add(new HourlyUnavailability(i, courts));
            }

            return unavailableHours;
        }

        public Task<IEnumerable<int>> GetHourlyUnavailabilityAsync(DateTime date, int courtId)
        {
            if (date.Date != _utcTimeService.CurrentUtcDateTime.Date)
                return Task.FromResult(Enumerable.Empty<int>());

            var firstHourAvailable = GetFirstHourAvailable();

            var unavailableHours = new List<int>();

            for (var i = 0; i < firstHourAvailable; i++)
            {
                unavailableHours.Add(i);
            }

            return Task.FromResult(unavailableHours.AsEnumerable());
        }

        private int GetFirstHourAvailable()
        {
            var firstHourAvailable =_utcTimeService.CurrentUtcDateTime.Hour + 1;

            return _utcTimeService.CurrentUtcDateTime.Minute < 30 ? firstHourAvailable : firstHourAvailable + 1;
        }
    }
}
