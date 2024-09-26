using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TennisBookings.DependencyInjection
{
	public static class AddingServices
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{

			services.TryAddScoped<ICourtBookingService, CourtBookingService>();
			services.TryAddSingleton<IUtcTimeService, TimeService>();

			services.TryAddScoped<IBookingService, BookingService>();

			services.TryAddScoped<ICourtService, CourtService>();

			services.TryAddScoped<ICourtBookingManager, CourtBookingManager>();

			services.AddSingleton<ICourtBookingRule, ClubIsOpenRule>();
			services.AddSingleton<ICourtBookingRule, MaxBookingLengthRule>();
			services.AddSingleton<ICourtBookingRule, MaxPeakTimeBookingLengthRule>();
			services.AddScoped<ICourtBookingRule, MemberBookingsMustNotOverlapRule>();
			services.AddScoped<ICourtBookingRule, MemberCourtBookingsMaxHoursPerDayRule>();
			services.AddScoped<ICourtMaintenanceService, CourtMaintenanceService>();
				
			services.TryAddScoped<IBookingRuleProcessor, BookingRuleProcessor>();
			services.TryAddSingleton<INotificationService, EmailNotificationService>();

			services.TryAddEnumerable(
			[
				ServiceDescriptor.Scoped<IUnavailabilityProvider, ClubClosedUnavailabilityProvider>(),
				ServiceDescriptor.Scoped<IUnavailabilityProvider, ClubClosedUnavailabilityProvider>(),
				ServiceDescriptor.Scoped<IUnavailabilityProvider, UpcomingHoursUnavailabilityProvider>(),
				ServiceDescriptor.Scoped<IUnavailabilityProvider, OutsideCourtUnavailabilityProvider>(),
				ServiceDescriptor.Scoped<IUnavailabilityProvider, CourtBookingUnavailabilityProvider>()
			]);

			return services;
		}
	}
}
