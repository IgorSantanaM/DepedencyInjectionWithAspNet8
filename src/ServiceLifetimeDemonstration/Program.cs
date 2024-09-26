using ServiceLifetimeDemonstration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var services =  builder.Services;

services.AddScoped<IGuidTrimmer, GuidTrimmer>();

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
