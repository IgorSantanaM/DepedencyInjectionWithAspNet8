#region Global Usings
global using Microsoft.AspNetCore.Identity;

global using TennisBookings;
global using TennisBookings.Data;
global using TennisBookings.Domain;
global using TennisBookings.Extensions;
global using TennisBookings.Configuration;
global using TennisBookings.Shared.Weather;
global using TennisBookings.Services.Bookings;
global using TennisBookings.Services.Unavailability;
global using TennisBookings.Services.Bookings.Rules;
global using TennisBookings.Services.Notifications;
global using TennisBookings.Services.Time;
global using TennisBookings.Services.Courts;
global using Microsoft.EntityFrameworkCore;
#endregion

using Microsoft.Data.Sqlite;
using TennisBookings.BackgroundService;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services; // Services == IServiceCollection


services.TryAddSingleton<IWeatherForecaster, RandomWeatherForecaster>(); // register a services so the framework know the DI

//services.Replace(ServiceDescriptor.Singleton<IWeatherForecaster, RandomWeatherForecaster>()); // replace when a new instance of a past call is created and replace to a new one
//services.RemoveAll<IWeatherForecaster>(); // remove all new instances that are created 

#region Service Descriptor Examples

//var serviceDescriptor1 = new ServiceDescriptor(typeof(IWeatherForecaster),
//	typeof(AmazingWeatherForecaster), ServiceLifetime.Singleton);

//var serviceDescriptor2 = ServiceDescriptor.Describe(typeof(IWeatherForecaster),
//	typeof(AmazingWeatherForecaster), ServiceLifetime.Singleton);

//var serviceDescriptor3 = ServiceDescriptor.Singleton(typeof(IWeatherForecaster),
//	typeof(AmazingWeatherForecaster));

//var serviceDescriptor4 = ServiceDescriptor.Singleton<IWeatherForecaster,
//	AmazingWeatherForecaster>();

//services.Add(serviceDescriptor1);
#endregion

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

	
services.Configure<BookingConfiguration>(builder.Configuration.GetSection("CourtBookings"));
services.TryAddScoped<IBookingRuleProcessor, BookingRuleProcessor>();
services.TryAddSingleton<INotificationService, EmailNotificationService>();

services.Configure<ClubConfiguration>(builder.Configuration.GetSection("ClubSettings"));
services.Configure<BookingConfiguration>(builder.Configuration.GetSection("CourtBookings"));
services.Configure<FeaturesConfiguration>(builder.Configuration.GetSection("Features"));


services.TryAddEnumerable(new ServiceDescriptor[]
{
	ServiceDescriptor.Scoped<IUnavailabilityProvider, ClubClosedUnavailabilityProvider>(),
	ServiceDescriptor.Scoped<IUnavailabilityProvider, ClubClosedUnavailabilityProvider>(),
	ServiceDescriptor.Scoped<IUnavailabilityProvider, UpcomingHoursUnavailabilityProvider>(),
	ServiceDescriptor.Scoped<IUnavailabilityProvider, OutsideCourtUnavailabilityProvider>(),
	ServiceDescriptor.Scoped<IUnavailabilityProvider, CourtBookingUnavailabilityProvider>()
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(options =>
{
	options.Conventions.AuthorizePage("/Bookings");
	options.Conventions.AuthorizePage("/BookCourt");
	options.Conventions.AuthorizePage("/FindAvailableCourts");
	options.Conventions.Add(new PageRouteTransformerConvention(new SlugifyParameterTransformer()));
});

#region InternalSetup
using var connection = new SqliteConnection("Filename=:memory:");
//using var connection = new SqliteConnection("Filename=test.db");
connection.Open();

// Add services to the container.
builder.Services.AddDbContext<TennisBookingsDbContext>(options => options.UseSqlite(connection));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<TennisBookingsUser, TennisBookingsRole>(options => options.SignIn.RequireConfirmedAccount = false)
	.AddEntityFrameworkStores<TennisBookingsDbContext>()
	.AddDefaultUI()
	.AddDefaultTokenProviders();

builder.Services.AddHostedService<InitialiseDatabaseService>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.AccessDeniedPath = "/identity/account/access-denied";
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
