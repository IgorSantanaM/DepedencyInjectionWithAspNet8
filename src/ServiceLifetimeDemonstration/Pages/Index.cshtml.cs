using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceLifetimeDemonstration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
		private readonly IGuidService _guidService;
<<<<<<< HEAD

		public IndexModel(ILogger<IndexModel> logger, IGuidService guidService)
        {
            _logger = logger;
			_guidService = guidService;
=======
		private readonly DisposableServices _disposableServices;

		public IndexModel(ILogger<IndexModel> logger, IGuidService guidService,
			DisposableServices disposableServices)
        {
            _logger = logger;
			_guidService = guidService;
			_disposableServices = disposableServices;
>>>>>>> refs/remotes/origin/master
		}

		public string Guid { get; private set; } = "Missing";

		public string GuidFromMiddleware { get; private set; } = "Missing";

        public void OnGet()
        {
			var guid = _guidService.GetGuid();

			Guid = guid;

			if (HttpContext.Items.TryGetValue(CustomMiddleware.MiddlewareGuid,
				out var mwGuid) && mwGuid is string middlewareGuid)
			{
				GuidFromMiddleware = middlewareGuid;
			}

			_logger.LogInformation($"Controller: The GUID from the " +
				$"GuidService is {guid}");
		}
    }
}
