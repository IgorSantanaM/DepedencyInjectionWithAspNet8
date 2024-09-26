using System.Runtime.InteropServices;

namespace TennisBookings.Middleware
{
	public class LastRequestMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IUtcTimeService _utcTimeService;
		private readonly UserManager<TennisBookingsUser> _userManager;

		public LastRequestMiddleware(RequestDelegate next,
			IUtcTimeService utcTimeService,
			UserManager<TennisBookingsUser> userManager)
		{
			_next = next;
			_utcTimeService = utcTimeService;
			_userManager = userManager;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if(context.User.Identity is not null &&
				context.User.Identity.IsAuthenticated)
			{
				var user = await _userManager
					.FindByNameAsync(context.User.Identity.Name);
				if(user is not null)
				{
					user.LastRequestUtc = _utcTimeService.CurrentUtcDateTime;
					await _userManager.UpdateAsync(user);
				}
			}
			await _next(context);
		}
	}
}
