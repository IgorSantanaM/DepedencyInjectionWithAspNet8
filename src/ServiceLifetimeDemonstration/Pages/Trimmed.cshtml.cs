using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceLifetimeDemonstration.Pages
{
	public class BoomModel : PageModel
    {
		private readonly IGuidTrimmer _guidTrimmer;

		public BoomModel(IGuidTrimmer guidTrimmer)
		{
			_guidTrimmer = guidTrimmer;
		}

		public string TrimmedGuid => _guidTrimmer.TrimmedGuid();

		public void OnGet()
        {
        }
    }
}
