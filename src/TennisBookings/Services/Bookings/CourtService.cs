namespace TennisBookings.Services.Bookings
{
	public class CourtService : ICourtService
	{
		private readonly TennisBookingsDbContext _dbContext;

		public CourtService(TennisBookingsDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IEnumerable<Court>> GetOutdoorCourts() =>
			await GetQueryableCourts().Where(c => c.Type == CourtType.Outdoor).ToListAsync();

		public async Task<HashSet<int>> GetCourtIds()
		{
			var ids = await GetQueryableCourts().Select(c => c.Id).OrderBy(c => c).ToListAsync();
			return ids.ToHashSet();
		}

		private IQueryable<Court> GetQueryableCourts() => _dbContext.Courts!.AsNoTracking();
	}
}
