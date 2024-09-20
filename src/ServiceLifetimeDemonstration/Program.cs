using ServiceLifetimeDemonstration;

var builder = WebApplication.CreateBuilder(args);
<<<<<<< HEAD

// Add services to the container.
builder.Services.AddRazorPages();
=======
// builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);

var services = builder.Services;
// Add services to the container.
services.AddRazorPages();

// services.AddTransient<IGuidService, GuidService>(); creates a new instance of the service every time is requested
//services.AddSingleton<IGuidService, GuidService>(); creates only one instance for the Container lifetime meaning that it reutilizes the response

services.AddSingleton<IGuidService, GuidService>(); // creates a new instance when a new
services.AddSingleton<IGuidTrimmer, GuidTrimmer>();
services.AddScoped<DisposableServices>();
>>>>>>> refs/remotes/origin/master

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<CustomMiddleware>();

app.MapRazorPages();

app.Run();
