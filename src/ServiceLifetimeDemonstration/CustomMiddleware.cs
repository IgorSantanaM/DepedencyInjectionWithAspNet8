namespace ServiceLifetimeDemonstration;

public class CustomMiddleware
{
	public const string MiddlewareGuid = nameof(MiddlewareGuid);

	private readonly RequestDelegate _next;
	private readonly ILogger<CustomMiddleware> _logger;

	public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context, IGuidService guidService)
	{
		var guid = guidService.GetGuid();

		context.Items.Add(MiddlewareGuid, guid);

		var logMessage = $"Middleware: The GUID from " +
						 $"GuidService is {guid}";

		_logger.LogInformation(logMessage);

		await _next(context);
	}
}
