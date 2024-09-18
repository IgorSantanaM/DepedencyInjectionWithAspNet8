namespace ServiceLifetimeDemonstration;

public class GuidTrimmer : IGuidTrimmer
{
	private readonly IGuidService _guidService;

	public GuidTrimmer(IGuidService guidService)
	{
		_guidService = guidService;
	}

	public string TrimmedGuid()
	{
		var guid = _guidService.GetGuid();
		var trimmed = guid.Replace("-", string.Empty);
		return trimmed;
	}
}
