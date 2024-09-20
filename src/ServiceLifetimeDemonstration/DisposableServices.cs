
namespace ServiceLifetimeDemonstration
{
	public class DisposableServices : IDisposable, IAsyncDisposable
	{
		private readonly ILogger<DisposableServices> _logger;

		public DisposableServices(ILogger<DisposableServices> logger)
		{
			_logger = logger;
		}
		public void Dispose()
		{
			_logger.LogInformation("DISPOSING OF SERVICE");
		}

		public ValueTask DisposeAsync()
		{
			_logger.LogInformation("DISPOSING OF SERVICE (ASYNC)");
			return ValueTask.CompletedTask;
		}
	}
}
